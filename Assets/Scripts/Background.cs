using UnityEngine;

[System.Serializable]
public class Background
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Transform _transform;

    public void Build(int width)
    {
        _sprite.size = new Vector2(7.2f, Mathf.Abs(width));
        _sprite.transform.position = new Vector2(0, width / 2);
    }
}
