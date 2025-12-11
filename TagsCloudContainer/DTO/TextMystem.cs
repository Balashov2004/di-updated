using System.Text.Json.Serialization;

namespace TagsCloudContainer.DTO;

public class TextMystem
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = "";
    
    [JsonPropertyName("analysis")]
    public List<WordAnalysis>? WordAnalyses { get; set; }
}