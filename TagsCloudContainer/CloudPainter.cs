using System.Drawing;
using System.Drawing.Imaging;


namespace TagsCloudContainer;

public class CloudPainter
{
    private readonly AppSettings? appSettings;
    
    public CloudPainter(AppSettings appSettings)
    {
        this.appSettings = appSettings;
    }

    private Bitmap DrawCloud(List<WordData> placedWords)
    {
        var bitmap = new Bitmap(appSettings.ImageSize.Width, appSettings.ImageSize.Height);
        using var graphics = Graphics.FromImage(bitmap);
        using var wordBrush = new SolidBrush(appSettings.WordColor);
        using var contourPen = new Pen(appSettings.ContourColor, 1);
    
        graphics.Clear(appSettings.BackgroundColor);

        foreach (var data in placedWords)
        {
            var rect = data.Placement; 
            
            graphics.DrawString(data.Word, data.WordFont, wordBrush, rect.Location);
            graphics.DrawRectangle(contourPen, rect); 
        }
    
        return bitmap;
    }
    
    public void SaveImage(List<WordData> placesWords, string path, ImageFormat format)
    {
        using var bitmap = DrawCloud(placesWords);
        bitmap.Save(path, format);
    }
}