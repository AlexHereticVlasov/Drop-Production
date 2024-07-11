using UnityEngine;

[CreateAssetMenu(fileName = nameof(ObsticleViewBean), menuName = nameof(ScriptableObject) + " / " + nameof(ObsticleViewBean))]
public sealed class ObsticleViewBean : ScriptableObject
{
    [SerializeField] private ObsticleView[] _views;

    public ObsticleView this[int index] => _views[index];
}
