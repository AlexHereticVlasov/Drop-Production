using UnityEngine;

public sealed class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    private Pause _pause;

    public void Init(Pause pause)
    {
        _pause = pause;
        _pause.TimeScaleChanged += OnTimeScaleChanged;
    }

    private void OnDisable() => _pause.TimeScaleChanged -= OnTimeScaleChanged;

    private void OnTimeScaleChanged(float timeScale) => _panel.SetActive(timeScale == 0);
}
