using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
    [Serializable]
    public class SlopeData
    {
        // Variables
        [field: SerializeField] [field: Range(0f, 1f)] public float StepHeightPercentage { get; private set; } = 0.25f;
    }
}
