using UnityEngine;
using Spine.Unity;

[RequireComponent(typeof(ColorBonus))]
public sealed class ColorBonusView : MonoBehaviour
{
    private static readonly string[] _keys = {"blue drop", "red drop", "yellow drop" };

    [SerializeField] private SkeletonAnimation _animation;
    [SerializeField] private ColorBonus _colorBonus;

    private void OnEnable() => _colorBonus.ColorChanged += UpdateSkin;

    private void OnDisable() => _colorBonus.ColorChanged -= UpdateSkin;

    private void UpdateSkin(BonusColor color)
    {
        _animation.Skeleton.SetSkin(_keys[(int)color - 1]);
        _animation.Skeleton.SetBonesToSetupPose();
        _animation.Skeleton.SetSlotsToSetupPose();
    }

    //ToDo: Collect Animation
}
