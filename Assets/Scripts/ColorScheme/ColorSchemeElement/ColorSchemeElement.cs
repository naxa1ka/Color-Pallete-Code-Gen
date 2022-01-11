using UnityEngine;

public abstract class ColorSchemeElement : MonoBehaviour
{
    [SerializeField] private ColorSchemeEnum _currentID;

    private void OnValidate()
    {
        UpdateColor();
    }

    protected Color GetColor()
    {
        return ColorSchemeProvider.Instance.GetColor(_currentID);
    }
    public abstract void UpdateColor();
}
