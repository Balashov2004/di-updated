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

    public void Run()
    {
        var reader = readerFactory.GetReader(appSettings.WordsFilePath);
        var text = reader.ReadAllText(appSettings.WordsFilePath);
        textProcessor.Process(text);
        var placesWords = textProcessor.ProcessWords;

        foreach (var wordData in placesWords)
        {
            var rectSize = wordData.Size;
            
            var rect = layouter.PutNextRectangle(rectSize);
            wordData.SetPlacement(rect); 
        }
        cloudPainter.SaveImage(placesWords, appSettings.OutputPath, ImageFormat.Png);
    }
}
