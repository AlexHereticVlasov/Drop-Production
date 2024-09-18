using UnityEngine;

[System.Serializable] 
public abstract class BaseState
{
    [field: SerializeField] public int CollisionCost { get; private set; }
    [field: SerializeField] public int TransformCost { get; private set; }
    [field: SerializeField] public float FallingSpeed { get; private set; }
    [field: SerializeField] public float SideSpeed { get; private set; }
    [field: SerializeField] public float Length { get; private set; }
    [field: SerializeField] public float SizeWiddth { get; private set; }
    [field: SerializeField] public DropStates State { get; private set; }
    [field: SerializeField] public Collider2D Collider2D { get; private set; }
}

[System.Serializable]
public sealed class DropState : BaseState
{ 
}

[System.Serializable]
public sealed class SnowflakeState : BaseState
{
}

[System.Serializable]
public sealed class SteamState : BaseState
{
}

[System.Serializable]
public sealed class IcycleState : BaseState
{
}

[System.Serializable]
public sealed class StatesBeen
{ 
    [field: SerializeField] public DropState DropState { get; private set; }
    [field: SerializeField] public SteamState SteamState { get; private set; }
    [field: SerializeField] public SnowflakeState SnowflakeState { get; private set; }
    [field: SerializeField] public IcycleState IcycleState { get; private set; }
}
