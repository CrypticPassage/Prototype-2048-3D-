using Controllers.Databases;
using Objects;
using UnityEngine;
using Zenject;

namespace Services.Impls
{
    public class ThrowableCubeItemService : MonoBehaviour, IThrowableCubeItemService
    {
        private IGameSettingsDatabase _gameSettingsDatabase;
        private CubeItem _throwableCubeItem;

        [Inject]
        public void Construct(SignalBus signalBus,
            IGameSettingsDatabase gameSettingsDatabase)
        {
            _gameSettingsDatabase = gameSettingsDatabase;
        }
        
        public void ResetCubeItem()
        {
            if (_throwableCubeItem == null)
                return;
            
            _throwableCubeItem.Rigidbody.constraints = RigidbodyConstraints.None;
            _throwableCubeItem.IsThrown = false;
            _throwableCubeItem = null;
        }
        
        public void SetCubeItem(CubeItem cube)
        {
            _throwableCubeItem = cube;
            
            _throwableCubeItem.Rigidbody.isKinematic = true; 
            _throwableCubeItem.Rigidbody.useGravity = false;
            _throwableCubeItem.transform.position = _gameSettingsDatabase.GameSettingVo.ThrowableCubeSpawnPosition;
            _throwableCubeItem.transform.rotation = Quaternion.Euler(_gameSettingsDatabase.GameSettingVo.ThrowableCubeSpawnRotationEuler);
        }

        public void MoveCubeItem(Vector3 shift)
        {
            if (_throwableCubeItem == null)
                return;
            
            var position = _throwableCubeItem.transform.position;
            var dx = shift.x * _gameSettingsDatabase.GameSettingVo.MoveCubeOffset;
            var borderX = _gameSettingsDatabase.GameSettingVo.ThrowableCubeBorderX;
            var targetX = Mathf.Clamp(position.x + dx, -borderX, borderX);

            _throwableCubeItem.transform.position = new Vector3(targetX, position.y, position.z);
        }
        
        public void ThrowCubeItem(Vector3 direction)
        {
            if (_throwableCubeItem == null)
                return;
            
            _throwableCubeItem.Rigidbody.isKinematic = false; 
            _throwableCubeItem.Rigidbody.useGravity = true;
            _throwableCubeItem.Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
            _throwableCubeItem.IsThrown = true;
            _throwableCubeItem.Rigidbody.AddForce(direction.normalized * _gameSettingsDatabase.GameSettingVo.CubeThrowImpulse, ForceMode.Impulse);
        }
    }
}