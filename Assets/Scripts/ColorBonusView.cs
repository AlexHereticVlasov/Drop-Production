using UnityEngine;

[RequireComponent(typeof(ColorBonus))]
public sealed class ColorBonusView : MonoBehaviour
{
    [SerializeField] private ColorBonus _colorBonus;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private ColorBean _bean;

    private void OnEnable() => _colorBonus.ColorChanged += OnColorChanged;

    private void OnDisable() => _colorBonus.ColorChanged -= OnColorChanged;

    private void OnColorChanged(BonusColor color) => _renderer.color = _bean[color];
}
