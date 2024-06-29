using UnityEngine;

[System.Serializable]
public sealed class ObsticleSaveableData : BaseSaveableData
{
    public int Index;
    public Vector2 Position;

    public ObsticleSaveableData(int index, Vector2 position)
    {
        Index = index;
        Position = position;
    }
}
