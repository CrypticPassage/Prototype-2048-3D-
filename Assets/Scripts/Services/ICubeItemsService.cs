using Objects;
using Signals;

namespace Services
{
    public interface ICubeItemsService
    {
        CubeItem GetCubeItem();
        void RemoveCubeItem(SignalCubeItemMerged signal);
        void RemoveAllCubeItems();
    }
}