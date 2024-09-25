using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[System.Serializable]
public sealed class ControlBuilder
{
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _snowButton;
    [SerializeField] private Button _steamButton;
    [SerializeField] private TMP_Text _snowText;
    [SerializeField] private TMP_Text _steamText;


    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    public void Build(Player player, CameraAnker cameraAnker, IUserDataBonusReadOnly userBonusData)
    {
        _virtualCamera.Follow = cameraAnker.transform;
        var playerMovement = player.GetComponent<PlayerMovement>();
        _leftButton.onClick.AddListener(playerMovement.MoveLeft);
        _rightButton.onClick.AddListener(playerMovement.MoveRight);

        _snowButton.onClick.AddListener(player.ChangeStateToSnowFlake);
        _steamButton.onClick.AddListener(player.ChangeStateToSteam);

        userBonusData.AmountChanged += OnAmountChanged;
        OnAmountChanged(userBonusData);
    }

    private void OnAmountChanged(IUserDataBonusReadOnly data)
    {
        _snowText.text = data.SnowflakeBonusAmount.ToString();
        _steamText.text = data.SteamBonusAmount.ToString();
    }

    internal void DeInitialize(IUserDataBonusReadOnly userBonusData)
    {
        userBonusData.AmountChanged -= OnAmountChanged;
    }
}
