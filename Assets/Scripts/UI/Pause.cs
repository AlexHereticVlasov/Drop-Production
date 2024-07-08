using UnityEngine;
using UnityEngine.Events;

public sealed class Pause
{
    public event UnityAction<float> TimeScaleChanged;

    public void SetPause()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        TimeScaleChanged?.Invoke(Time.timeScale);
    }
}
