using System.Drawing;
using TagsCloudVisualization.Interface;

namespace TagsCloudContainer;

public class WordData
{
    public string Word { get; }
    public int FontSize { get; }
    public Size Size { get; }
    
    public Rectangle Placement { get; private set; }
    
    public WordData(string word, int fontSize, Size size)
    {
        Word = word;
        FontSize = fontSize;
        Size = size;
        Placement = new Rectangle(Point.Empty, size);
    }
    
    public void SetPlacement(Rectangle placementRectangle)
    {
        Placement = placementRectangle;
    }
}