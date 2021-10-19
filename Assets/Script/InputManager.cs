using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    public delegate void StartTouchEvent(Vector2 position, float time);
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    public event EndTouchEvent OnEndTouch;

    private TouchControl _touchControl;

    void Awake()
    {
        _touchControl = new TouchControl();
    }

    private void OnEnable()
    {
        _touchControl.Enable();
    }

    private void OnDisable()
    {
        _touchControl.Disable();
    }

    private void Start()
    {
        _touchControl.Touch.TouchPress.started += context => StartTouch(context);
        _touchControl.Touch.TouchPress.canceled += context => EndTouch(context);
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null)
        {
            Vector2 touchPosition = _touchControl.Touch.TouchPosition.ReadValue<Vector2>();
            float time = (float)context.startTime;
            OnStartTouch(touchPosition, time);
        }
        
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
        {
            Vector2 touchPosition = _touchControl.Touch.TouchPosition.ReadValue<Vector2>();
            float time = (float)context.time;
            OnEndTouch(touchPosition, time);
        }
    }

    public Vector3 PrimaryPosition()
    {
        Vector3 touchPosition = _touchControl.Touch.TouchPosition.ReadValue<Vector2>();
        touchPosition.z = _camera.nearClipPlane;
        Vector3 worldPosition = _camera.ScreenToWorldPoint(touchPosition);
        worldPosition.z = 0;
        return worldPosition;
    }

    public Vector2 ScreenPosition()
    {
        return _touchControl.Touch.TouchPosition.ReadValue<Vector2>();
    }
}
