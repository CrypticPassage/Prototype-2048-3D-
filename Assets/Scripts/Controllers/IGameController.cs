using Signals;

namespace Controllers
{
    public interface IGameController
    {
        void OnCubeItemCollisionWithBorder(SignalCubeItemCollisionWithBorder signal);
        void OnCubeItemCollisionWithOtherCubeItem(SignalCubeItemCollisionWithOtherCubeItem signal);
        
    }
}