using FluentAssertions;
using TagsCloudContainer.Mystem;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class MystemParserTests
{
    private MystemParser parser;

    [SetUp]
    public void SetUp() => parser = new MystemParser();

    [Test]
    public void JsonIsInvalid_Test()
    {
        var result = parser.Parse("[]");
        result.Should().BeEmpty();
    }

    [Test]
    public void PStandardMystemOutput_Test()
    {
        var json = "[{\"analysis\":[{\"lex\":\"кот\",\"gr\":\"S,m,anim=acc,sg\"}],\"text\":\"кота\"}]";
        
        var result = parser.Parse(json);

        result.Should().HaveCount(1);
        result[0].WordAnalyses[0].Grammar.Should().Be("S,m,anim=acc,sg");
    }
}