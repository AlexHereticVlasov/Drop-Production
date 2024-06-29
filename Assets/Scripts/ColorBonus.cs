using UnityEngine;
using UnityEngine.Events;

public sealed class ColorBonus : BaseBonus
{
    private BonusColor _bonusColor;

    public event UnityAction<BonusColor> ColorChanged;

    public override void Init()
    {
        _bonusColor = (BonusColor)(Random.Range(1, 4));
        ColorChanged?.Invoke(_bonusColor);
    }

    public override void Apply(Player player)
    {
        player.AddWater(10, _bonusColor);
        Destroy(gameObject);
    }
}
