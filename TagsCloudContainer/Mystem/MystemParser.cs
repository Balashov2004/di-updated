using System.Text.Json;
using TagsCloudContainer.DTO;
using TagsCloudVisualization.Interface;

namespace TagsCloudContainer.Mystem;

public class MystemParser: IMystemParser
{
    public List<TextMystem> Parse(string jsonOutput)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return JsonSerializer.Deserialize<List<TextMystem>>(jsonOutput, options);
    }
}