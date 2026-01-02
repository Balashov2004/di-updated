using System.Diagnostics;
using System.Text;
using TagsCloudVisualization.Interface;

namespace TagsCloudContainer.Mystem;

public class MystemRunner : IMystemRunner
{
    private const string MystemPath = "./Mystem/mystem.exe";

    public Result<string> GetAnalysisJson(string input)
    {
        if (!File.Exists(MystemPath))
            return Result<string>.Failure($"MyStem не найден");

        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = MystemPath,
                Arguments = "-i --eng-gr --format json",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardInputEncoding = Encoding.UTF8,
                StandardOutputEncoding = Encoding.UTF8
            }
        };
        process.Start();
        process.StandardInput.WriteLine(input);
        process.StandardInput.Close();
        var output = process.StandardOutput.ReadToEnd();
        var error = process.StandardError.ReadToEnd();
        process.WaitForExit();
        if (process.ExitCode != 0)
            return Result<string>.Failure($"MyStem завершился с ошибкой: {error}");

        return Result<string>.Success(output);
    }
}