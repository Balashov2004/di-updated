using TagsCloudContainer.DTO;
using TagsCloudVisualization.Interface;


namespace TagsCloudContainer;

public class WordsFilter : IWordsFilter
{
    private readonly AppSettings settings;
    private readonly IMystemRunner runner;
    private readonly IMystemParser parser;

    public WordsFilter(AppSettings settings, IMystemRunner runner, IMystemParser parser)
    {
        this.settings = settings;
        this.runner = runner;
        this.parser = parser;
    }

    public Result<List<string>> ApplyFilter(List<string> words)
    {
        var inputWords = string.Join(" ", words.Select(w => w.ToLower()));
        var result = runner.GetAnalysisJson(inputWords);
        if (!result.IsSuccess)
            return Result<List<string>>.Failure(result.ErrorMessage);
        var analysisList = parser.Parse(result.Value);
        
        var filtered = words
            .Where((word, index) => !IsBoring(index, analysisList))
            .ToList();
        
        return Result<List<string>>.Success(filtered);
    }

    private bool IsBoring(int index, List<TextMystem> analysisList)
    {
        if (index >= analysisList.Count || analysisList[index].WordAnalyses?.Count == 0)
            return false;

        var grammar = analysisList[index].WordAnalyses[0].Grammar?.ToUpperInvariant();
        if (string.IsNullOrEmpty(grammar)) return false;

        
        var partOfSpeech = grammar.Split('=', ',', '|')[0].Trim();
        
        return settings.ExcludePartsSpeech.Contains(partOfSpeech);
    }
}
