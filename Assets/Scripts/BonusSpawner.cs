using System.Collections;
using UnityEngine;

public sealed class BonusSpawner : MonoBehaviour
{
    [SerializeField] private BaseBonus _template;
    [SerializeField] private Transform[] _positions;

    private readonly WaitForSeconds _delay = new WaitForSeconds(5);

    //ToDo: Init(5)

    public void Launch() => StartCoroutine(Work());

    private IEnumerator Work()
    {
        while (true)
        {
            yield return _delay;
            Spawn();
        }
    }

    private void Spawn()
    {
        var position = _positions[Random.Range(0, _positions.Length)].position;
        var bonus = Instantiate(_template, position, Quaternion.identity);
        bonus.Init();
    }
}