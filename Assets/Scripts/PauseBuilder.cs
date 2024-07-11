using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PauseBuilder
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _pauseResumeButton;
    [SerializeField] private PauseMenu _pauseMenu;

    private Pause _pause;

    public void BuildPause()
    {
        _pause = new Pause();
        _pauseButton.onClick.AddListener(_pause.SetPause);
        _pauseResumeButton.onClick.AddListener(_pause.SetPause);
        _pauseMenu.Init(_pause);
    }
}
