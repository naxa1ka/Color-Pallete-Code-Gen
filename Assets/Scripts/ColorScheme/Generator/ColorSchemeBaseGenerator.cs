using System;

public static class ColorSchemeBaseGenerator
{
    public const string PathAssets = "Assets";
    public const string PathToCodeGen = "/Scripts/ColorScheme/CodeGen/";
    
    public static void GenerateNew(string currentId)
    {
        ColorSchemeEnumGenerator.NewEnum(currentId);
        ColorSchemeGenerator.NewColorScheme(currentId);
    }

    public static void RemoveAndGenerate(ColorSchemeEnum selectedEnum)
    {
        ColorSchemeEnumGenerator.RemoveEnum(selectedEnum);
        ColorSchemeGenerator.RemoveColorScheme(selectedEnum);
    }
    
    public static string FormatEnum(string str)
    {
        return str.Replace(" ", "_").ToUpper();
    }

    public static bool IsValidEnum(string str)
    {
        if (str == null)
        {
            return false;
        }

        if (str.Equals(""))
        {
            return false;
        }

        var charArray = str.ToCharArray();
        foreach (var c in charArray)
        {
            if (c < 'a' && 'z' > c)
            {
                return false;
            }
        }

        return true;
    }

    public static bool IsExistEnum(string str)
    {
        var formattedString = FormatEnum(str);
        var enums = Enum.GetValues(typeof(ColorSchemeEnum));

        foreach (var colorEnum in enums)
        {
            if (colorEnum.ToString() == formattedString)
            {
                return true;
            }
        }

        return false;
    }
}