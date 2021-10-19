using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ysocorp.character
{
    public class CharacterArea : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager = default;
        [SerializeField] private Transform _characterContainer = default;
        [SerializeField] private BoxCollider _areaCollider = default;

        public static float AREA_X = 6;
        public static float AREA_Z = 7;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider collision)
        {
            Character npcCharacter = collision.GetComponent<Character>();
            if (npcCharacter != null && collision.CompareTag("NPCCharacter"))
            {
                AddCharacterToArea(npcCharacter);
            }
        }

        private void AddCharacterToArea(Character npcCharacter)
        {
            _gameManager.AddCharacter(npcCharacter);
            npcCharacter.transform.SetParent(_characterContainer);
            npcCharacter.transform.localPosition = GetRandomPosition(_areaCollider.size);
            npcCharacter.StartLife();
            npcCharacter.tag = "Character";
        }

        private Vector3 GetRandomPosition(Vector3 size)
        {
            float boundX = size.x * 0.5f;
            float boundsZ = size.z * 0.5f;
            return new Vector3(Random.Range(-boundX, boundX), 1.5f, Random.Range(-boundsZ, boundsZ));
        }
    }
}
