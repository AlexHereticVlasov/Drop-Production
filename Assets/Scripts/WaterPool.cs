using UnityEngine;
using UnityEngine.Events;

public sealed class WaterPool : MonoBehaviour
{
    private int _value = 100;
    private int _max = 100;

    public event UnityAction<float, float> ValueChanged;
    public event UnityAction WaterIsOver;

    public bool TryReduce(int amount)
    {
        if (_value >= amount)
        {
            Reduce(amount);
            return true;
        }

        return false;
    }

    public void Reduce(int amount)
    {
        if (amount <= 0)
            throw new System.Exception("Reduce Value must be positive");

        _value -= amount;
        _value = Mathf.Clamp(_value, 0, _max);
        ValueChanged?.Invoke(_value, _max);

        if (_value <= 0)
            WaterIsOver?.Invoke();
    }

    public void Increase(int amount)
    {
        if (amount <= 0)
            throw new System.Exception("Increase Value must be positive");

        _value += amount;
        _value = Mathf.Clamp(_value, 0, _max);
        ValueChanged?.Invoke(_value, _max);
    }
}
