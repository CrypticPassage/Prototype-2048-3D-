using Objects;
using Signals;

namespace Services
{
    public interface ICubeItemsService
    {
        void OnCubeItemMerged(SignalCubeItemMerged signal);
        CubeItem GetCube();
        void RemoveAllCubeItems();
    }
}