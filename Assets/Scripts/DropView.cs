using UnityEngine;

public sealed class DropView
{
    private SpriteRenderer _renderer;
    private ColorBean _bean;
    private ColorHandler _colorHandler;

    public DropView(SpriteRenderer renderer, ColorBean bean, ColorHandler colorHandler)
    {
        _renderer = renderer;
        _bean = bean;
        _colorHandler = colorHandler;
        _colorHandler.ColorChanged += OnColorChanged;
    }
    public void Deinitialize() => _colorHandler.ColorChanged -= OnColorChanged;

    private void OnColorChanged(BonusColor newColor) => _renderer.color = _bean[newColor];

}