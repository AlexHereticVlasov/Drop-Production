using Spine.Unity;

public sealed class DropView
{
    private readonly SkeletonAnimation _animation;
    private readonly ColorHandler _colorHandler;
    private readonly IStateObservable _player;

    private DropStates _state;

    private readonly string[,] _keys =
    {
        {"blue drop","blue snowflake","blue steam","blue icicle"},
        {"red drop","red snowflake","red steam","red icicle"},
        {"yellow drop","yellow snowflake","yellow steam","yellow icicle"},
    };

    public DropView(SkeletonAnimation animation, ColorHandler colorHandler, IStateObservable player)
    {
        _animation = animation;
        _player = player;
        _colorHandler = colorHandler;

        _colorHandler.ColorChanged += OnColorChanged;
        _player.StateChanged += OnStateChanged;

        StartAnimations();
    }

    private void StartAnimations()
    {
        _animation.AnimationState.SetAnimation(0, "snowflake particles", true);
        _animation.AnimationState.SetAnimation(1, "snowflake idle spin", true);
        _animation.AnimationState.SetAnimation(2, "steam particles", true);
    }

    private void OnStateChanged(DropStates state)
    {
        _state = state;
        UpdateSkin();
    }

    private void UpdateSkin()
    {
        _animation.Skeleton.SetSkin(_keys[(int)_colorHandler.Last, (int)_state]);
        _animation.Skeleton.SetBonesToSetupPose();
        _animation.Skeleton.SetSlotsToSetupPose();
    }

    public void Deinitialize()
    {
        _colorHandler.ColorChanged -= OnColorChanged;
        _player.StateChanged -= OnStateChanged;
    }

    private void OnColorChanged(BonusColor newColor) => UpdateSkin();
}
