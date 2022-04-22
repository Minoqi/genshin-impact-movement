using System;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
    [Serializable]
    public class PlayerGroundedData
    {
        [SerializeField] [field: Range(0f, 25f)] public float baseSpeed { get; private set; } = 5f;
        [SerializeField] public PlayerStateRotationData BaseRotationData { get; private set; }
        [SerializeField] public PlayerWalkData WalkData { get; private set; }
        [SerializeField] public PlayerRunData RunData { get; private set; }


    }
}
