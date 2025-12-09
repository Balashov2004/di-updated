namespace TagsCloudContainer;

public class WordStat
{
    public Dictionary<string, int> WordCounts { get; }
    public int MaxCount { get; }
    public int MinCount { get; }

    public WordStat(Dictionary<string, int> wordCounts, int maxCount, int minCount)
    {
        WordCounts = wordCounts;
        MaxCount = maxCount;
        MinCount = minCount;
    }
}