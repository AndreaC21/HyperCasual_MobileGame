using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ysocorp.obstacle
{
    public class ObstacleInstanceUnity : MonoBehaviour
    {
        [Header("Options")]
        [SerializeField] private bool _rotate = false;
        // [SerializeField] private bool _doubleRotation = false;
        //  [SerializeField] private Transform[] _transformToTotate = default;
        private Obstacle _obstacle;
        private bool _moveHorizontal;
        private bool _moveVertical;
        private const float MAX_X = 5;
        private const float MIN_X = -5.0f;
        private const float MAX_Y = 8;
        private const float MIN_Y = 0;
        private Vector3 _leftpoint, _rightPoint, _upPoint, _downPoint;

        private float _speedRotation = 200.0f;
        private float _spedMovement = 0.5f;

        public void Build(Obstacle obstacle)
        {
            _obstacle = obstacle;
            _moveHorizontal = _obstacle.MoveHorizontal;
            _moveVertical = _obstacle.MoveVertical;
            transform.localScale = _obstacle.Scale;

            Vector3 position = transform.localPosition;
            switch (obstacle.Type)
            {
                case Obstacle.ObstacleType.Bottom:
                    {
                        position.x = 0;
                        position.y = 0;
                        break;
                    }
                case Obstacle.ObstacleType.Wall:
                    {
                        position.y = transform.localScale.y * 0.5f;
                        break;
                    }
            }
            transform.localPosition = position;
            enabled = false;

            if (_rotate || _moveHorizontal || _moveVertical)
            {
                enabled = true;
                if (_moveHorizontal || _moveVertical)
                {
                    _leftpoint = new Vector3(MIN_X, transform.localPosition.y, transform.localPosition.z);
                    _rightPoint = new Vector3(MAX_X, transform.localPosition.y, transform.localPosition.z);
                    _upPoint = new Vector3(transform.localPosition.x, MAX_Y, transform.localPosition.z);
                    _downPoint = new Vector3(transform.localPosition.x, MIN_Y, transform.localPosition.z);
                }
            }
        }

        private void Update()
        {
            if (_moveHorizontal)
            {
                MoveHorizontal();
            }
            else if (_moveVertical)
            {
                MoveVertical();
            }
            if (_rotate)
            {
                RotationX();
            }
            /* if (_doubleRotation)
             {
                 RotationY();
             }*/
        }

        private void MoveHorizontal()
        {
            float val = Mathf.PingPong(Time.time * _spedMovement, 1);
            transform.localPosition = Vector3.Lerp(_leftpoint, _rightPoint, val);
        }
        private void MoveVertical()
        {
            float val = Mathf.PingPong(Time.time * _spedMovement, 1);
            transform.localPosition = Vector3.Lerp(_downPoint, _upPoint, val);
        }

        private void RotationX()
        {
            transform.Rotate(Vector3.forward * _speedRotation * Time.deltaTime);
        }

        /*private void RotationY()
        {
            foreach( Transform transform in _transformToTotate)
            {
                transform.Rotate(Vector3.up * 100.0f * Time.deltaTime);
            }
        }*/
    }
}