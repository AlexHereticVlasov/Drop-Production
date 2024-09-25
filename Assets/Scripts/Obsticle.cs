using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
public sealed class Obsticle : MonoBehaviour, ISaveableItem<ObsticleSaveableData>
{
    [SerializeField] private int _index;
    [SerializeField] private string[] _keys;

    public event UnityAction Killed;
    public event UnityAction Hited;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDestructable destructable))
            destructable.Hit(this);
    }

    public void Kill()
    {
        Killed?.Invoke();
        Destroy(gameObject, 2);
    }

    public ObsticleSaveableData GetData() => new ObsticleSaveableData(_index, transform.position);

    public void Load(ObsticleSaveableData data) => transform.position = data.Position;

    public void Hit() => Hited?.Invoke();
}
