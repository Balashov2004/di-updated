
using Autofac;
using TagsCloudContainer;
using TagsCloudVisualization;


public class Program
{
    public static void Main(string[] args)
    {
        var appSettings = new AppSettings
        {
            OutputPath = "./results/result5.png",
            WordsFilePath = "./resurses/words3.txt",
            ReaderType = ReaderType.FileTxt,
            PointGeneratorType = GeneratorType.Spiral
        };
        var builder = new ContainerBuilder();
        builder.RegisterModule(new CompositionRoot());
        builder.RegisterInstance(appSettings).AsSelf().SingleInstance();
        var container = builder.Build();
        var runner = container.Resolve<CloudRunner>();
        runner.Run();
    }
}