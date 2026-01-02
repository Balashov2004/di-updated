using TagsCloudContainer;

namespace TagsCloudVisualization.Interface;

public interface IFileReaderFactory
{
    Result<IFileReader> GetReader(string filePath);
}