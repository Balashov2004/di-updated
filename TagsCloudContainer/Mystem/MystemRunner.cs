using System.Diagnostics;
using System.Text;
using TagsCloudVisualization.Interface;

namespace TagsCloudContainer.Mystem;

public class MystemRunner : IMystemRunner
{
    private const string MystemPath = "./Mystem/mystem.exe";

    public string GetAnalysisJson(string input)
    {
        if (!File.Exists(MystemPath))
            throw new FileNotFoundException($"Mystem не найден: {MystemPath}");

        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = MystemPath,
                Arguments = "-i --eng-gr --format json",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardInputEncoding = Encoding.UTF8,
                StandardOutputEncoding = Encoding.UTF8
            }
        };
        process.Start();
        process.StandardInput.WriteLine(input);
        process.StandardInput.Close();
        return process.StandardOutput.ReadToEnd();
    }
}