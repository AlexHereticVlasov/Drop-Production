using System;
using System.Collections;
using UnityEngine;

public class DropSize
{
    private Transform _transform;
    private Func< IEnumerator, Coroutine> _corutine;
    public float Size { get; private set; } = 1;

    public DropSize(Transform transform, Func<IEnumerator, Coroutine> func)
    {
        _transform = transform;
        _corutine = func;
    }

    public void ChangeSize() => _corutine(ChangeSizeSmoothly(Size));

    public void ChangeSize(float size )
    {
        Size = size;
        ChangeSize();
    }

    private IEnumerator ChangeSizeSmoothly(float targetSize)
    {
        float factor = 0;

        while (factor < 1)
        {
            factor += Time.deltaTime * .5f;
            _transform.localScale = Vector2.Lerp(_transform.localScale, Vector2.one * targetSize, factor);
            yield return null;
        }
    }
}
