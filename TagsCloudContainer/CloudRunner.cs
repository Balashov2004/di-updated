using System.Drawing.Imaging;
using TagsCloudVisualization.Interface;


namespace TagsCloudContainer;

public class CloudRunner
{
    
    private readonly CircularCloudLayouter layouter;
    private readonly TextProcessor textProcessor;
    private readonly AppSettings appSettings;
    private readonly CloudPainter cloudPainter;
    private readonly IFileReaderFactory readerFactory;
    
    public CloudRunner(
        AppSettings appSettings, 
        CircularCloudLayouter layouter, 
        TextProcessor textProcessor,
        CloudPainter cloudPainter,
        IFileReaderFactory readerFactory)
    {
        this.appSettings = appSettings;
        this.layouter = layouter;
        this.textProcessor = textProcessor;
        this.cloudPainter = cloudPainter;
        this.readerFactory = readerFactory;
    }

    public Result<string> Run()
    {
        var readerResult = readerFactory.GetReader(appSettings.WordsFilePath);
        if (!readerResult.IsSuccess)
            return Result<string>.Failure(readerResult.ErrorMessage);
        var text = readerResult.Value.ReadAllText(appSettings.WordsFilePath);
        var result = textProcessor.Process(text);
        if (!result.IsSuccess) return Result<string>.Failure(result.ErrorMessage);
        
        var placesWords = result.Value;

        foreach (var wordData in placesWords)
        {
            var rectSize = wordData.Size;
            var rect = layouter.PutNextRectangle(rectSize);
            wordData.SetPlacement(rect); 
        }
        cloudPainter.SaveImage(placesWords, appSettings.OutputPath, ImageFormat.Png);
        return Result<string>.Success(appSettings.OutputPath);
    }
}
