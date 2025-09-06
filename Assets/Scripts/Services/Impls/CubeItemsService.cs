using Controllers.Databases;
using Factories;
using Objects;
using Pools;
using Signals;
using UnityEngine;
using Zenject;

namespace Services.Impls
{
    public class CubeItemsService : MonoBehaviour, ICubeItemsService
    {
        private IGameSettingsDatabase _gameSettingsDatabase;
        private CubeItemPool _cubeItemPool;

        [Inject]
        public void Construct(IGameSettingsDatabase gameSettingsDatabase, CubeItemPool cubeItemPool)
        {
            _gameSettingsDatabase = gameSettingsDatabase;
            _cubeItemPool = cubeItemPool;
        }

        public void OnCubeItemMerged(SignalCubeItemMerged signal) 
            => _cubeItemPool.Despawn(signal.MergedCubeItem);

        public CubeItem GetCube()
        {
            var newCube = _cubeItemPool.Spawn();
            newCube.Number = 2;
            return newCube;
        }
    }
}