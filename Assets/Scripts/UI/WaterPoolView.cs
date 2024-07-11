using UnityEngine;

namespace UserInterface
{
    public abstract class WaterPoolView : MonoBehaviour
    {
        [SerializeField] private WaterPool _pool;

        private void OnEnable() => _pool.ValueChanged += OnValueChanged;

        private void OnDisable() => _pool.ValueChanged -= OnValueChanged;

        protected abstract void OnValueChanged(float current, float max);
    }
}