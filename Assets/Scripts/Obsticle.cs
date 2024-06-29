using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
public sealed class Obsticle : MonoBehaviour, ISaveableItem
{
    [SerializeField, Range(0, 2)] private int _index;
    [SerializeField] private ObsticleViewBean _bean;

    private void OnValidate()
    {
        if (Application.isEditor == false || Application.isPlaying) return;

        GetComponent<BoxCollider2D>().size = _bean[_index].ColliderSize;
        GetComponent<SpriteRenderer>().sprite = _bean[_index].Sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDestructable destructable))
            destructable.Hit(this);
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public BaseSaveableData GetData()
    {
        return new ObsticleSaveableData(_index, transform.position);
    }

    public void Load(BaseSaveableData data)
    {
        if (data is ObsticleSaveableData obsticleSaveableData)
        {
            transform.position = obsticleSaveableData.Position;
            _index = obsticleSaveableData.Index;
            GetComponent<BoxCollider2D>().size = _bean[_index].ColliderSize;
            GetComponent<SpriteRenderer>().sprite = _bean[_index].Sprite;
        }
    }
}
