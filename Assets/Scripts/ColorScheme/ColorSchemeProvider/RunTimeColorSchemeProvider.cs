using UnityEngine;

public class RunTimeColorSchemeProvider : MonoBehaviour, IColorSchemeProvider
{
    [SerializeField] private ColorScheme _runtimeColorScheme;

    public Color GetColor(ColorSchemeEnum colorSchemeEnum)
    {
        return _runtimeColorScheme.GetColor(colorSchemeEnum);
    }

    public void Install(string json)
    {
        JsonUtility.FromJsonOverwrite(json, _runtimeColorScheme);

        ColorSchemeProvider.Instance.OtherSchemeProvider = _runtimeColorScheme;
        ColorSchemeProvider.Instance.UpdateElements();
    }
}