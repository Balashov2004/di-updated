using System.Drawing;
using System.Text.RegularExpressions;
using WeCantSpell.Hunspell;
using TagsCloudVisualization.Interface;

namespace TagsCloudContainer;

public class TextProcessor
{
    private readonly AppSettings appSettings;
    public List<WordData> ProcessWords { get; private set; } = new List<WordData>();

    public TextProcessor(AppSettings appSettings)
    {
        this.appSettings = appSettings;
    }

    public void Process(string text)
    {
        var stats = GetWordStat(text);
        var max = stats.MaxCount;
        var min = stats.MinCount;
        foreach (var pair in stats.WordCounts)
        {
            ProcessWords.Add(CreateWordData(pair.Key, pair.Value, min, max));
        }
    }

    private WordStat GetWordStat(string text)
    {
        const string delimitersPattern = @"[\W_]+";
        var words = Regex.Split(text, delimitersPattern, RegexOptions.IgnoreCase)
            .Where(w => !string.IsNullOrWhiteSpace(w))
            .Select(w => w.ToLowerInvariant())
            .GroupBy(w => w)
            .ToDictionary(g => g.Key, g => g.Count());
        
        var maxCount = words.Values.DefaultIfEmpty(0).Max();
        var minCount = words.Values.DefaultIfEmpty(0).Min();
        return new WordStat(words, maxCount, minCount);
    }
    
    private WordData CreateWordData(string word, int count, int minCount,  int maxCount)
    {
        var spread = maxCount - minCount;
        var normalize = spread == 0 ? 0.5 : (double)(count - minCount) / spread;
        
        var fontSize = (int)Math.Round(appSettings.MinFontSize + 
                                       (appSettings.MaxFontSize - appSettings.MinFontSize)
                                       * normalize);
        
        var wordFont = new Font(appSettings.DefaultFontName, fontSize);
        var size = MeasureWordSize(word, wordFont);
        
        return new WordData(word, wordFont, size);
    }

    private Size MeasureWordSize(string word, Font font)
    {
        using var tempBitmap = new Bitmap(1, 1);
        using var graphics = Graphics.FromImage(tempBitmap);
        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        var sizeF = graphics.MeasureString(word, font);
        
        return new Size((int)Math.Ceiling(sizeF.Width), (int)Math.Ceiling(sizeF.Height));
    }
    
}