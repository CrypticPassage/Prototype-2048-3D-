using Controllers.Databases;
using Objects;
using Signals;
using UnityEngine;
using Zenject;

namespace Services.Impls
{
    /// <summary>
    /// Даний сервіс відповідає за взаємодію кубиків у грі. Якщо кубики сходяться за числами, то тоді виконується їх злиття.
    /// Злиття виконується за рахунок того, що один з кубиків оновлює свої значення, а інший деактивується в пулі.
    /// </summary>
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
            if (firstCubeItem.Number != secondCubeItem.Number 
                || forceImpact < _gameSettingsDatabase.GameSettingVo.MinimalForceImpactToMergeCubes 
                || firstCubeItem.IsThrowable || secondCubeItem.IsThrowable) 
                return;
            
            var newCubeItemNumber = firstCubeItem.Number + secondCubeItem.Number;
            var newCubeItemColor = _colorSettingsDatabase.GetColorSettingByNumber(newCubeItemNumber).Color;

            firstCubeItem.SetData(newCubeItemNumber, newCubeItemColor);
            firstCubeItem.Rigidbody.AddForce(Vector3.up * _gameSettingsDatabase.GameSettingVo.CubeJumpForce, ForceMode.Impulse);
            
            _signalBus.Fire(new SignalCubeItemMerged(firstCubeItem, secondCubeItem));
        }
    }
}