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

public sealed class ItemBonus : BaseBonus
{
    private ItemType _itemType;

    public event UnityAction<ItemType> TypeChanged;


    public override void Apply(Player player)
    {
        player.AddItem(_itemType);
        Destroy(gameObject);
    }

    public override void Init()
    {
        _itemType = (ItemType)Random.Range(0, 2);
        TypeChanged?.Invoke(_itemType);
    }
}

public enum ItemType
{ 
    Snowflake = 0,
    Steam = 1
}