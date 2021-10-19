using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCharacterAreaInstanceUnity : MonoBehaviour
{
    [SerializeField] private Transform _npcCharacterContainer = default;

    public Transform Container
    {
        get => _npcCharacterContainer;
    }
}
