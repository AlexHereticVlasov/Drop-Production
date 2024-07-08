using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class LevelLauncher : MonoBehaviour
{
    [SerializeField] private GameObject _levelPanel;
    [SerializeField] private Player _playerTemplate;
    [SerializeField] private CameraAnker _cameraAnkerTemplate;
    [SerializeField] private Transform _content;
    [SerializeField] private LevelSelectionButton _template;
    [SerializeField] private LevelLoader _loader;
    [SerializeField] private BonusSpawner _bonusSpawner;
    [SerializeField] private AllLevels _levels;
    [SerializeField] private WaterPool _pool;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    
    [Header("User Interface")]
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _snowButton;
    [SerializeField] private Button _steamButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private PauseMenu _pauseMenu;

    private Pause _pause;
    

    private void Start()
    {
        _levelPanel.SetActive(true);

        for (int i = 0; i < _levels.Lenght; i++)
        {
            var button = Instantiate(_template, _content);
            button.Init(i, Launch);
        }
    }

    public void Launch(int index)
    {
        //ToDo: Check is Launch Possible
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

            _virtualCamera.Follow = cameraAnker.transform;
            var playerMovement = player.GetComponent<PlayerMovement>();
            _leftButton.onClick.AddListener(playerMovement.MoveLeft);
            _rightButton.onClick.AddListener(playerMovement.MoveRight);
            _snowButton.onClick.AddListener(player.ChangeStateToSnowFlake);
            _steamButton.onClick.AddListener(player.ChangeStateToSteam);

            _pause = new Pause();
            _pauseButton.onClick.AddListener(_pause.SetPause);
            //ToDo: Resume Button
            _pauseMenu.Init(_pause);
        }
    }
}
