using System;
using UnityEngine;
using Spine.Unity;

public sealed class ObsticleAnimation : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SerializeField] private Obsticle _obsticle;

    private void OnEnable()
    {
        _obsticle.Hited += OnHited;
        _obsticle.Killed += OnKilled;
    }

    private void OnDisable()
    {
        _obsticle.Hited -= OnHited;
        _obsticle.Killed -= OnKilled;
    }

    private void OnKilled()
    {
        _skeletonAnimation.AnimationState.SetAnimation(2, "", false);
    }

    private void OnHited()
    {
        Debug.Log("Hited");
        _skeletonAnimation.AnimationState.SetAnimation(1, "stepping", false).Complete += (v)=> _skeletonAnimation.AnimationState.SetAnimation(1, "idle", true);
    }

}