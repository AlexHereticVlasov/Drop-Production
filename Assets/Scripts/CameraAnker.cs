using UnityEngine;

public class CameraAnker : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Transform _player;

    public void Init(Transform player) => _player = player;

    private void Update() => _transform.position = new Vector2(0, _player.position.y);
}
