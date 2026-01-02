using TagsCloudContainer;

namespace TagsCloudVisualization.Interface;

public interface IWordsFilter
{
    Result<List<string>> ApplyFilter(List<string> words);
}