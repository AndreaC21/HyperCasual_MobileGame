using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ysocorp.obstacle;
namespace ysocorp.levels
{

    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
    public class Level : ScriptableObject
    {
        [SerializeField] private int _characterOnStart = 1;
        [SerializeField] private ObstacleSerializable[] _obstacle = default;
        [SerializeField] private NPCCharacterArea[] _npcCharacterArea = default;
        [SerializeField] private float _groundSize = 300.0f;

        public int CharacterOnStart
        {
            get => _characterOnStart;
        }

        public float GroundSize
        {
            get => _groundSize;
        }

        public ObstacleSerializable[] Obstacles
        {
            get => _obstacle;
        }

        public NPCCharacterArea[] NPCCharacter
        {
            get => _npcCharacterArea;
        }


        [Serializable]
        public class NPCCharacterArea
        {
            [SerializeField] private int _quantityCharacter;
            [SerializeField] private Vector3 _position;

            public int Quantity
            {
                get => _quantityCharacter;
            }

            public Vector3 Position
            {
                get => _position;
            }
        }

        [Serializable]
        public class ObstacleSerializable
        {
            [SerializeField] private Obstacle _obstacle;
            [SerializeField] private Vector3 _position;

            public Obstacle obstacle
            {
                get => _obstacle;
            }

            public ObstacleInstanceUnity Prefab
            {
                get => _obstacle.Prefab;
            }

            public Vector3 Position
            {
                get => _position;
            }
        }
    }
}
