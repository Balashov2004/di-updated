using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;
using TagsCloudContainer.DTO;
using TagsCloudVisualization.Interface;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class WordsFilterTests
{
    private IMystemRunner fakeRunner;
    private IMystemParser fakeParser;
    private AppSettings settings;

    [SetUp]
    public void SetUp()
    {
        fakeRunner = A.Fake<IMystemRunner>();
        fakeParser = A.Fake<IMystemParser>();
        settings = new AppSettings();
    }

    [Test]
    public void ExcludedParts_Test()
    {
        settings.ExcludePartsSpeech = new List<string> { "S" };
        var words = new List<string> { "бежать", "дом" };
        var fakeAnalysis = new List<TextMystem>
        {
            new TextMystem 
            { 
                WordAnalyses = new List<WordAnalysis> { new WordAnalysis { Grammar = "V,ipf,intr" } } 
            },
            new TextMystem 
            { 
                WordAnalyses = new List<WordAnalysis> { new WordAnalysis { Grammar = "S,m,inan" } } 
            }
        };

        A.CallTo(() => fakeRunner.GetAnalysisJson(A<string>._)).Returns("fake_json");
        A.CallTo(() => fakeParser.Parse("fake_json")).Returns(fakeAnalysis);

        var filter = new WordsFilter(settings, fakeRunner, fakeParser);
        var result = filter.ApplyFilter(words);
        result.Should().HaveCount(1);
        result.Should().Contain("бежать");
        result.Should().NotContain("дом");
    }

    [Test]
    public void WordsWithoutAnalysis_Test()
    {
        var words = new List<string> { "ааа" };
        var fakeAnalysis = new List<TextMystem> 
        { 
            new TextMystem { WordAnalyses = new List<WordAnalysis>() } 
        };

        A.CallTo(() => fakeParser.Parse(A<string>._)).Returns(fakeAnalysis);
        var filter = new WordsFilter(settings, fakeRunner, fakeParser);
        var result = filter.ApplyFilter(words);
        
        result.Should().Contain("ааа");
    }

    [Test]
    public void LowercaseWords_Test()
    {
        var words = new List<string> { "СОБАКА" };
        A.CallTo(() => fakeParser.Parse(A<string>._)).Returns(new List<TextMystem>());
        var filter = new WordsFilter(settings, fakeRunner, fakeParser);
        filter.ApplyFilter(words);

        A.CallTo(() => fakeRunner.GetAnalysisJson("собака")).MustHaveHappened();
    }
}