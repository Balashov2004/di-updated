namespace TagsCloudVisualization.Interface;

public interface IWordsFilter
{
    List<string> ApplyFilter(List<string> words);
}