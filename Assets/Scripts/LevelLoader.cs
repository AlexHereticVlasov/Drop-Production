using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Earth _earth;
    [SerializeField] private Confinder _confinder;
    [SerializeField] private Transform _anker;

    [SerializeField] private TextAsset _textAsset;
    [SerializeField] private Obsticle _staticObsticleTemplate;
    [SerializeField] private ObsticleMovement _movingObsticleTemplate;

    private List<Obsticle> _obsticles = new List<Obsticle>();
    private List<ObsticleMovement> _movingObsticles = new List<ObsticleMovement>();

    public void Load() => Load(_textAsset);

    public void Load(TextAsset textAsset)
    {
        string json = textAsset.ToString();
        LevelData data = JsonUtility.FromJson<LevelData>(json);

        _confinder.Load(data.ConfinderSaveableData);
        _earth.Load(data.EarthSaveableData);

        int count = _anker.childCount;
        for (int i = count - 1; i >= 0; i--)
            DestroyImmediate(_anker.GetChild(i).gameObject);

        foreach (var item in data.ObsticleSaveableDatas)
        {
            var obsticle = Instantiate(_staticObsticleTemplate, _anker);
            _obsticles.Add(obsticle);
            obsticle.Load(item);
        }

        foreach (var item in data.MovingObsticleSaveableDatas)
        {
            var obsticle = Instantiate(_movingObsticleTemplate, _anker);
            _movingObsticles.Add(obsticle);
            obsticle.Load(item);
        }
    }

    public void Save()
    {
        var LevelData = new LevelData();
        LevelData.ConfinderSaveableData = _confinder.GetData() as ConfinderSaveableData;
        LevelData.EarthSaveableData = _earth.GetData() as EarthSaveableData;

        var obsticleDatas = new List<ObsticleSaveableData>();
        var movingObsticleDatas = new List<MovingObsticleSaveableData>();

        var obsticlesCount = _anker.childCount;
        for (int i = 0; i < obsticlesCount; i++)
        {
            var child = _anker.GetChild(i);
            if (child.TryGetComponent(out Obsticle obsticle))
                obsticleDatas.Add(obsticle.GetData() as ObsticleSaveableData);
            else if (child.TryGetComponent(out ObsticleMovement movement))
                movingObsticleDatas.Add(movement.GetData() as MovingObsticleSaveableData);
            else
                Debug.LogWarning("Wrong Type");
        }

        LevelData.ObsticleSaveableDatas = obsticleDatas;
        LevelData.MovingObsticleSaveableDatas = movingObsticleDatas;

        string json = JsonUtility.ToJson(LevelData, true);
        File.WriteAllText(Application.dataPath + "/testLevel.json", json);
        Debug.Log("Save");
    }
}

[System.Serializable]
public sealed class LevelData
{
    public ConfinderSaveableData ConfinderSaveableData;
    public EarthSaveableData EarthSaveableData;
    public List<ObsticleSaveableData> ObsticleSaveableDatas;
    public List<MovingObsticleSaveableData> MovingObsticleSaveableDatas;
}
