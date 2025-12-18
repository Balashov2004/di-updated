using System.Drawing;
using FluentAssertions;
using FakeItEasy;
using TagsCloudContainer;
using TagsCloudVisualization.Interface;


namespace TagsCloudVisualizationTests;

[TestFixture]
public class CircularCloudLayouterTests
{
    private AppSettings defaultSettings;
    private Point center;

    [SetUp]
    public void SetUp()
    {
        center = new Point(500, 500);
        defaultSettings = new AppSettings();
        defaultSettings.Padding = 0;
        defaultSettings.ImageSize = new Size(1000, 1000);
    }


    [Test]
    public void FirstRectangleInCenter_Test()
    {
        var customCenter = new Point(100, 60);
        var rectangleSize = new Size(20, 10);
        var fakeGenerator = A.Fake<IPointGenerator>();
        A.CallTo(() => fakeGenerator.Center).Returns(customCenter);
        var layouter = new CircularCloudLayouter(defaultSettings, fakeGenerator);

        var rectangle = layouter.PutNextRectangle(rectangleSize);
        var expectedX = customCenter.X - rectangleSize.Width / 2;
        var expectedY = customCenter.Y - rectangleSize.Height / 2;

        Assert.That(rectangle.X, Is.EqualTo(expectedX));
        Assert.That(rectangle.Y, Is.EqualTo(expectedY));
        Assert.That(rectangle.Width, Is.EqualTo(rectangleSize.Width));
        Assert.That(rectangle.Height, Is.EqualTo(rectangleSize.Height));
    }


    [Test]
    public void PutNextRectangle_NoIntersection_Test()
    {
        var settingsWithPadding = new AppSettings();
        settingsWithPadding.Padding = 5;
        var generator = new SpiralPointGenerator(center, settingsWithPadding.SpiralDensity, 1);
        var fakeGenerator = A.Fake<IPointGenerator>();
        A.CallTo(() => fakeGenerator.GeneratePoints()).Returns(generator.GeneratePoints());
        A.CallTo(() => fakeGenerator.Center).Returns(center);

        var layouter = new CircularCloudLayouter(settingsWithPadding, fakeGenerator);

        var random = new Random();
        var rectangles = new List<Rectangle>();
        var count = 50;

        for (int i = 0; i < count; i++)
        {
            var size = new Size(random.Next(10, 50), random.Next(10, 50));
            var rect = layouter.PutNextRectangle(size);
            rectangles.Add(rect);
        }

        for (int i = 0; i < rectangles.Count; i++)
        {
            for (int j = i + 1; j < rectangles.Count; j++)
            {
                var rect1 = rectangles[i];
                var rect2 = rectangles[j];

                Assert.That(rect1.IntersectsWith(rect2), Is.False,
                    $"Прямоугольники пересеклись.");
            }
        }
    }


    [Test]
    public void RectangleNotFit_Test()
    {
        var imageSize = new Size(100, 100);
        var appSettings = new AppSettings { ImageSize = imageSize };
        var generator = new SpiralPointGenerator(new Point(50, 50), 1.0, 0.1);
        var layouter = new CircularCloudLayouter(appSettings, generator);
        var largeSize = new Size(200, 200);

        var rect = layouter.PutNextRectangle(largeSize);
        rect.Size.Should().Be(largeSize);
    }
    
}