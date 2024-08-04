using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UserInterface;

public class LevelLauncher : MonoBehaviour
{
    [SerializeField] private GameObject _levelPanel;
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private Transform _content;

    [Header("Data")]
    [SerializeField] private AllLevels _levels;
    [SerializeField] private WaterRequres _waterRequres;

    [Header("Prefabs")]
    [SerializeField] private Player _playerTemplate;
    [SerializeField] private CameraAnker _cameraAnkerTemplate;
    [SerializeField] private LevelSelectionButton _template;

    [Header("Components")]
    [SerializeField] private LevelLoader _loader;
    [SerializeField] private BonusSpawner _bonusSpawner;
    [SerializeField] private WaterPool _waterPool;
    [SerializeField] private MainPool _mainPool;
    [SerializeField] private DifficultyBuilder _difficultyBuilder;
    [SerializeField] private PauseBuilder _pauseBuilder;
    [SerializeField] private ControlBuilder _controlBuilder;

    private Background _background;

    private Player _player;

    private void Start()
    {
        _levelPanel.SetActive(true);

        for (int i = 0; i < _levels.Lenght; i++)
        {
            var button = Instantiate(_template, _content);
            button.Init(i, Launch);
        }

        _pauseBuilder.BuildPause();
        _difficultyBuilder.Build();
    }

    private void OnDisable()
    {
        _difficultyBuilder.DeInitialize();
    }

    public void Launch(int index)
    {
        var difficulty = _difficultyBuilder.Get();
        var amount = _waterRequres[difficulty];

        if (_mainPool.TryReduce(amount) == false)
        {
            Debug.Log("Not Enough Water");
            return;
        }

        _loader.Load(_levels[index]);
        //ToDo: Init Drop and BonusSpawner;
        _levelPanel.SetActive(false);
        _bonusSpawner.Launch();

         _player = Instantiate(_playerTemplate, Vector2.zero, Quaternion.identity);
        var cameraAnker = Instantiate(_cameraAnkerTemplate, Vector2.zero, Quaternion.identity);

        _player.Init(_waterPool);
        cameraAnker.Init(_player.transform);
        _controlBuilder.BuildControl(_player, cameraAnker);
        _waterPool.Init(amount);

        _player.Victory += OnVictory;
    }

    private void OnVictory(Player player)
    {
        //Remove Camera target

        //Play Flower Animation

        //Show Victory Window
        _victoryPanel.SetActive(true);
    }

    public void ShowLevelMenu()
    {
        _victoryPanel.SetActive(false);
        _levelPanel.SetActive(true);
        Destroy(_player.gameObject);
        _bonusSpawner.Stop();
    }
}

[System.Serializable]
public sealed class DifficultyBuilder
{
    [SerializeField] private Slider _difficultySlider;
    [SerializeField] private TMP_Text _text;

    private DifficultyHandler _difficultyHandler;
    private DifficultyView _view;

    public void Build()
    {
        _difficultyHandler = new DifficultyHandler();
        _difficultySlider.onValueChanged.AddListener(_difficultyHandler.SetDifficulty);
        _view = new DifficultyView(_text, _difficultyHandler);
    }

    public Difficulty Get() => _difficultyHandler.Get();

    public void DeInitialize() => _view.DeInitialize();
}

public sealed class DifficultyHandler
{
    private Difficulty _value;

    public event UnityAction<Difficulty> ValueChanged;

    public void SetDifficulty(float value)
    {
        _value = (Difficulty)(2 - value);
        ValueChanged?.Invoke(_value);
    }

    public Difficulty Get() => _value;
}

public enum Difficulty
{
    Easy = 0,
    Normal = 1,
    Hard = 2
}
