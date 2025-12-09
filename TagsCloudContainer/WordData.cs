using System.Drawing;
using TagsCloudVisualization.Interface;

namespace TagsCloudContainer;

public class WordData
{
    public string Word { get; }
    public Font WordFont { get; }
    public Size Size { get; }
    
    public Rectangle Placement { get; private set; }
    
    public WordData(string word, Font font, Size size)
    {
        Word = word;
        WordFont = font;
        Size = size;
        Placement = new Rectangle(Point.Empty, size);
    }
    
    public void SetPlacement(Rectangle placementRectangle)
    {
        Placement = placementRectangle;
    }
}