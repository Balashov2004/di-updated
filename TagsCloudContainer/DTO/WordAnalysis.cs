using System.Text.Json.Serialization;

namespace TagsCloudContainer.DTO;

public class WordAnalysis
{
    [JsonPropertyName("gr")]
    public string Grammar { get; set; } = ""; 
    
    [JsonPropertyName("lex")]
    public string Lemma { get; set; } = "";
}