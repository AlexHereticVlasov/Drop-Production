using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confinder : MonoBehaviour, ISaveableItem
{
    [SerializeField] private PolygonCollider2D _collider;

    public BaseSaveableData GetData()
    {
        Vector2[] _points = new Vector2[4];

        for (int i = 0; i < 4; i++)
            _points[i] = _collider.points[i];

        return new ConfinderSaveableData(_points);
    }

    public void Load(BaseSaveableData data)
    {
        if (data is ConfinderSaveableData confinderSaveableData)
        {
            for (int i = 0; i < 4; i++)
            {
                _collider.points[i] = confinderSaveableData.Points[i];
            }
        }
    }
}

[System.Serializable]
public class ConfinderSaveableData : BaseSaveableData
{
    public Vector2[] Points;

    public ConfinderSaveableData(Vector2[] points) => Points = points;
}
