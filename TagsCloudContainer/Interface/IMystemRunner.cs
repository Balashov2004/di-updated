using TagsCloudContainer;

namespace TagsCloudVisualization.Interface;

public interface IMystemRunner
{
    Result<string> GetAnalysisJson(string input);
}