using UnityEngine;

[System.Serializable] 
public abstract class BaseState
{
    [field: SerializeField] public int CollisionCost { get; private set; }
    [field: SerializeField] public int TransformCost { get; private set; }
    [field: SerializeField] public float FallingSpeed { get; internal set; }
    [field: SerializeField] public float SideSpeed { get; internal set; }
    [field: SerializeField] public float Length { get; internal set; }
    [field: SerializeField] public float SizeWiddth { get; internal set; }
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
