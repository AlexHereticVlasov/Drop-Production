using UnityEngine;

public abstract class BaseBonus : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _speed;

    private void Update() => _transform.Translate(_speed * Time.deltaTime * Vector2.down);

    public abstract void Init();

    public abstract void Apply(Player player);
}
