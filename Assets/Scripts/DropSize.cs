using System;
using System.Collections;
using UnityEngine;
using Spine.Unity;

public class DropSize
{
    private readonly Vector2 _max = new Vector2(5.5f, 2);
    private readonly Vector2 _min = new Vector2(4.25f, 2);
    private readonly float _maxSize = 0.5f;
    private readonly float _minSize = 0.25f;
    private readonly Vector2 _maxOffset = new Vector2(0, -0.27f);
    private readonly Vector2 _minOffset = new Vector2(0, -0.525f);

    private SkeletonUtilityBone _IK;
    private CircleCollider2D _collider;
    private Func< IEnumerator, Coroutine> _corutine;
    public float Size { get; private set; } = 1;

    public DropSize(CircleCollider2D circleCollider, Func<IEnumerator, Coroutine> func, SkeletonUtilityBone utilityBone)
    {
        _IK = utilityBone;
        _IK.mode = SkeletonUtilityBone.Mode.Override;

        _collider = circleCollider;
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
        Vector2 target = Vector2.Lerp(_min, _max, targetSize);
        Vector2 start = _IK.transform.localPosition;
        _collider.radius = Mathf.Lerp(_minSize, _maxSize, targetSize);
        _collider.offset = Vector2.Lerp(_minOffset, _maxOffset, targetSize);

        float factor = 0;

        while (factor < 1)
        {
            factor += Time.deltaTime * 2;
            _IK.transform.localPosition = Vector2.Lerp(start, target, factor);
            Debug.Log(_IK.transform.localPosition);
            
            yield return null;
        }
    }
}
