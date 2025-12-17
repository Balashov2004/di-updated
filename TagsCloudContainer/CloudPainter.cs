using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudContainer.DTO;


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
        var minX = placedWords.Min(w => w.Placement.Left);
        var maxX = placedWords.Max(w => w.Placement.Right);
        var minY = placedWords.Min(w => w.Placement.Top);
        var maxY = placedWords.Max(w => w.Placement.Bottom);
        var width = Math.Max(appSettings.ImageSize.Width, (maxX - minX) + 40);
        var height = Math.Max(appSettings.ImageSize.Height, (maxY - minY) + 40);
        
        var bitmap = new Bitmap(width, height);
        using var graphics = Graphics.FromImage(bitmap);
        using var wordBrush = new SolidBrush(appSettings.WordColor);
        using var contourPen = new Pen(appSettings.ContourColor, 1);
        if (width != appSettings.ImageSize.Width && height != appSettings.ImageSize.Height)
            graphics.TranslateTransform(-minX + 40, -minY + 40);
        
        graphics.Clear(appSettings.BackgroundColor);
        
        foreach (var data in placedWords)
        {
            var rect = data.Placement; 
            using var font = new Font(appSettings.FontName, data.FontSize);
            graphics.DrawString(data.Word, font, wordBrush, rect.Location);
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