using UnityEngine;
using UnityEngine.UI;

public class LevelLauncher : MonoBehaviour
{
    [SerializeField] private GameObject _levelPanel;
    [SerializeField] private Transform _content;
    [SerializeField] private AllLevels _levels;

    [Header("Prefabs")]
    [SerializeField] private Player _playerTemplate;
    [SerializeField] private CameraAnker _cameraAnkerTemplate;
    [SerializeField] private LevelSelectionButton _template;
    
    [Header("User Interface")]
    [SerializeField] private Slider _difficultySlider;

    [Header("Components")]
    [SerializeField] private LevelLoader _loader;
    [SerializeField] private BonusSpawner _bonusSpawner;
    [SerializeField] private WaterPool _pool;
    [SerializeField] private PauseBuilder _pauseBuilder;
    [SerializeField] private ControlBuilder _controlBuilder;

    private void Start()
    {
        _levelPanel.SetActive(true);

        for (int i = 0; i < _levels.Lenght; i++)
        {
            var button = Instantiate(_template, _content);
            button.Init(i, Launch);
        }

        _pauseBuilder.BuildPause();
    }

    public void Launch(int index)
    {
        //ToDo: Check is Launch Possible(is level reached and is enough Water in Main Pool)
        if (true)
        {
            _loader.Load(_levels[index]);
            //ToDo: Init Drop and BonusSpawner;
            _levelPanel.SetActive(false);
            _bonusSpawner.Launch();

            var player = Instantiate(_playerTemplate, Vector2.zero, Quaternion.identity);
            var cameraAnker = Instantiate(_cameraAnkerTemplate, Vector2.zero, Quaternion.identity);

            player.Init(_pool);
            cameraAnker.Init(player.transform);
            _controlBuilder.BuildControl(player, cameraAnker);
        }
    }
}
