using System;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class ColorSchemeGenerator
{
    private static string _colorSchemeName = "ColorScheme";

    public static void NewColorScheme(string newColorEnum)
    {
        MainGenerate(NewFields(newColorEnum), NewSwitch(newColorEnum));
    }

    public static void RemoveColorScheme(ColorSchemeEnum colorSchemeEnum)
    {
        MainGenerate(RemoveFields(colorSchemeEnum), RemoveSwitch(colorSchemeEnum));
    }
    
    private static void MainGenerate(StringBuilder additionalInfo, StringBuilder switchAdditionalInfo)
    {
        string codePath = $"{ColorSchemeBaseGenerator.PathToCodeGen}{_colorSchemeName}.cs";
        string path = $"{Application.dataPath}{codePath}";

        string usingStr = "using UnityEngine;\nusing System;\n\n";
        string assetStr = "[CreateAssetMenu(fileName = \"ColorScheme\", menuName = \"ColorScheme\", order = 0)]\n";

        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append(additionalInfo);

        string switchStartStr = "\n" + @"public Color GetColor(ColorSchemeEnum colorSchemeEnum)
        {
            return colorSchemeEnum switch
            {";
        string swicthEndStr =
            @"            _ => throw new ArgumentOutOfRangeException(nameof(colorSchemeEnum), colorSchemeEnum, null)
        };
    }";


        string code = usingStr + assetStr + $@" public class ColorScheme : ScriptableObject, IColorSchemeProvider {{
                {
                    stringBuilder + switchStartStr + switchAdditionalInfo + swicthEndStr
                }
            }}";

        System.IO.File.WriteAllText(path, code);
        AssetDatabase.ImportAsset($"{ColorSchemeBaseGenerator.PathAssets}{codePath}");
    }

    private static StringBuilder NewFields(string str)
    {
        return New(str, AppendFields);
    }
    
    private static StringBuilder NewSwitch(string str)
    {
        return New(str, AppendSwitch);
    }
    
    private static StringBuilder New(string str, Append append)
    {
        StringBuilder stringBuilder = new StringBuilder();

        var values = Enum.GetValues(typeof(ColorSchemeEnum));
        foreach (var value in values)
        {
            append.Invoke(stringBuilder, value);
        }

        append.Invoke(stringBuilder, ColorSchemeBaseGenerator.FormatEnum(str));

        return stringBuilder;
    }


    private static StringBuilder RemoveFields(ColorSchemeEnum str)
    {
        return Remove(str, AppendFields);
    }

    private static StringBuilder RemoveSwitch(ColorSchemeEnum str)
    {
        return Remove(str, AppendSwitch);
    }

    private static StringBuilder Remove(ColorSchemeEnum str, Append append)
    {
        StringBuilder stringBuilder = new StringBuilder();

        var values = Enum.GetValues(typeof(ColorSchemeEnum));
        foreach (var value in values)
        {
            if (value.ToString() == str.ToString())
            {
                continue;
            }

            append.Invoke(stringBuilder, value);
        }

        return stringBuilder;
    }


    private delegate void Append(StringBuilder stringBuilder, object value);

    private static void AppendFields(StringBuilder stringBuilder, object value)
    {
        stringBuilder.Append($"public Color {value};");
    }

    private static void AppendSwitch(StringBuilder stringBuilder, object value)
    {
        stringBuilder.Append($"ColorSchemeEnum.{value} => {value},\n");
    }
}