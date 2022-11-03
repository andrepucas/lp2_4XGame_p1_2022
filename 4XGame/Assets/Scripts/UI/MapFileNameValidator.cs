using System.Text.RegularExpressions;

public static class MapFileNameValidator
{
    // Readonly
    private static readonly Regex ILLEGAL_CHARS = 
        new Regex("[#%&{}\\<>*?/$!'\":@+`|= ]");

    public static string Validate(string p_fileName) => 
        ILLEGAL_CHARS.Replace(p_fileName, "_");
}
