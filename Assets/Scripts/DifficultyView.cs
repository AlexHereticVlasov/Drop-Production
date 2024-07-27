using TMPro;

namespace UserInterface
{
    public sealed class DifficultyView
    {
        private TMP_Text _text;
        private DifficultyHandler _difficultyHandler;

        public DifficultyView(TMP_Text text, DifficultyHandler difficultyHandler)
        {
            _text = text;
            _difficultyHandler = difficultyHandler;

            _difficultyHandler.ValueChanged += OnValueChanged;
        }

        private void OnValueChanged(Difficulty value) => _text.text = value.ToString();

        public void DeInitialize() => _difficultyHandler.ValueChanged -= OnValueChanged;
    }
}
