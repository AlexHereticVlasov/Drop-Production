using UnityEngine;
using UnityEngine.Events;

public class Timer
{
    private float _value;

    public event UnityAction TimeIsRunningOut;

    public Timer(float value) => _value = value;

    public void Update()
    {
        _value -= Time.deltaTime;

        if (_value <= 0)
            TimeIsRunningOut?.Invoke();
    }
}
