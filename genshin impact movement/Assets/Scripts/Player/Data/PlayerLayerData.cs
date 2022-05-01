using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GenshinImpactMovementSystem
{
    [Serializable]
    public class PlayerLayerData
    {
        [field: SerializeField] public LayerMask GroundLayer { get; private set; }
    }
}
