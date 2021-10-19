using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ysocorp.obstacle
{

    [CreateAssetMenu(fileName = "Obstacle", menuName = "ScriptableObjects/Obsctale", order = 2)]

    public class Obstacle : ScriptableObject
    {
        [SerializeField] private Vector3 _scale;
        [SerializeField] private ObstacleType _obstacleType;
        [SerializeField] private ObstacleInstanceUnity _prefab;
        [Header("Options")]
        [SerializeField] private bool _moveVertical = false;
        [SerializeField] private bool _moveHorizontal = false;



        public Vector3 Scale
        {
            get => _scale;
        }

        public ObstacleType Type
        {
            get => _obstacleType;
        }

        public bool MoveHorizontal
        {
            get => _moveHorizontal;
        }

        public bool MoveVertical
        {
            get => _moveVertical;
        }

        public ObstacleInstanceUnity Prefab
        {
            get => _prefab;
        }

        public enum ObstacleType
        {
            Wall = 0,
            Bottom = 1,
            Top = 2
        }
    }
}
