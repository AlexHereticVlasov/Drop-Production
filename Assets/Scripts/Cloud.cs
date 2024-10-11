using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Cloud : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _animation; 

    public void Tap()
    {
        _animation.AnimationState.SetAnimation(0, "TAP", false).Complete += (v) => _animation.AnimationState.SetAnimation(0, "IDLE", true);
    }
}
