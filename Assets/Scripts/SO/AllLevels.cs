using UnityEngine;

[CreateAssetMenu(fileName = nameof(AllLevels), menuName = nameof(ScriptableObject) + " / " + nameof(AllLevels))]
public sealed class AllLevels : ScriptableObject
{
    [SerializeField] private TextAsset[] _levels;

    public TextAsset this[int index] => _levels[index];

    public int Lenght => _levels.Length;
}
