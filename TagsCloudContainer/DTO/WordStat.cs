namespace TagsCloudContainer.DTO;

public class WordStat(Dictionary<string, int> wordCounts, int maxCount, int minCount)
{
    public Dictionary<string, int> WordCounts { get; } = wordCounts;
    public int MaxCount { get; } = maxCount;
    public int MinCount { get; } = minCount;
}