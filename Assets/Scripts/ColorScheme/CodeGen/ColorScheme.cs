using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ColorScheme", menuName = "ColorScheme", order = 0)]
 public class ColorScheme : ScriptableObject, IColorSchemeProvider {
                public Color BUY;public Color LIGHT;public Color GRAY;public Color CANCEL;
public Color GetColor(ColorSchemeEnum colorSchemeEnum)
        {
            return colorSchemeEnum switch
            {ColorSchemeEnum.BUY => BUY,
ColorSchemeEnum.LIGHT => LIGHT,
ColorSchemeEnum.GRAY => GRAY,
ColorSchemeEnum.CANCEL => CANCEL,
            _ => throw new ArgumentOutOfRangeException(nameof(colorSchemeEnum), colorSchemeEnum, null)
        };
    }
            }