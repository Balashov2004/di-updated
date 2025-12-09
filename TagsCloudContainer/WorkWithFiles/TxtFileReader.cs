using TagsCloudVisualization.Interface;
using System.IO;

namespace TagsCloudContainer.WorkWithFiles;

public class TxtFileReader : IFileReader
{
    public string ReadAllText(string filePath)
    {
        return File.ReadAllText(filePath);
    }
}