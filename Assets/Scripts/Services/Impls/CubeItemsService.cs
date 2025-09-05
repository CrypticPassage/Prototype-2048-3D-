using Controllers.Databases;
using Factories;
using Objects;
using Pools;
using UnityEngine;
using Zenject;

namespace Services.Impls
{
    public class CubeItemsService : MonoBehaviour, ICubeItemsService
    {
        private IGameSettingsDatabase _gameSettingsDatabase;
        private PoolBase<CubeItem> _cubeItemsPool;
        private CubeItemFactory _cubeItemFactory;

        [Inject]
        public void Construct(IGameSettingsDatabase gameSettingsDatabase, 
            CubeItemFactory cubeItemFactory)
        {
            _gameSettingsDatabase = gameSettingsDatabase;
            _cubeItemFactory = cubeItemFactory;
            
            _cubeItemsPool = new PoolBase<CubeItem>(
                PreloadCubeItem, GetActionCubeItem, ReturnActionCubeItem, _gameSettingsDatabase.GameSettingVo.CubesAmountForPool);
        }
        
        public CubeItem GetCube()
        {
            return _cubeItemsPool.Get();
        }
        
        public CubeItem PreloadCubeItem() => _cubeItemFactory.Create();

        public void GetActionCubeItem(CubeItem cubeItem) => cubeItem.gameObject.SetActive(true);

        public void ReturnActionCubeItem(CubeItem cubeItem) => cubeItem.gameObject.SetActive(false);
    }
}