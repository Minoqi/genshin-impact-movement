using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
    [Serializable]
    public class DefaultColliderData
    {
        // Variables
        [field: SerializeField] public float Height { get; private set; } = 1.8f;
        [field: SerializeField] public float CenterY { get; private set; } = 0.9f; // Half of the height
        [field: SerializeField] public float Radius { get; private set; } = 0.2f;
    }
}
