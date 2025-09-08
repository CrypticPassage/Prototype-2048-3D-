using Signals;

namespace Controllers
{
    public interface IGameController
    {
        void OnCubeItemMerged(SignalCubeItemMerged signal);
        void OnCubeItemCollision(SignalCubeItemCollision signal);
    }
}