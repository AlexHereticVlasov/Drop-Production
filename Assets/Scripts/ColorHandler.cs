using UnityEngine.Events;

public sealed class ColorHandler
{
    private BonusColor _previous;
    private int _sameColorInRow = 0;

    public event UnityAction ThreeInRow;

    public void ApplyColor(BonusColor color)
    {
        if (color == _previous)
        {
            _sameColorInRow++;
            if (_sameColorInRow == 3)
            {
                ThreeInRow?.Invoke();
                _previous = BonusColor.None;
                _sameColorInRow = 0;
            }

            return;
        }

        _previous = color;
        _sameColorInRow = 1;
    }
}