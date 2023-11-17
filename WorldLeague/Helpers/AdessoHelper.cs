namespace WorldLeague.Helpers;

public class AdessoHelper
{
    public static string TeamName => "Adesso ";

    public static List<char> GetGroup()
    {
        return new List<char> {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'};
    }

    public static List<string> GetCountry()
    {
        return new List<string>
        {
            "Türkiye", "Almanya", "Fransa", "Hollanda",
            "Portekiz", "İtalya", "İspanya", "Belçika"
        };
    }
}