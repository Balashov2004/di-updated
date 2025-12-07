using TagsCloudVisualization.Interface;

namespace TagsCloudContainer;

public class FileReader : IFileReader
{
    public string ReadAllText(string path)
    {
        return File.ReadAllText(path);
    }
}