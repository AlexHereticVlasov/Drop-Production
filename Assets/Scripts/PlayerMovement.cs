using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _transform;

    private DropSize _size;
    private BaseState _state;
    private float[] _xPositions = {-2f, -1f, 0, 1f, 2f };
    private int _currentIndex = 2;
    private float _sideSpeed = 1;
    private float _speed = 1;

    private Coroutine _moveRoutine;

    public void Init(DropSize size, ref BaseState state)
    {
        _size = size;
        _state = state;
    }

    public void Move()
    {
        float width = _state.SizeWiddth == 1 ? _size.Size : 1;
        //Debug.Log(width);
        _transform.Translate(_speed * Time.deltaTime * Vector2.down * width);
    }

    public void MoveLeft() => ChangePosition(-1);
    public void MoveRight() => ChangePosition(1);

    private void ChangePosition(int direction)
    {
        int newIndex = _currentIndex + direction;
        if (newIndex < 0 || newIndex >= _xPositions.Length)
            return;

        _currentIndex = newIndex;

        if (_moveRoutine != null)
            StopCoroutine(_moveRoutine);

        _moveRoutine = StartCoroutine(MoveSmoothly());
    }

    private IEnumerator MoveSmoothly()
    {
        float factor = 0;
        float previousX = _transform.position.x;
        float distance = CalculateDistance(previousX);

        while (factor < 1)
        {
            float width = _state.SizeWiddth == 1 ? _size.Size : 1;
            factor += Time.deltaTime / distance * _sideSpeed * width;
            float newX = Mathf.Lerp(previousX, _xPositions[_currentIndex], factor);
            _transform.position = new Vector2(newX, _transform.position.y);
            yield return null;
        }

        _moveRoutine = null;
    }

    internal void SetSpeed(BaseState state)
    {
        _sideSpeed = state.SideSpeed;
        _speed = state.FallingSpeed;
    }

    private float CalculateDistance(float previousX) => 
        Mathf.Abs(Mathf.Min(previousX, _xPositions[_currentIndex]) -
        Mathf.Max(previousX, _xPositions[_currentIndex])) / 1.5f;
}
