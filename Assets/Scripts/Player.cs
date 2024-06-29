using UnityEngine;

public class Player : MonoBehaviour, IDestructable
{
    [SerializeField] private WaterPool _pool;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private BaseState _curentState;
    [SerializeField] private StatesBeen _states;

    private ColorHandler _colorHandler;
    private Timer _timer;

    public void Init(WaterPool pool)
    {
        _pool = pool;
        _pool.WaterIsOver += OnWaterIsOver;
    }

    private void Awake()
    {
        _colorHandler = new ColorHandler();
        ChangeState(_states.DropState);
    }

    private void OnEnable()
    {
        
        _colorHandler.ThreeInRow += OnThreeInRow;
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
        _colorHandler.ThreeInRow -= OnThreeInRow;
        if (_timer != null)
            _timer.TimeIsRunningOut -= OnTimeIsRunningOut;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BaseBonus baseBonus))
            baseBonus.Apply(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Earth earth))
        {
            Debug.Log("Victory");
        }
    }

    private void OnWaterIsOver()
    {
        Debug.Log("Lose");
        Destroy(gameObject);
    }

    public bool TryChangeStateToSnowflake()
    {
        if (_curentState is SnowflakeState || _curentState is IcycleState) 
            return false;

        if (_pool.TryReduce(_states.SnowflakeState.TransformCost))
            ChangeState(_states.SnowflakeState);

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

        if (_curentState.Length > 0)
            SetTimer(_curentState.Length);
    }

    public void Hit(Obsticle obsticle)
    {
            if (_curentState is IcycleState)
            {
                obsticle.Kill();
                return;
            } 

            _pool.Reduce(_curentState.CollisionCost);
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
