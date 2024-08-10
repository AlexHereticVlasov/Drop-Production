using UnityEngine;

public sealed class Earth : MonoBehaviour, ISaveableItem<EarthSaveableData>
{
    public EarthSaveableData GetData() => new EarthSaveableData(transform.position);

    public void Load(EarthSaveableData data) => transform.position = data.Position;
}

[System.Serializable]
public sealed class EarthSaveableData : BaseSaveableData
{
    public Vector2 Position;

    public EarthSaveableData(Vector2 position) => Position = position;
}
