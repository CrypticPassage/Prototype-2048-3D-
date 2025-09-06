using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class GameSettingVo
    {
        [Header("Cubes Data")]
        public int CubesAmountForPool;
        public float CubeThrowImpulse;
        public float MoveCubeOffset;
        public float MinimalForceImpactToMergeCubes;
        public float CubeJumpForce;
        public Vector3 ThrowableCubeSpawnPosition;
    }
}