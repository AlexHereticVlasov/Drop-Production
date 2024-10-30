using UnityEngine;
using UnityEngine.Events;

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
