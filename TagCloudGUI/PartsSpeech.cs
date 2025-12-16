namespace TagsCloudContainer.DTO;

public class PartsSpeech
{
    public static readonly Dictionary<string, string> RussianToTag = new Dictionary<string, string>
    {
        {"Предлог", "PR"},
        {"Союз", "CONJ"},
        {"Частица", "PART"},
        {"Местоимение-существительное", "SPRO"},
        {"Местоимение-прилагательное", "APRO"},
        {"Местоимение-наречие", "ADVPRO"},
        {"Междометие", "INTJ"},
        {"Наречие", "ADVB"},
        {"Числительное", "NUM"},
        {"Существительное", "S"},
        {"Глагол", "V"},
        {"Прилагательное", "A"}
    };
    
    public static readonly Dictionary<string, string> ToTags = 
        RussianToTag.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
}