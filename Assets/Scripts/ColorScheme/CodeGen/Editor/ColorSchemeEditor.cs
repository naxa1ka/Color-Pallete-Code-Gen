using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColorScheme))]
public class ColorSchemeEditor : Editor
{
    private string _currentId;
    private ColorSchemeEnum _selectedEnum;

    private static readonly GUIContent
        NewButton = new GUIContent("New ID", "New ID"),
        RemoveButton = new GUIContent("Delete ID", "Delete ID"),
        UpdateButton = new GUIContent("Update elements", "Update elements");

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        serializedObject.Update();

        EditorGUILayout.Space();
        
        _selectedEnum = (ColorSchemeEnum) EditorGUILayout.EnumPopup("Selected ID", _selectedEnum);
        _currentId = EditorGUILayout.TextField("New ID", _currentId);

        EditorGUILayout.Space();

        bool isValidEnum = ColorSchemeBaseGenerator.IsValidEnum(_currentId);
        if (isValidEnum == false)
        {
            EditorGUILayout.HelpBox("This enum isn't correct", MessageType.Error);
        }

        bool isExistEnum = true;
        if (isValidEnum)
        {
            isExistEnum = ColorSchemeBaseGenerator.IsExistEnum(_currentId);
            if (isExistEnum)
            {
                EditorGUILayout.HelpBox("This enum is existing now!", MessageType.Error);
            }
        }

        GUI.enabled = !isExistEnum;
        if (GUILayout.Button(NewButton))
        {
            ColorSchemeBaseGenerator.GenerateNew(_currentId);
        }

        GUI.enabled = isExistEnum;

        if (GUILayout.Button(RemoveButton))
        {
            ColorSchemeBaseGenerator.RemoveAndGenerate(_selectedEnum);
        }

        GUI.enabled = true;

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        if (GUILayout.Button(UpdateButton))
        {
            ColorSchemeProvider.Instance.UpdateElements();
        }
    }
}