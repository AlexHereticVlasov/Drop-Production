using Cinemachine;
using UnityEngine;

public class Confiner : MonoBehaviour, ISaveableItem
{
    [SerializeField] private PolygonCollider2D _collider;
    [SerializeField] private CinemachineConfiner2D _cinemachineConfiner;

    public BaseSaveableData GetData()
    {
        Vector2[] _points = new Vector2[4];

        for (int i = 0; i < 4; i++)
            _points[i] = _collider.points[i];

        return new ConfinerSaveableData(_points);
    }

    public void Load(BaseSaveableData data)
    {
        if (data is ConfinerSaveableData confinerSaveableData)
        {
            _collider.SetPath(0, confinerSaveableData.Points);
            _cinemachineConfiner.InvalidateCache();
        }
    }
}

[System.Serializable]
public class ConfinerSaveableData : BaseSaveableData
{
    public Vector2[] Points;

    public ConfinerSaveableData(Vector2[] points) => Points = points;
}
