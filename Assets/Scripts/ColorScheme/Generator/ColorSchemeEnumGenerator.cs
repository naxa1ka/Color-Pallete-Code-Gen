using System;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class ColorSchemeEnumGenerator
{
    private static string _colorSchemeEnumName = "ColorSchemeEnum";

    public static void NewEnum(string newColorEnum)
    {
        StringBuilder stringBuilder = new StringBuilder();

        PasteNewEnums(stringBuilder, newColorEnum);

        MainGenerate(stringBuilder);
    }

    public static void RemoveEnum(ColorSchemeEnum colorSchemeEnum)
    {
        StringBuilder stringBuilder = new StringBuilder();

        PasteEnumsWithExcluded(stringBuilder, colorSchemeEnum);

        MainGenerate(stringBuilder);
    }

    private static void PasteNewEnums(StringBuilder stringBuilder, string newColorEnum)
    {
        var enums = Enum.GetValues(typeof(ColorSchemeEnum));
        foreach (var colorEnum in enums)
        {
            Append(stringBuilder, colorEnum);
        }

        Append(stringBuilder, ColorSchemeBaseGenerator.FormatEnum(newColorEnum));
    }

    private static void PasteEnumsWithExcluded(StringBuilder stringBuilder, ColorSchemeEnum excludedElement)
    {
        var enums = Enum.GetValues(typeof(ColorSchemeEnum));
        foreach (var colorEnum in enums)
        {
            if (colorEnum.ToString() == excludedElement.ToString())
            {
                continue;
            }

            Append(stringBuilder, colorEnum);
        }
    }

    private static void Append(StringBuilder stringBuilder, object colorEnum)
    {
        stringBuilder.Append(colorEnum).Append(",");
    }

    private static void MainGenerate(StringBuilder stringBuilder)
    {
        string codePath = $"{ColorSchemeBaseGenerator.PathToCodeGen}{_colorSchemeEnumName}.cs";
        string path = $"{Application.dataPath}{codePath}";

        string code = $@"public enum {_colorSchemeEnumName} {{
                {
                    stringBuilder
                }
            }}";

        System.IO.File.WriteAllText(path, code);
        AssetDatabase.ImportAsset($"{ColorSchemeBaseGenerator.PathAssets}{codePath}");
    }
}