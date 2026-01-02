using System.Text.Json.Serialization;

namespace TagsCloudContainer.DTO;

public class TextMystem
{
    [JsonPropertyName("analysis")]
    public List<WordAnalysis>? WordAnalyses { get; set; }
}