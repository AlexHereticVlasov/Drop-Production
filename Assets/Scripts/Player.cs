using Spine.Unity;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDestructable, IStateObservable
{
    [SerializeField] private WaterPool _pool;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private BaseState _curentState;
    [SerializeField] private StatesBeen _states;
    [SerializeField] private CircleCollider2D _circleCollider;
    [SerializeField] private SkeletonUtilityBone _IKBone;

    private bool _canBeHited = true;
    private ColorHandler _colorHandler;
    private DropView _dropView;
    private Timer _timer;
    private DropSize _size;
    private UserData _userData;

    public event UnityAction<DropStates> StateChanged;
    
    public event UnityAction Lose;
    public event UnityAction Hited;

    public bool WasHited { get; private set; }

    public void Init(WaterPool pool, UserData data)
    {
        _pool = pool;
        _userData = data;
        _pool.ValueChanged += OnValueChanged;
        _pool.WaterIsOver += OnWaterIsOver;

        _size = new DropSize(_circleCollider, StartCoroutine, _IKBone);
        _colorHandler = new ColorHandler();
        _dropView = new DropView(GetComponentInChildren<SkeletonAnimation>(), _colorHandler, this);
        ChangeState(_states.DropState);

        _movement.Init(_size, ref _curentState);

        _colorHandler.ThreeInRow += OnThreeInRow;
    }

    private void OnValueChanged(float value, float max)
    {
        if (_curentState is DropState)
            _size.ChangeSize(value / max);
    }

    private void OnThreeInRow() => ChangeState(_states.IcycleState);

    private void Update()
    {
        _timer?.Update();
        _movement.Move();
    }

    private void OnDisable()
    {
        _pool.WaterIsOver -= OnWaterIsOver;
        _pool.ValueChanged -= OnValueChanged;
        _colorHandler.ThreeInRow -= OnThreeInRow;

        if (_timer != null)
            _timer.TimeIsRunningOut -= OnTimeIsRunningOut;

        _dropView.Deinitialize();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BaseBonus baseBonus))
        {
            baseBonus.Apply(this);
            return;
        }

        if (collision.transform.TryGetComponent(out Earth earth))
        {
            earth.Win();
            return;
        }
    }

    private void OnWaterIsOver()
    {
        Lose?.Invoke();
        Destroy(gameObject, 0.5f);
    }

    public void ChangeStateToSnowFlake()
    {
        if (TryChangeStateToSnowflake() == false)
        {
            Debug.Log("Not Enough Message");
        }
    }

    private bool TryChangeStateToSnowflake()
    {
        if (_curentState is SnowflakeState || _curentState is IcycleState)
            return false;

        if (_userData.TryUseBonus(ItemType.Snowflake))
        {
            ChangeState(_states.SnowflakeState);
            return true;
        }
        return false;
    }

    public void ChangeStateToSteam()
    {
        if (TryChangeStateToSteam() == false)
        {
            Debug.Log("Not Enough Message");
        }
    }

    private bool TryChangeStateToSteam() //ToDo: Try DRY
    {
        if (_curentState is SteamState || _curentState is IcycleState)
            return false;

        if (_userData.TryUseBonus(ItemType.Steam))
            ChangeState(_states.SteamState);

        return false;
    }

    public void AddWater(int amount, BonusColor color)
    {
        _pool.Increase(amount);
        _colorHandler.ApplyColor(color);
    }

    private void ChangeState(BaseState state)
    {
        _movement.SetSpeed(state);

        _size.ChangeSize(state is DropState ? _pool.Value / (float)_pool.Max : 1);

        if (_curentState != null)
            _curentState.Collider2D.enabled = false;

        _curentState = state;
        _curentState.Collider2D.enabled = true;

        StateChanged?.Invoke(_curentState.State);

        if (_curentState.Length > 0)
            SetTimer(_curentState.Length);
    }

    public void Hit(Obsticle obsticle)
    {
        if (_canBeHited == false) return;
        WasHited = true;

        if (_curentState is DropState)
        {
            obsticle.Hit();
            Hited?.Invoke();
        }

        if (_curentState is IcycleState)
        {
            obsticle.Kill();
            return;
        }

        _movement.OnHit();
        _pool.Reduce(_curentState.CollisionCost);
        StartCoroutine(BecomeImmortal());
    }

    private IEnumerator BecomeImmortal()
    {
        _canBeHited = false;
        yield return new WaitForSeconds(1);
        _canBeHited = true;
    }

    public void SetTimer(float length)
    {
        if (_timer != null)
            _timer.TimeIsRunningOut -= OnTimeIsRunningOut;

        _timer = new Timer(length);
        _timer.TimeIsRunningOut += OnTimeIsRunningOut;
    }

    private void OnTimeIsRunningOut()
    {
        _timer.TimeIsRunningOut -= OnTimeIsRunningOut;
        _timer = null;

        ChangeState(_states.DropState);
    }

    public void AddItem(ItemType itemType) => _userData.AddItem(itemType);
}
