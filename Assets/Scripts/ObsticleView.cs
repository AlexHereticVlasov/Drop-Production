using UnityEngine;

[System.Serializable]
public sealed class ObsticleView
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public Vector2 Offset { get; private set; }
    [field: SerializeField] public Vector2 ColliderSize { get; private set; }
}
