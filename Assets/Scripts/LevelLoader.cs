using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Earth _earth;
    [SerializeField] private Confiner _confinder;
    [SerializeField] private Transform _anker;

    [Header("Save Name")]
    [SerializeField] private string _nameOfSave = "newLevel";
    [Header("Level To Laod")]
    [SerializeField] private TextAsset _textAsset;

    [Header("Prefabs")]
    [SerializeField] private Obsticle[] _staticObsticleTemplates;
    [SerializeField] private ObsticleMovement[] _movingObsticleTemplate;

    private List<Obsticle> _obsticles = new List<Obsticle>();
    private List<ObsticleMovement> _movingObsticles = new List<ObsticleMovement>();

    public event UnityAction<float> EarthPositionChanged;

    public void Load() => Load(_textAsset);

    public void Load(TextAsset textAsset)
    {
        string json = textAsset.ToString();
        LevelData data = JsonUtility.FromJson<LevelData>(json);

        _confinder.Load(data.ConfinderSaveableData);
        _earth.Load(data.EarthSaveableData);
        EarthPositionChanged?.Invoke(_earth.transform.position.y);

        int count = _anker.childCount;
        for (int i = count - 1; i >= 0; i--)
            DestroyImmediate(_anker.GetChild(i).gameObject);

        foreach (var item in data.ObsticleSaveableDatas)
        {
            var obsticle = Instantiate(_staticObsticleTemplates[item.Index], _anker);
            _obsticles.Add(obsticle);
            obsticle.Load(item);
        }

        foreach (var item in data.MovingObsticleSaveableDatas)
        {
            var obsticle = Instantiate(_movingObsticleTemplate[item.Index], _anker);
            _movingObsticles.Add(obsticle);
            obsticle.Load(item);
        }
    }

    public void Save()
    {
        var LevelData = new LevelData();
        LevelData.ConfinderSaveableData = _confinder.GetData();
        LevelData.EarthSaveableData = _earth.GetData();

        var obsticleDatas = new List<ObsticleSaveableData>();
        var movingObsticleDatas = new List<MovingObsticleSaveableData>();

        var obsticlesCount = _anker.childCount;
        for (int i = 0; i < obsticlesCount; i++)
        {
            var child = _anker.GetChild(i);
            if (child.TryGetComponent(out Obsticle obsticle))
                obsticleDatas.Add(obsticle.GetData());
            else if (child.TryGetComponent(out ObsticleMovement movement))
                movingObsticleDatas.Add(movement.GetData());
            else
                Debug.LogWarning("Wrong Type");
        }

        LevelData.ObsticleSaveableDatas = obsticleDatas;
        LevelData.MovingObsticleSaveableDatas = movingObsticleDatas;

        string json = JsonUtility.ToJson(LevelData, true);
        File.WriteAllText(Application.dataPath + $"/{_nameOfSave}.json", json);
        Debug.Log("Save");
    }
}

[System.Serializable]
public sealed class LevelData
{
    public ConfinerSaveableData ConfinderSaveableData;
    public EarthSaveableData EarthSaveableData;
    public List<ObsticleSaveableData> ObsticleSaveableDatas;
    public List<MovingObsticleSaveableData> MovingObsticleSaveableDatas;
}
