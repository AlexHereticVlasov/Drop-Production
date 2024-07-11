using UnityEngine;

[CreateAssetMenu(fileName = nameof(ColorBean), menuName = nameof(ScriptableObject) + " / " + nameof(ColorBean))]
public sealed class ColorBean : ScriptableObject
{
    [SerializeField] private Color[] _colors;

    public Color this[BonusColor color] => _colors[(int)color - 1];
}
