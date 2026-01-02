using System.Drawing;
using System.Text.RegularExpressions;
using TagsCloudContainer.DTO;
using TagsCloudVisualization.Interface;


namespace TagsCloudContainer;

public class TextProcessor
{
    private readonly AppSettings appSettings;
    public List<WordData> ProcessWords { get; } = new();
    private readonly IWordsFilter wordsFilter;

    public TextProcessor(AppSettings appSettings, IWordsFilter wordsFilter)
    {
        this.appSettings = appSettings;
        this.wordsFilter = wordsFilter;
    }

    public Result<List<WordData>> Process(string text)
    {
        ProcessWords.Clear();
        var statsResult = GetWordStat(text);
        
        if (!statsResult.IsSuccess)
            return Result<List<WordData>>.Failure(statsResult.ErrorMessage);
        
        var stats = statsResult.Value;
        
        if (stats.WordCounts.Count == 0)
            return Result<List<WordData>>.Failure("После фильтрации не осталось слов для отрисовки.");

        foreach (var pair in stats.WordCounts)
        {
            ProcessWords.Add(CreateWordData(pair.Key, pair.Value, stats.MinCount, stats.MaxCount));
        }

        return Result<List<WordData>>.Success(ProcessWords);
    }

    private Result<WordStat> GetWordStat(string text)
    {
        const string delimitersPattern = @"[\W_]+";
        var words = Regex.Split(text, delimitersPattern, RegexOptions.IgnoreCase)
            .Where(w => !string.IsNullOrWhiteSpace(w))
            .Select(w => w.ToLowerInvariant())
            .ToList();
        var filterResult = wordsFilter.ApplyFilter(words);
        if (!filterResult.IsSuccess)
            return Result<WordStat>.Failure(filterResult.ErrorMessage);
        
        var wordCounts = filterResult.Value
            .GroupBy(w => w)
            .ToDictionary(g => g.Key, g => g.Count());
        var maxCount = wordCounts.Values.DefaultIfEmpty(0).Max();
        var minCount = wordCounts.Values.DefaultIfEmpty(0).Min();
        
        return Result<WordStat>.Success(new WordStat(wordCounts,  maxCount, minCount));
    }
    
    private WordData CreateWordData(string word, int count, int minCount,  int maxCount)
    {
        var spread = maxCount - minCount;
        var normalize = spread == 0 ? 0.5 : (double)(count - minCount) / spread;
        
        var fontSize = (int)Math.Round(appSettings.MinFontSize + 
                                       (appSettings.MaxFontSize - appSettings.MinFontSize)
                                       * normalize);
        
        var size = MeasureWordSize(word, fontSize);
        
        return new WordData(word, fontSize, size);
    }

    private Size MeasureWordSize(string word, int fontSize)
    {
        using var font = new Font(appSettings.FontName, fontSize);
        using var tempBitmap = new Bitmap(1, 1);
        using var graphics = Graphics.FromImage(tempBitmap);

        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        
        var sizeF = graphics.MeasureString(word, font);
        
        return new Size(
            (int)Math.Ceiling(sizeF.Width),
            (int)Math.Ceiling(sizeF.Height)
        );
    }
    
}