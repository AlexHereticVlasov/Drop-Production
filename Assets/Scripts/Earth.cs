using UnityEngine;

public sealed class Earth : MonoBehaviour, ISaveableItem
{
    public BaseSaveableData GetData() => new EarthSaveableData(transform.position);

    public void Load(BaseSaveableData data)
    {
        if (data is EarthSaveableData earthSaveableData)
            transform.position = earthSaveableData.Position;
    }
}

[System.Serializable]
public sealed class EarthSaveableData : BaseSaveableData
{
    public Vector2 Position;

    public EarthSaveableData(Vector2 position)
    {
        Position = position;
    }
}
