using Controllers.Databases;
using Factories;
using Models;
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
        private IColorSettingsDatabase _colorSettingsDatabase;
        private GameSettingVo _gameSettingVo;
        private PoolBase<CubeItem> _cubeItemsPool;
        private CubeItemFactory _cubeItemFactory;

        [Inject]
        public void Construct(IGameSettingsDatabase gameSettingsDatabase,
            IColorSettingsDatabase colorSettingsDatabase,
            CubeItemFactory cubeItemFactory)
        {
            _gameSettingsDatabase = gameSettingsDatabase;
            _gameSettingVo = _gameSettingsDatabase.GameSettingVo;
            _colorSettingsDatabase = colorSettingsDatabase;
            _cubeItemFactory = cubeItemFactory;
            _cubeItemsPool = new PoolBase<CubeItem>(
                PreloadCubeItem, GetActionCubeItem, ReturnActionCubeItem, _gameSettingsDatabase.GameSettingVo.CubesAmountForPool);
        }

        public void OnCubeItemMerged(SignalCubeItemMerged signal) 
            => ReturnActionCubeItem(signal.MergedCubeItem);

        public CubeItem GetCube()
        {
            var cubeNumber = Random.value <= _gameSettingVo.CubeWithTwoSpawnChance ? _gameSettingVo.CubeNumberTwo
                : _gameSettingVo.CubeNumberFour;
            var color = _colorSettingsDatabase.GetColorSettingByNumber(cubeNumber).Color;
            var newCube = _cubeItemsPool.Get();
            
            newCube.SetData(cubeNumber, color);
            
            return newCube;
        }
        
        public void RemoveAllCubeItems()
        {
            _cubeItemsPool.ReturnAll();
        }

        private CubeItem PreloadCubeItem() => _cubeItemFactory.Create();

        private void GetActionCubeItem(CubeItem cubeItem) => cubeItem.gameObject.SetActive(true);

        private void ReturnActionCubeItem(CubeItem cubeItem) => cubeItem.gameObject.SetActive(false);
    }
}