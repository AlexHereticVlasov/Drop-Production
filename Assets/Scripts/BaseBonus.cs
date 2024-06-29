using UnityEngine;

public abstract class BaseBonus : MonoBehaviour, IDestructable
{
    [SerializeField] private Transform _transform;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private float _speed;

    private void Update() => _transform.Translate(_speed * Time.deltaTime * Vector2.down);

    public abstract void Init();

    public abstract void Apply(Player player);

    public void Hit(Obsticle obsticle)
    {
        Destroy(gameObject);
    }
}

public interface IDestructable
{
    void Hit(Obsticle obsticle);
}
