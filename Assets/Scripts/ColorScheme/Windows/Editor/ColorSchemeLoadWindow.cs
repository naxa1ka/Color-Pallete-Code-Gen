using System.IO;
using UnityEngine;
using UnityEditor;

public class ColorSchemeLoadWindow : ColorSchemeBaseWindow
{
    private static readonly GUIContent
        LoadButton = new GUIContent("Load JSON", "Load JSON");
    
    [MenuItem("Color Scheme/Load color scheme from json")]
    private static void Init()
    {
        ColorSchemeLoadWindow saveWindow = (ColorSchemeLoadWindow) GetWindow(typeof(ColorSchemeLoadWindow));
        saveWindow.Show();
    }

    private new void OnGUI()
    {
        GUILayout.Label("Load color scheme from json", EditorStyles.boldLabel);

        base.OnGUI();

        if (GUILayout.Button(LoadButton))
        {
            Load();
        }

        GUI.enabled = true;
    }

    private void Load()
    {
        string json = "";
        using (var reader = new StreamReader(FilePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                json += line;
            }
        }

        JsonUtility.FromJsonOverwrite(json, SelectedColorScheme);
    }
}