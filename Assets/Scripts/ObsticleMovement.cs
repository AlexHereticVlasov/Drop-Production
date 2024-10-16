using System.Collections.Generic;
using UnityEngine;

public class ObsticleMovement : MonoBehaviour, ISaveableItem
{
    [SerializeField] private GameObject _pointTemplate;

    [SerializeField] private Obsticle _obsticle;
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed = 1;

    private Transform _obsticleTransform;
    private int _index;
    private float _factor;
    private float _distance;

    private void Awake()
    {
        _obsticleTransform = _obsticle.transform;

        if (_points.Length < 2)
            throw new System.Exception("Moving Obsticle has less then 2 waypoints");
    }

    private void Start() => _distance = 1 / Vector2.Distance(_points[0].position, _points[1].position);

    private void Update()
    {
        _factor += Time.deltaTime * _distance * _speed;
        Move();

        if (_factor >= 1)
        {
            _factor--;
            _index++;
            _index %= _points.Length;
        }
    }

    private void Move() => _obsticleTransform.position =
        Vector2.Lerp(_points[_index].position, _points[(_index + 1) %
                     _points.Length].position, _factor);

    public BaseSaveableData GetData()
    {
        Vector2[] points = new Vector2[_points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = _points[i].position;
        }

        ObsticleSaveableData data = _obsticle.GetData() as ObsticleSaveableData;

        return new MovingObsticleSaveableData(points, _speed, data);
    }

    public void Load(BaseSaveableData data)
    {
        if (data is MovingObsticleSaveableData movingObsticleSaveableData)
        {
            _points = new Transform[movingObsticleSaveableData.Points.Length];
            for (int i = 0; i < movingObsticleSaveableData.Points.Length; i++)
            {
                var position = movingObsticleSaveableData.Points[i];
                _points[i] = Instantiate(_pointTemplate, position, Quaternion.identity, transform).transform;
            }

            _speed = movingObsticleSaveableData.Speed;
            _obsticle.Load(new ObsticleSaveableData(movingObsticleSaveableData.Index, movingObsticleSaveableData.Position));
        }
    }
}

public interface ISaveableItem
{
    BaseSaveableData GetData();

    void Load(BaseSaveableData data);
}

public abstract class BaseSaveableData { }

[System.Serializable]
public sealed class MovingObsticleSaveableData : BaseSaveableData
{
    public Vector2[] Points;
    public float Speed;
    public int Index;
    public Vector2 Position;

    public MovingObsticleSaveableData(Vector2[] points, float speed, ObsticleSaveableData saveableData)
    {
        Points = points;
        Speed = speed;
        Index = saveableData.Index;
        Position = saveableData.Position;
    }
}
