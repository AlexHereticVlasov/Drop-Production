using Cinemachine;
using UnityEngine;

public class Confiner : MonoBehaviour, ISaveableItem<ConfinerSaveableData>
{
    [SerializeField] private PolygonCollider2D _collider;
    [SerializeField] private CinemachineConfiner2D _cinemachineConfiner;

    public ConfinerSaveableData GetData()
    {
        Vector2[] _points = new Vector2[4];

        for (int i = 0; i < 4; i++)
            _points[i] = _collider.points[i];

        return new ConfinerSaveableData(_points);
    }

    public void Load(ConfinerSaveableData data)
    {
        _collider.SetPath(0, data.Points);
        _cinemachineConfiner.InvalidateCache();
    }
}

[System.Serializable]
public class ConfinerSaveableData : BaseSaveableData
{
    public Vector2[] Points;

    public ConfinerSaveableData(Vector2[] points) => Points = points;
}
