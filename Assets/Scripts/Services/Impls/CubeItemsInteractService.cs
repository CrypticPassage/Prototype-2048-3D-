using Controllers.Databases;
using Objects;
using Signals;
using UnityEngine;
using Zenject;

namespace Services.Impls
{
    public class CubeItemsInteractService : MonoBehaviour, ICubeItemsInteractService
    {
        private SignalBus _signalBus;
        private IGameSettingsDatabase _gameSettingsDatabase;
        private IColorSettingsDatabase _colorSettingsDatabase;
        
        [Inject]
        public void Construct(SignalBus signalBus,
            IGameSettingsDatabase gameSettingsDatabase,
            IColorSettingsDatabase colorSettingsDatabase)
        {
            _signalBus = signalBus;
            _gameSettingsDatabase = gameSettingsDatabase;
            _colorSettingsDatabase = colorSettingsDatabase;
        }

        public void MergeCubeItems(CubeItem firstCubeItem, CubeItem secondCubeItem, float forceImpact)
        {
            if (firstCubeItem.Number != secondCubeItem.Number || forceImpact < _gameSettingsDatabase.GameSettingVo.MinimalForceImpactToMergeCubes)
                return;

            var newCubeItemNumber = firstCubeItem.Number + secondCubeItem.Number;
            var newCubeItemColor = _colorSettingsDatabase.GetColorSettingByNumber(newCubeItemNumber).Color;

            firstCubeItem.SetData(newCubeItemNumber, newCubeItemColor);
            firstCubeItem.Rigidbody.AddForce(Vector3.up * _gameSettingsDatabase.GameSettingVo.CubeJumpForce, ForceMode.Impulse);
            
            _signalBus.Fire(new SignalCubeItemMerged(secondCubeItem));
            
            if (firstCubeItem.Number >= _gameSettingsDatabase.GameSettingVo.MaxCubeNumber)
                _signalBus.Fire<SignalGameOver>();
        }
    }
}