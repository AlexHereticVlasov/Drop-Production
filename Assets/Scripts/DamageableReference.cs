using UnityEngine;

public sealed class DamageableReference : MonoBehaviour, IDestructable
{
    [SerializeField] private Player _player;

    public void Hit(Obsticle obsticle) => _player.Hit(obsticle);
}
