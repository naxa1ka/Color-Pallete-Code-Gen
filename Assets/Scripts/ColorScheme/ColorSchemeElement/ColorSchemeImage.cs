using UnityEngine;
using UnityEngine.UI;

public class ColorSchemeImage : ColorSchemeElement
{
    [SerializeField] private Image _image;

    private void Start()
    {
        UpdateColor();
    }

    public override void UpdateColor()
    {
        _image.color = GetColor();
    }
}