using UnityEngine;

public interface IColorSchemeProvider
{
    public Color GetColor(ColorSchemeEnum colorSchemeEnum);
}