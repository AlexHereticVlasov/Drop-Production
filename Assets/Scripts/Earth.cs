using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public sealed class Earth : MonoBehaviour, ISaveableItem<EarthSaveableData>
{
    [SerializeField] private Transform _transform;

    public Transform Transform => _transform;

    public event UnityAction Victory;
    public event UnityAction Hited;
    public event UnityAction Restarted;

    public EarthSaveableData GetData() => new EarthSaveableData(transform.position);

    public void Load(EarthSaveableData data)
    {
        _transform.position = data.Position;
        Restarted?.Invoke();
    }

    public void Win()
    {
        StartCoroutine(WinRoutine());
    }

    public IEnumerator WinRoutine()
    {
        Hited?.Invoke();
        yield return new WaitForSeconds(2);
        Victory?.Invoke();
    }
}

[System.Serializable]
public sealed class EarthSaveableData : BaseSaveableData
{
    public Vector2 Position;

    public EarthSaveableData(Vector2 position) => Position = position;
}
