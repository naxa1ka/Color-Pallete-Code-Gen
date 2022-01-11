using UnityEditor;
using UnityEngine;

public class ColorSchemeBaseWindow : EditorWindow
{
    private static readonly GUIContent
        OpenFileButton = new GUIContent("Open File", "Open File");

    protected string FilePath = "empty";
    protected ColorScheme SelectedColorScheme;

    public void OnGUI()
    {
        SelectedColorScheme =
            (ColorScheme) EditorGUILayout.ObjectField("Color scheme", SelectedColorScheme, typeof(ColorScheme),
                false);
        EditorGUILayout.LabelField("File path", FilePath);

        if (GUILayout.Button(OpenFileButton))
        {
            FilePath = EditorUtility.OpenFilePanel("Color scheme", "", "json");
        }

        if (FilePath.Equals("empty"))
        {
            EditorGUILayout.HelpBox("File is empty!", MessageType.Error);
        }

        if (SelectedColorScheme == null)
        {
            EditorGUILayout.HelpBox("Object is empty!", MessageType.Error);
        }

        GUI.enabled = !FilePath.Equals("empty") && SelectedColorScheme != null;
    }
}