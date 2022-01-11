using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColorSchemeElement), true)]
public class ColorSchemeElementEditor : Editor
{
    private static readonly GUIContent
        UpdateButton = new GUIContent("Update elements", "Update elements");
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();
        
        EditorGUILayout.Space();
        
        var enumField = serializedObject.FindProperty("_currentID");
        EditorGUILayout.ColorField("Current color",
            ColorSchemeProvider.Instance.GetColor((ColorSchemeEnum) enumField.enumValueIndex));

        EditorGUI.BeginChangeCheck();
        ColorSchemeProvider.Instance.ColorScheme =
            (ColorScheme) EditorGUILayout.ObjectField("Current Color Scheme", ColorSchemeProvider.Instance.ColorScheme, typeof(ColorScheme), false);

        if (EditorGUI.EndChangeCheck())
        {
            ColorSchemeElement colorScheme = (ColorSchemeElement)target; 
            colorScheme.UpdateColor();
            ColorSchemeProvider.Instance.UpdateElements();
        }
        
        if (GUILayout.Button(UpdateButton))
        {
            ColorSchemeProvider.Instance.UpdateElements();
        }

        
        serializedObject.ApplyModifiedProperties();
    }
}
