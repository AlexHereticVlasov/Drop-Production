using UnityEngine;
using UnityEngine.Events;

public sealed class WaterPool : BasePool
{
    public event UnityAction WaterIsOver;
    //hack: Temp solution, use SetValue(newValue);
    
    //private void Start() => ValueChanged?.Invoke(Value, Max);

    public void Init(int value)
    {
        Value = value;
    }

    public override void Reduce(int amount)
    {
        base.Reduce(amount);

        if (Value <= 0)
            WaterIsOver?.Invoke();
    }
}

public abstract class BasePool : MonoBehaviour
{
    [SerializeField] private int _value = 100;

    public int Value
    {
        get => _value;
        protected set
        {
            _value = value;
            ValueChanged?.Invoke(Value, Max);
        }
    }

    [field: SerializeField] public int Max { get; private set; } = 100;

    public event UnityAction<float, float> ValueChanged;
    


    public bool TryReduce(int amount)
    {
        if (Value >= amount)
        {
            Reduce(amount);
            return true;
        }

        return false;
    }

    public virtual void Reduce(int amount)
    {
        if (amount <= 0)
            throw new System.Exception("Reduce Value must be positive");

        Value -= amount;
        Value = Mathf.Clamp(Value, 0, Max);
        ValueChanged?.Invoke(Value, Max);
    }

    public void Increase(int amount)
    {
        if (amount <= 0)
            throw new System.Exception("Increase Value must be positive");

        Value += amount;
        Value = Mathf.Clamp(Value, 0, Max);
        ValueChanged?.Invoke(Value, Max);
    }
}
