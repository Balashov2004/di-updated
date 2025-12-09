using System.Drawing.Imaging;
using TagsCloudVisualization.Interface;


namespace TagsCloudContainer;

public class CloudRunner
{
    
    private readonly CircularCloudLayouter layouter;
    private readonly TextProcessor textProcessor;
    private readonly AppSettings appSettings;
    private readonly CloudPainter cloudPainter;
    private readonly IFileReader fileReader;
    
    public CloudRunner(
        AppSettings appSettings, 
        CircularCloudLayouter layouter, 
        TextProcessor textProcessor,
        CloudPainter cloudPainter,
        IFileReader fileReader)
    {
        this.appSettings = appSettings;
        this.layouter = layouter;
        this.textProcessor = textProcessor;
        this.cloudPainter = cloudPainter;
        this.fileReader = fileReader;
    }

    public void Run()
    {
        var text = fileReader.ReadAllText(appSettings.WordsFilePath);
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
