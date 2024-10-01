using UnityEngine;
using Spine.Unity;

public sealed class ItemBonusView : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _animation;
    [SerializeField] private ItemBonus _itemBonus;

    private static readonly string[] _keys = new string[] { "blue snowflake", "blue steam" };

    private void OnEnable() => _itemBonus.TypeChanged += OnTypeChanged;

    private void OnDisable() => _itemBonus.TypeChanged -= OnTypeChanged;

    private void OnTypeChanged(ItemType itemType)
    {
        _animation.Skeleton.SetSkin(_keys[(int)itemType]);
        _animation.Skeleton.SetBonesToSetupPose();
        _animation.Skeleton.SetSlotsToSetupPose();
    }
}