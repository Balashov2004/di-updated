using System.Text;
using DocumentFormat.OpenXml.Packaging;

using TagsCloudVisualization.Interface;

namespace TagsCloudContainer.WorkWithFiles;

public class DocxFileReader : IFileReader
{
    public string ReadAllText(string filePath)
    {
        using (WordprocessingDocument doc = WordprocessingDocument.Open(filePath, true))
        {
            var body = doc.MainDocumentPart.Document.Body;
            var sb = new StringBuilder();

            foreach (var part in body) sb.Append(part.InnerText + '\n');
            return sb.ToString();
        }
        
    }
}