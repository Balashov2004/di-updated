using FakeItEasy;
using TagsCloudContainer;
using TagsCloudVisualization.Interface;

namespace TagsCloudVisualizationTests;

public class TextProcessorTests
{
    private IWordsFilter fakeFilter;
    private IFileReader fakeReader;
    private AppSettings settings;

    [SetUp]
    public void SetUp()
    {
        settings = new AppSettings
        {
            MinFontSize = 10,
            MaxFontSize = 20
        };
        
        fakeFilter = A.Fake<IWordsFilter>();
        fakeReader = A.Fake<IFileReader>();
        A.CallTo(() => fakeFilter.ApplyFilter(A<List<string>>.Ignored))
            .ReturnsLazily((List<string> rawWords) => 
            {
                var filtered = rawWords.Select(w => w.ToLowerInvariant()).ToList();
                return Result<List<string>>.Success(filtered);
            });
    }
    
    [Test]
    public void Process_CalculatesCorrectFontSize_BasedOnFrequency()
    {
        var fakeFileContent = "cat cat cat dog dog apple";
        A.CallTo(() => fakeReader.ReadAllText(A<string>.Ignored)).Returns(fakeFileContent);
        var processor = new TextProcessor(settings, fakeFilter);
        processor.Process(fakeFileContent);
        
        var catData = processor.ProcessWords.Single(w => w.Word.Equals("cat"));
        var dogData = processor.ProcessWords.Single(w => w.Word.Equals("dog"));
        var appleData = processor.ProcessWords.Single(w => w.Word.Equals("apple"));
        
        Assert.That(appleData.FontSize, Is.EqualTo(10));
        Assert.That(dogData.FontSize, Is.EqualTo(15));
        Assert.That(catData.FontSize, Is.EqualTo(20));
    }

    [Test]
    public void Process_CalculatesCorrectFontSize_WhenSameFrequency()
    {
        var fakeFileContent = "cat dog apple";
        A.CallTo(() => fakeReader.ReadAllText(A<string>.Ignored)).Returns(fakeFileContent);
        var processor = new TextProcessor(settings, fakeFilter);
        processor.Process(fakeFileContent);
        
        var catData = processor.ProcessWords.Single(w => w.Word.Equals("cat"));
        var dogData = processor.ProcessWords.Single(w => w.Word.Equals("dog"));
        var appleData = processor.ProcessWords.Single(w => w.Word.Equals("apple"));
        
        Assert.That(appleData.FontSize, Is.EqualTo(15));
        Assert.That(dogData.FontSize, Is.EqualTo(15));
        Assert.That(catData.FontSize, Is.EqualTo(15));
    }
}