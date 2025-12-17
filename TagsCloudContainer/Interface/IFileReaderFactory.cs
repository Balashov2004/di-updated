namespace TagsCloudVisualization.Interface;

public interface IFileReaderFactory
{
    IFileReader GetReader(string filePath);
}