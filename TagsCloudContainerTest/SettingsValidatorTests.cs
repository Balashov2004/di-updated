using FluentAssertions;

using System.Drawing;
using TagsCloudContainer;

namespace TagsCloudVisualizationTests;

public class SettingsValidatorTests
{
    private SettingsValidator validator;
    private AppSettings settings;

    [SetUp]
    public void SetUp()
    {
        validator = new SettingsValidator();
        settings = new AppSettings
        {
            WordsFilePath = "test.txt",
            OutputPath = "output.png",
            ImageSize = new Size(800, 600),
            MinFontSize = 10,
            MaxFontSize = 50,
            FontName = "Arial",
            SpiralDensity = 1.0
        };
        
        File.WriteAllText("test.txt", "content");
    }
    
    [TearDown]
    public void TearDown()
    {
        if (File.Exists("test.txt")) File.Delete("test.txt");
    }

    [Test]
    public void WhenSettingsAreCorrect_Test()
    {
        var errors = validator.Validate(settings);
        errors.Should().BeEmpty();
    }

    [Test]
    public void WhenFileDoesNotExist_Test()
    {
        settings.WordsFilePath = "non_existent.txt";
        var errors = validator.Validate(settings);
        errors.Should().Contain(e => e.Contains("файл не найден"));
    }

    [Test]
    public void MaxFontSizeLessThanMin_Test()
    {
        settings.MinFontSize = 40;
        settings.MaxFontSize = 20;

        var errors = validator.Validate(settings);
        errors.Should().Contain(e => e.Contains("Максимальный размер шрифта должен быть больше минимального"));
    }

    [TestCase(0)]
    [TestCase(-5)]
    public void ImageSizeIsSmall_Test(int size)
    {
        settings.ImageSize = new Size(size, size);
        var errors = validator.Validate(settings);
        errors.Should().Contain(e => e.Contains("больше 100x100"));
    }

    [Test]
    public void FontNameIsEmpty_Test()
    {
        settings.FontName = "";
        var errors = validator.Validate(settings);
        errors.Should().Contain("Имя шрифта не указано.");
    }
}