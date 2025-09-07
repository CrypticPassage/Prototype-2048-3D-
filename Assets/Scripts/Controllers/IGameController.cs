using Signals;

namespace Controllers
{
    public interface IGameController
    {
        void OnGameOver();
        void OnCubeItemMerged(SignalCubeItemMerged signal);
        void OnCubeItemCollision(SignalCubeItemCollision signal);
    }
}