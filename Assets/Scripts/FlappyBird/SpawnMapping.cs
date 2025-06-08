using System;
using UnityEngine;

namespace FlappyBird
{
    [Serializable]
    public struct SpawnMapping
    {
        public GameObject prefab;
        public int spawnEveryNth;
        public Transform point;
    }
}