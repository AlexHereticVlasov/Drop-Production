using UnityEngine;

[CreateAssetMenu(fileName = nameof(WaterRequres), menuName = nameof(ScriptableObject) + " / " + nameof(WaterRequres))]
public sealed class WaterRequres : ScriptableObject
{
    [SerializeField] private int[] _amouns;

    public int this[Difficulty index] => _amouns[(int) index];
}