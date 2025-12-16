using Autofac;
using System.Drawing;
using TagsCloudContainer;
using TagsCloudContainer.FileReaders;
using TagsCloudVisualization.Interface;

namespace TagsCloudVisualization;

public class CompositionRoot : Module
{
    protected override void Load(ContainerBuilder builder)
    {

        builder.RegisterType<AppSettings>().AsSelf();
        builder.RegisterType<CircularCloudLayouter>().AsSelf();
        builder.RegisterType<TextProcessor>().AsSelf();
        builder.RegisterType<CloudPainter>().AsSelf();
        builder.RegisterType<CloudRunner>().AsSelf();
        builder.RegisterType<TxtFileReader>().Keyed<IFileReader>(".txt");
        builder.RegisterType<DocxFileReader>().Keyed<IFileReader>(".docx");
        builder.RegisterType<FileCoordinator>().As<IFileReader>();
        builder.RegisterType<WordsFilter>().As<IWordsFilter>().SingleInstance();
        
        builder.Register<SpiralPointGenerator>(ctx =>
        {
            var settings = ctx.Resolve<AppSettings>();
            return new SpiralPointGenerator(settings.GetCenter(), settings.SpiralDensity, settings.Angle);
        }).Named<IPointGenerator>(GeneratorType.Spiral.ToString());
        
        builder.Register(ctx =>
        {
            var settings = ctx.Resolve<AppSettings>();
            var key = settings.PointGeneratorType.ToString(); 
            return ctx.ResolveNamed<IPointGenerator>(key);
        }).As<IPointGenerator>();
    }
}

internal static class AppSettingsExtensions
{
    public static Point GetCenter(this AppSettings settings)
    {
        return new Point(settings.ImageSize.Width / 2, settings.ImageSize.Height / 2);
    }
}