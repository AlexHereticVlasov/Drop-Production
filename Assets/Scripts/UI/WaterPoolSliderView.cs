using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public sealed class WaterPoolSliderView : WaterPoolView
    {
        [SerializeField] private Image _bar;

        protected override void OnValueChanged(float current, float max) =>
            _bar.fillAmount = current / max;
    }
}
