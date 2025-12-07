using Autofac;
using System.Drawing;
using TagsCloudContainer;
using TagsCloudVisualization.Interface;

namespace TagsCloudVisualization;

public class CompositionRoot : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AppSettings>().SingleInstance();
        builder.RegisterType<CircularCloudLayouter>();
        builder.RegisterType<TextProcessor>();
        builder.RegisterType<CloudRunner>();

        builder.RegisterType<FileReader>().Named<IFileReader>(nameof(ReaderType.FileTxt));
        builder.Register(ctx =>
        {
            var settings = ctx.Resolve<AppSettings>();
            var key = settings.ReaderType.ToString();

            return ctx.ResolveNamed<IFileReader>(key);
        }).As<IFileReader>();

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