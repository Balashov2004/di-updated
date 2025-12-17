using TagsCloudContainer.DTO;

namespace TagsCloudVisualization.Interface;

public interface IMystemParser
{
    List<TextMystem> Parse(string jsonOutput);
}