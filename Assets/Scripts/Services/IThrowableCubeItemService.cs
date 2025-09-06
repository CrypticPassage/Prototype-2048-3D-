using Objects;
using UnityEngine;

namespace Services
{
    public interface IThrowableCubeItemService
    {
        void DisableCube();
        void SetCube(CubeItem cube);
        void MoveCube(Vector3 shift);
        void ThrowCube(Vector3 direction);
    }
}