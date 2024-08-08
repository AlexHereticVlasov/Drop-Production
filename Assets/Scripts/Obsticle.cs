using Spine.Unity;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
public sealed class Obsticle : MonoBehaviour, ISaveableItem<ObsticleSaveableData>
{
    [SerializeField] private int _index;
    [SerializeField] private SkeletonAnimation _animation;
    [SerializeField] private string[] _keys;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDestructable destructable))
            destructable.Hit(this);
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public ObsticleSaveableData GetData() => new ObsticleSaveableData(_index, transform.position);

    public void Load(ObsticleSaveableData data) => transform.position = data.Position;
}
