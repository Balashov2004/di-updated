using FakeItEasy;
using TagsCloudContainer;
using TagsCloudVisualization.Interface;

namespace TagsCloudVisualizationTests;

public class TextProcessorTests
{
    
    [Test]
    public void Process_CalculatesCorrectFontSize_BasedOnFrequency()
    {
        var settings = new AppSettings(
            minFontSize: 10, 
            maxFontSize: 20
        );
        var fakeFileContent = "cat cat cat dog dog apple";
        var fakeReader = A.Fake<IFileReader>();
        A.CallTo(() => fakeReader.ReadAllText(A<string>.Ignored)).Returns(fakeFileContent);
        var processor = new TextProcessor(settings, fakeReader);
        
        processor.Process();
        var catData = processor.ProcessWords.Single(w => w.Word.Equals("cat"));
        var dogData = processor.ProcessWords.Single(w => w.Word.Equals("dog"));
        var appleData = processor.ProcessWords.Single(w => w.Word.Equals("apple"));
        
        Assert.That(appleData.WordFont.Size, Is.EqualTo(10));
        Assert.That(dogData.WordFont.Size, Is.EqualTo(15));
        Assert.That(catData.WordFont.Size, Is.EqualTo(20));
    }

    [Test]
    public void Process_CalculatesCorrectFontSize_WhenDameDensity()
    {
        var settings = new AppSettings(
            minFontSize: 10, 
            maxFontSize: 20
        );
        var fakeFileContent = "cat dog apple";
        var fakeReader = A.Fake<IFileReader>();
        A.CallTo(() => fakeReader.ReadAllText(A<string>.Ignored)).Returns(fakeFileContent);
        var processor = new TextProcessor(settings, fakeReader);
        
        processor.Process();
        var catData = processor.ProcessWords.Single(w => w.Word.Equals("cat"));
        var dogData = processor.ProcessWords.Single(w => w.Word.Equals("dog"));
        var appleData = processor.ProcessWords.Single(w => w.Word.Equals("apple"));
        
        Assert.That(appleData.WordFont.Size, Is.EqualTo(15));
        Assert.That(dogData.WordFont.Size, Is.EqualTo(15));
        Assert.That(catData.WordFont.Size, Is.EqualTo(15));
    }
    
}