using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner
{
    private Vector3 _startPosition;
    private float _distanceToNext = 1.5f;

    private Vector2 _direction;
    private float _angle = 90.0f;
    private float _angleNext = 45.0f;

    public CircleSpawner(Vector3 startPosition)
    {
        _startPosition = startPosition;
    }

   public Vector3 GetNextPosition()
   {
        Vector3 result;
        float angleRadian = _angle * Mathf.Deg2Rad;
        _direction = new Vector2(Mathf.Cos(angleRadian), Mathf.Sin(angleRadian));
        float positionX = _startPosition.x + _direction.x * _distanceToNext;
        float positionZ = _startPosition.z + _direction.y * _distanceToNext;
        _angle += _angleNext;
        if (_angle >=360)
        {
            _angle = _angle % 360;
            _distanceToNext *= 2;
        }
        result = new Vector3(positionX, 0.0f, positionZ);
        return result;
   }
}
