using Autofac.Features.Indexed;
using System.IO;
using TagsCloudVisualization.Interface;

namespace TagsCloudContainer.FileReaders;

public class FileReaderFactory : IFileReaderFactory
{
    private readonly IIndex<string, IFileReader> readers;

    public FileReaderFactory(IIndex<string, IFileReader> readers)
    {
        this.readers = readers;
    }

    public Result<IFileReader> GetReader(string filePath)
    {
        var extension = Path.GetExtension(filePath).ToLowerInvariant();

        if (readers.TryGetValue(extension, out var reader))
        {
            return Result<IFileReader>.Success(reader);
        }

        return Result<IFileReader>.Failure($"Расширение '{extension}' не поддерживается.");
    }
}