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
        public float CubeWithTwoSpawnChance;
        public int CubeNumberTwo;
        public int CubeNumberFour;
        public int MaxCubeNumber;
        public float ThrowableCubeBorderX;
        public Vector3 ThrowableCubeSpawnPosition;
        public Vector3 ThrowableCubeSpawnRotationEuler;
        [Header("Click Data")] 
        public float ClickPositionMaxYToThrow;
    }
}