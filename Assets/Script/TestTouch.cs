using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTouch : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Camera _camera;
    void OnEnable()
    {
        _inputManager.OnStartTouch += Move;
    }

    void OnDisable()
    {
        _inputManager.OnEndTouch += Move;
    }

    private void Move(Vector2 screenPosition, float time)
    {
        Vector3 screenCoordinate = new Vector3(screenPosition.x, screenPosition.y, _camera.nearClipPlane);
        Vector3 worldCoordinate = _camera.ScreenToWorldPoint(screenCoordinate);
        worldCoordinate.z = 0;
        transform.position = worldCoordinate;
    }
}
