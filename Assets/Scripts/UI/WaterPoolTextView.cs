using TMPro;
using UnityEngine;

namespace UserInterface
{
    public sealed class WaterPoolTextView : WaterPoolView
    {
        [SerializeField] private TMP_Text _text;
        protected override void OnValueChanged(float current, float max) =>
            _text.text = $"{current} / {max}";
    }
}