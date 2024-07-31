using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDestructable, IStateObservable
{
    [SerializeField] private WaterPool _pool;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private BaseState _curentState;
    [SerializeField] private StatesBeen _states;

    private bool _canBeHited = true;
    private ColorHandler _colorHandler;
    private DropView _dropView;
    private Timer _timer;
    private DropSize _size;

    public event UnityAction<DropStates> StateChanged;
    public event UnityAction<Player> Victory;
    public event UnityAction Lose;

    public bool WasHited { get; private set; }

    public void Init(WaterPool pool)
    {
        _pool = pool;
        _pool.ValueChanged += OnValueChanged;
        _pool.WaterIsOver += OnWaterIsOver;

        _size = new DropSize(transform, StartCoroutine, GameObject.FindGameObjectWithTag("XYINJA").GetComponent<Spine.Unity.SkeletonUtilityBone>());
        _colorHandler = new ColorHandler();
        _dropView = new DropView(GetComponentInChildren<Spine.Unity.SkeletonAnimation>(), _colorHandler, this);
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
            Victory?.Invoke(this);
            Debug.Log("Victory");
            return;
        }
    }

    private void OnWaterIsOver()
    {
        Lose?.Invoke();
        Debug.Log("Lose");
        Destroy(gameObject);
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

        if (_pool.TryReduce(_states.SnowflakeState.TransformCost))
            ChangeState(_states.SnowflakeState);

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

        if (_pool.TryReduce(_states.SteamState.TransformCost))
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
        _curentState = state;
        StateChanged?.Invoke(_curentState.State);

        if (_curentState.Length > 0)
            SetTimer(_curentState.Length);

        if (state is DropState)
            _size.ChangeSize();
        else
            _size.ChangeSize(1);
    }

    public void Hit(Obsticle obsticle)
    {
        if (_canBeHited == false) return;
        WasHited = true;

        if (_curentState is IcycleState)
        {
            obsticle.Kill();
            return;
        }

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
}
