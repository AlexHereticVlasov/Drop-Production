using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public sealed class ControlBuilder
{
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _snowButton;
    [SerializeField] private Button _steamButton;

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    public void BuildControl(Player player, CameraAnker cameraAnker)
    {
        _virtualCamera.Follow = cameraAnker.transform;
        var playerMovement = player.GetComponent<PlayerMovement>();
        _leftButton.onClick.AddListener(playerMovement.MoveLeft);
        _rightButton.onClick.AddListener(playerMovement.MoveRight);

        _snowButton.onClick.AddListener(player.ChangeStateToSnowFlake);
        _steamButton.onClick.AddListener(player.ChangeStateToSteam);
    }
}
