using UnityEngine;
using Spine.Unity;

public class EarthView : MonoBehaviour
{
    [SerializeField] private Earth _earth;
    [SerializeField] private SkeletonAnimation _animation;

    private void OnEnable()
    {
        _earth.Hited += OnHited;
        _earth.Restarted += OnRestarted;
    }

    private void OnDisable()
    {
        _earth.Hited -= OnHited;
        _earth.Restarted -= OnRestarted;
    }

    private void OnHited()
    {
        _animation.AnimationState.SetAnimation(0, "growing up", false).Complete += (v) =>
        _animation.AnimationState.SetAnimation(0, "full state idle", true);
    }

    private void OnRestarted()
    {
        _animation.AnimationState.SetAnimation(0,"underground state", true);
    }
}
