using System.IO;
using UnityEngine;
using UnityEditor;

public class ColorSchemeSaveWindow : ColorSchemeBaseWindow
{
    private static readonly GUIContent
        SaveButton = new GUIContent("Save JSON", "Save JSON");

    [MenuItem("Color Scheme/Save color scheme to json")]
    private static void Init()
    {
        ColorSchemeSaveWindow saveWindow = (ColorSchemeSaveWindow) GetWindow(typeof(ColorSchemeSaveWindow));
        saveWindow.Show();
    }

    private new void OnGUI()
    {
        GUILayout.Label("Save color scheme to json", EditorStyles.boldLabel);
        
        base.OnGUI();

        if (GUILayout.Button(SaveButton))
        {
            Save();
        }

        GUI.enabled = true;
    }

    private void Save()
    {
        var json = JsonUtility.ToJson(SelectedColorScheme);
        using var writer = new StreamWriter(FilePath);
        
        writer.Write(json);
    }
}