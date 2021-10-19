using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ysocorp.obstacle;
using ysocorp.character;

namespace ysocorp.levels
{
    public class LevelInstanceUnity : MonoBehaviour
    {
        [Header("Parameter")]

        [SerializeField] private Transform _ground = default;
        [SerializeField] private Transform _endLine = default;
        [Header("NPC")]
        [SerializeField] private Transform _npcAreacontainer = default;
        [SerializeField] private NPCCharacterAreaInstanceUnity _nPCCharacterAreaInstanceUnityPrefab = default;
        [SerializeField] private Character _characterPrefab = default;
        [Header("Obstacle")]
        [SerializeField] private Transform _obstacleContainer = default;

        private GameManager _gameManager;
        private Level _level;

        public const float POSITION_CHARACTER_Y = 0.5f;
        private const float POSITION_GROUND = -6.0f;

        public Level Level
        {
            get => _level;

        }

        public void Build(Level level, GameManager gameManager)
        {
            _level = level;
            _gameManager = gameManager;
            InstantiateLevelContents();
        }

        private void InstantiateLevelContents()
        {
            InitiateGround();
            InstantiateCharacterNPC();
            InstantiateObstacle();
        }

        private void InitiateGround()
        {
            _ground.localPosition = new Vector3(0, POSITION_GROUND, _level.GroundSize * 0.5f - _gameManager.CameraClipNear * 0.5f);
            _ground.localScale = new Vector3(15, 1f, _level.GroundSize);
            _endLine.localPosition = new Vector3(0, POSITION_GROUND, _level.GroundSize * 0.5f + 40);
        }

        private void InstantiateCharacterNPC()
        {
            foreach (Level.NPCCharacterArea npcArea in _level.NPCCharacter)
            {
                NPCCharacterAreaInstanceUnity area = Instantiate(_nPCCharacterAreaInstanceUnityPrefab, _npcAreacontainer);
                area.transform.localPosition = npcArea.Position;

                CircleSpawner circleSpawner = new CircleSpawner(Vector3.zero);
                Character npcCharacter = Instantiate(_characterPrefab, area.Container);
                npcCharacter.transform.localPosition = new Vector3(0, POSITION_CHARACTER_Y, 0);
                npcCharacter.Build(_gameManager);

                for (int i = 1; i < npcArea.Quantity; i++)
                {
                    npcCharacter = Instantiate(_characterPrefab, area.Container);
                    Vector3 position = circleSpawner.GetNextPosition();
                    position.y = POSITION_CHARACTER_Y;
                    npcCharacter.transform.localPosition = position;
                    npcCharacter.Build(_gameManager);
                }
            }
        }

        private void InstantiateObstacle()
        {
            foreach (Level.ObstacleSerializable obstacle in _level.Obstacles)
            {
                ObstacleInstanceUnity obstacleInstance = Instantiate(obstacle.Prefab, obstacle.Position, transform.rotation, _obstacleContainer);
                obstacleInstance.Build(obstacle.obstacle);
            }
        }

        public void RestartLevel()
        {

            foreach (Character npcCharacter in _npcAreacontainer.GetComponentsInChildren<Character>())
            {
                Destroy(npcCharacter.gameObject);
            }
            InstantiateCharacterNPC();
        }
    }
}
