using Autofac.Features.Indexed;
using System.IO;
using TagsCloudVisualization.Interface;

namespace TagsCloudContainer.WorkWithFiles;

public class FileCoordinator : IFileReader
{
    private readonly IIndex<string, IFileReader> readers;
    
    public FileCoordinator(IIndex<string, IFileReader> readers)
    {
        this.readers = readers;
    }
    public string ReadAllText(string filePath)
    {
        var extension = Path.GetExtension(filePath).ToLowerInvariant();
        
        if (readers.TryGetValue(extension, out var reader))
        {
            return reader.ReadAllText(filePath);
        }
            
        throw new NotSupportedException($"Расширение '{extension}' не поддерживается.");
    }
}