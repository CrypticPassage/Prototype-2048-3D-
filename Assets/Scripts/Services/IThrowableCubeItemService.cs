using Objects;
using UnityEngine;

namespace Services
{
    public interface IThrowableCubeItemService
    {
        void ResetCubeItem();
        void SetCubeItem(CubeItem cube);
        void MoveCubeItem(Vector3 shift);
        void ThrowCubeItem(Vector3 direction);
    }
}