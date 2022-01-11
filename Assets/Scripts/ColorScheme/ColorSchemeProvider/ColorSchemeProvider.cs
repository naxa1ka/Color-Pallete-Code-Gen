using UnityEngine;
using Color = UnityEngine.Color;

[ExecuteAlways]
public class ColorSchemeProvider : MonoBehaviour, IColorSchemeProvider
{
    [SerializeField] private ColorScheme _currentColorScheme;
    
    private static ColorSchemeProvider _instance;

    public ColorScheme OtherSchemeProvider { get; set; }

    private IColorSchemeProvider SchemeProvider
    {
        get
        {
            if (OtherSchemeProvider == null)
            {
                return _currentColorScheme;
            }

            return OtherSchemeProvider;
        }
    }

    public ColorScheme ColorScheme
    {
        get => _currentColorScheme;
        set
        {
            if (value == null) return;
            _currentColorScheme = value;
        }
    }

    public static ColorSchemeProvider Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<ColorSchemeProvider>();

            return _instance;
        }
    }

    public Color GetColor(ColorSchemeEnum schemeEnum)
    {
        return SchemeProvider.GetColor(schemeEnum);
    }

    public void UpdateElements()
    {
        foreach (var colorSchemeElement in FindObjectsOfType<ColorSchemeElement>())
        {
            colorSchemeElement.UpdateColor();
        }
    }
}