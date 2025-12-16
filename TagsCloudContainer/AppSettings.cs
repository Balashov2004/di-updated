using System.Drawing;


namespace TagsCloudContainer;
public enum GeneratorType { Spiral }

public class AppSettings
{
    public Size ImageSize { get; set; } = new Size(1500, 1500);
    public string FontName { get; set; } = "Times New Roman";
    public int MinFontSize { get; set; } = 10;
    public int MaxFontSize { get; set; } = 48;
    public int Padding { get; set; } = 2;
    public double SpiralDensity { get; set; } = 0.1;
    public string OutputPath { get; set; } = 
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop), 
            "TagCloudResults", 
            "result.png"
        );
    public string WordsFilePath { get; set; } = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        );
    public Color BackgroundColor { get; set; } = Color.White;
    public Color WordColor { get; set; } = Color.BurlyWood;
    public Color ContourColor { get; set; } = Color.Black;
    public double Angle { get; set; } = 1.0;
    public GeneratorType PointGeneratorType { get; set; } = GeneratorType.Spiral;
    public List<string> ExcludePartsSpeech { get; set; } = new List<string> 
    { 
        "PR", "CONJ", "PART", "SPRO", "APRO", "ADVPRO", "INTJ", "ADVB", "NUM"
    };
}
