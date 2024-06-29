using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLauncher : MonoBehaviour
{
    [SerializeField] private GameObject _levelPanel;
    [SerializeField] private Player _playerTemplate;
    [SerializeField] private Transform _content;
    [SerializeField] private LevelSelectionButton _template;
    [SerializeField] private LevelLoader _loader;
    [SerializeField] private BonusSpawner _bonusSpawner;
    [SerializeField] private AllLevels _levels;
    [SerializeField] private WaterPool _pool;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;

    private void Start()
    {
        _levelPanel.SetActive(true);
        //ToDo: Create and Init Buttons

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
            player.Init(_pool);
            _virtualCamera.Follow = player.transform;
            var playerMovement = player.GetComponent<PlayerMovement>();
            _leftButton.onClick.AddListener(playerMovement.MoveLeft);
            _rightButton.onClick.AddListener(playerMovement.MoveRight);
        }
    }
}
