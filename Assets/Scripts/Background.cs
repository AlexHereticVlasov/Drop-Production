using UnityEngine;

public class Background
{
    [SerializeField] private Sprite _sprite;

    private readonly Transform _transform;

    public Background(Transform transform)
    {
        _transform = transform;
    }

    public void Build(int width)
    {
        for (int y = 0; y > width; y-= 15)
        {
            var position = new Vector2(0, y);
            Object.Instantiate(_sprite, position, Quaternion.identity, _transform);
        }
    }
}
