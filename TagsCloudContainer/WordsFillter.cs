using System.Diagnostics;
using System.Text;
using System.Text.Json;
using TagsCloudContainer.DTO;


namespace TagsCloudContainer;

public class WordsFilter
{
    private const string MystemPath = "./resources/mystem.exe";
    private readonly AppSettings settings;
    
    public WordsFilter(AppSettings settings)
    {
        this.settings = settings;
        if (!File.Exists(MystemPath))
        {
            throw new FileNotFoundException($"Не найден Mystem. Проверьте путь: {MystemPath}");
        }
    }

    public List<string> ApplyFilter(List<string> words)
    {
        var inputWords = string.Join(" ", words.Select(w => w.ToLower()));
        var mystemResult = RunMystem(inputWords);
        var analysisList = ParseMystemOutput(mystemResult);
        var filteredWords = words
            .Where((word, index) => 
            {
                if (index < analysisList.Count)
                {
                    return !IsBoring(word, analysisList[index]);
                }
                return true;
            })
            .ToList();
        
        return filteredWords;
    }

    private string RunMystem(string input)
    {
        var arguments = "-i --eng-gr --format json";

        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = MystemPath,
                Arguments = arguments,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardInputEncoding = Encoding.UTF8,
                StandardOutputEncoding = Encoding.UTF8
            }
        };
        process.Start();
        process.StandardInput.WriteLine(input);
        process.StandardInput.Close();
        var output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        
        
        return output;
    }

    private List<TextMystem> ParseMystemOutput(string jsonOutput)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var result = JsonSerializer.Deserialize<List<TextMystem>>(jsonOutput, options);
            
        return result ?? new List<TextMystem>();
    }

    private bool IsBoring(string word, TextMystem mystemWord)
    {
        if (mystemWord?.WordAnalyses == null || 
            mystemWord.WordAnalyses.Count == 0)
        {
            return false; 
        }
        var analysis = mystemWord.WordAnalyses[0];
        if (string.IsNullOrEmpty(analysis.Grammar))
        {
            return false;
        }
        var fullTag = analysis.Grammar.ToUpperInvariant();
        var cleanTag = fullTag.Split(new char[] { '=' }, 2).First().Trim();
        var po = cleanTag.Split(new char[] { ',', '|' }, 2).First().Trim();
        var isBoring = settings.ExcludePartsSpeech.Contains(po);
        return isBoring;
    }
    
}