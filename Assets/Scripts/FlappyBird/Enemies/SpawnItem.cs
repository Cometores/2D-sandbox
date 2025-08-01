﻿using System;
using UnityEngine;

namespace FlappyBird.Enemies
{
    [Serializable]
    public struct SpawnItem
    {
        public GameObject prefab;
        [Min(2)]public int spawnEveryNth;
        public Transform point;
    }
}