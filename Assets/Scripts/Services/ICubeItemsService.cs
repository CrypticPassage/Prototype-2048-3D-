using Objects;
using Signals;

namespace Services
{
    public interface ICubeItemsService
    {
        CubeItem GetCubeItem();
        void RemoveCubeItem(CubeItem cubeItem);
        void RemoveAllCubeItems();
    }
}