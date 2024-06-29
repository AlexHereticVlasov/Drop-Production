using UnityEngine;
using UnityEngine.UI;

public sealed class WaterPoolView : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private WaterPool _pool;

    private void OnEnable() => _pool.ValueChanged += OnValueChanged;

    private void OnDisable() => _pool.ValueChanged -= OnValueChanged;

    private void OnValueChanged(float current, float max) => _bar.fillAmount = current / max;
}
