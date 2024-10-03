using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;

public sealed class LevelEndPanel : MonoBehaviour
{
    [SerializeField] private SkeletonGraphic _animation;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _animation.AnimationState.SetAnimation(0, "pop up", false).Complete += OnComplete;
        
    }

    private void OnDisable()
    {
        _animation.Clear();
        _button.gameObject.SetActive(false);
    }

    private void OnComplete(Spine.TrackEntry trackEntry)
    {
        trackEntry.Complete -= OnComplete;
        _animation.AnimationState.SetAnimation(0, "pop up state", true);
        _button.gameObject.SetActive(true);

    }
}
