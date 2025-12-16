using TagsCloudVisualization.Interface;
using System.IO;

namespace TagsCloudContainer.FileReaders;

public class TxtFileReader : IFileReader
{
    public string ReadAllText(string filePath)
    {
        return File.ReadAllText(filePath);
    }
}