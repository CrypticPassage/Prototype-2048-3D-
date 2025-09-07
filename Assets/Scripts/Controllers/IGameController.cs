using Signals;

namespace Controllers
{
    public interface IGameController
    {
        void OnCubeItemMerged(SignalCubeItemMerged signal);
        void OnCubeItemCollisionWithBorder(SignalCubeItemCollisionWithBorder signal);
        void OnCubeItemCollisionWithOtherCubeItem(SignalCubeItemCollisionWithOtherCubeItem signal);
        
    }
}