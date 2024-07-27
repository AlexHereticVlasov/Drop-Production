using UnityEngine.Events;

public sealed class ColorHandler
{
    private BonusColor _previous;
    private int _sameColorInRow = 0;

    public event UnityAction<BonusColor> ColorChanged;
    public event UnityAction ThreeInRow;

    public void ApplyColor(BonusColor color)
    {
        ColorChanged?.Invoke(color);

        if (color == _previous)
        {
            _sameColorInRow++;
            CheckIsThreeInRow();
            return;
        }

        _previous = color;
        _sameColorInRow = 1;
    }

    private void CheckIsThreeInRow()
    {
        if (_sameColorInRow == 3)
        {
            ThreeInRow?.Invoke();
            _previous = BonusColor.None;
            _sameColorInRow = 0;
        }
    }
}
