namespace TagsCloudContainer;

public class SettingsValidator
{
    public List<string> Validate(AppSettings settings)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(settings.WordsFilePath))
            errors.Add("Путь к файлу с текстом не может быть пустым.");
        else if (!File.Exists(settings.WordsFilePath))
            errors.Add("Входной файл не найден.");

        if (string.IsNullOrWhiteSpace(settings.OutputPath))
            errors.Add("Путь сохранения не может быть пустым.");

        if (settings.ImageSize.Width <= 100 || settings.ImageSize.Height <= 100)
            errors.Add("Размер изображения должен быть больше 100x100.");

        if (settings.MinFontSize <= 0)
            errors.Add("Минимальный размер шрифта должен быть больше 0.");

        if (settings.MaxFontSize <= settings.MinFontSize)
            errors.Add("Максимальный размер шрифта должен быть больше минимального.");

        if (settings.SpiralDensity <= 0)
            errors.Add("Плотность спирали должна быть положительным числом.");

        if (string.IsNullOrWhiteSpace(settings.FontName))
            errors.Add("Имя шрифта не указано.");

        return errors;
    }
}