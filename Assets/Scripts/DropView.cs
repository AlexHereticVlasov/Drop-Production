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
        _player.Hited += PlayHitAnimation;
        _player.Lose += OnLose;

        StartAnimations();
    }

    private void OnColorChanged(BonusColor newColor) => UpdateSkin();

    private void OnStateChanged(DropStates state)
    {
        _state = state;
        UpdateSkin();
    }

    private void OnLose()
    {
        _animation.AnimationState.SetAnimation(0, "nothing", false);
    }

    private void StartAnimations()
    {
        _animation.AnimationState.SetAnimation(0, "snowflake particles", true);
        _animation.AnimationState.SetAnimation(1, "snowflake idle spin", true);
        _animation.AnimationState.SetAnimation(2, "steam particles", true);
    }

    private void PlayHitAnimation()
    {
        _animation.AnimationState.SetAnimation(8, "left impact1", false).Complete += (v)=> _animation.AnimationState.SetEmptyAnimation(8, 0); 
        _animation.AnimationState.SetAnimation(9, "left impact2", false).Complete += (v) => _animation.AnimationState.SetEmptyAnimation(9, 0);
        _animation.AnimationState.SetAnimation(10, "left impact3", false).Complete += (v) => _animation.AnimationState.SetEmptyAnimation(10, 0);
        _animation.AnimationState.SetAnimation(3, "left impact4", false).Complete += (v) => _animation.AnimationState.SetEmptyAnimation(3, 0); 
        _animation.AnimationState.SetAnimation(4, "right impact1", false).Complete += (v) => _animation.AnimationState.SetEmptyAnimation(4, 0);
        _animation.AnimationState.SetAnimation(5, "right impact2", false).Complete += (v) => _animation.AnimationState.SetEmptyAnimation(5, 0); 
        _animation.AnimationState.SetAnimation(6, "right impact3", false).Complete += (v) => _animation.AnimationState.SetEmptyAnimation(6, 0); 
        _animation.AnimationState.SetAnimation(7, "right impact4", false).Complete += (v) => _animation.AnimationState.SetEmptyAnimation(7, 0); 
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
        _player.Hited -= PlayHitAnimation;
    }

   
}
