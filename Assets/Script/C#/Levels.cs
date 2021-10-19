using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ysocorp.levels
{
    [CreateAssetMenu(fileName = "Levels", menuName = "ScriptableObjects/Levels", order = 1)]

    public class Levels : ScriptableObject
    {
        [SerializeField] private Level[] _level = default;

        public Level GetLevel(int index)
        {
            if (index < 0 || index >= _level.Length)
            {
                Debug.LogError("Try to load level beyond capacity");
                return null;
            }
            return _level[index];
        }

        public int Size
        {
            get => _level.Length;
        }
    }
}
