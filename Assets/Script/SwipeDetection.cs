
using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private GameObject _trail = default;
    [SerializeField] private InputManager _inputManager = default;
    [SerializeField] private float _minDistance = 0.05f;
    [SerializeField] private float _maxTime = 1.0f;

    private Vector2 _startPosition;
    private float _startTime;
    private Vector2 _endPosition;
    private float _endTime;
    private const float DIRECTION_THRESHOLD = 0.9f;
    private Coroutine _moveTrail;

    public Vector3 WorldTouchPosition
    {
        get => _inputManager.PrimaryPosition();
    }

    private void OnEnable()
    {
        _inputManager.OnStartTouch += SwipeStart;
        _inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        _inputManager.OnStartTouch -= SwipeStart;
        _inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 worldPosition, float time)
    {
        _startPosition = worldPosition;
        _startTime = time;
       _trail.transform.position = worldPosition;
        _moveTrail = StartCoroutine(MoveTrail());
        _trail.SetActive(true);
    }

    private IEnumerator MoveTrail()
    {
        while (true)
        {
            _trail.transform.position = _inputManager.PrimaryPosition();
            yield return null;
        }
    }

    private void SwipeEnd(Vector2 worldPosition, float time)
    {
        _trail.SetActive(false);
        StopCoroutine(_moveTrail);
        _endPosition = worldPosition;
        _endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(_startPosition, _endPosition) >= _minDistance && (_endTime - _startTime) <= _maxTime)
        {
            Vector3 directionD = _endPosition - _startPosition;
            Vector2 direction = new Vector2(directionD.x, directionD.y).normalized;
            SwipeDirection(direction);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.left, direction) > DIRECTION_THRESHOLD)
        {
            OnSwipeDirection(-1);
        }
        else if (Vector2.Dot(Vector2.right, direction) > DIRECTION_THRESHOLD)
        {
            OnSwipeDirection(1);
        }
        else if (Vector2.Dot(Vector2.up, direction) > DIRECTION_THRESHOLD)
        {
            OnSwipeVertical(1);
        }
        else if (Vector2.Dot(Vector2.down, direction) > DIRECTION_THRESHOLD)
        {
            OnSwipeVertical(-1);
        }
    }
   public delegate void Swipe(int direction);
   public event Swipe OnSwipeDirection;
   public delegate void SwipeVertical(int direction);
   public event SwipeVertical OnSwipeVertical;
}
