using Controllers.Databases;
using Objects;
using UnityEngine;
using Zenject;

namespace Services.Impls
{
    public class ThrowableCubeItemService : MonoBehaviour, IThrowableCubeItemService
    {
        private IGameSettingsDatabase _gameSettingsDatabase;
        private CubeItem _throwableCube;

        [Inject]
        public void Construct(SignalBus signalBus,
            IGameSettingsDatabase gameSettingsDatabase)
        {
            _gameSettingsDatabase = gameSettingsDatabase;
        }

        public void DisableCube()
        {
            if (_throwableCube == null)
                return;
            
            _throwableCube.Rigidbody.constraints = RigidbodyConstraints.None;
            _throwableCube.IsThrown = false;
            _throwableCube = null;
        }

        public void SetCube(CubeItem cube)
        {
            _throwableCube = cube;
            
            _throwableCube.Rigidbody.isKinematic = true; 
            _throwableCube.Rigidbody.useGravity = false;
            _throwableCube.transform.position = _gameSettingsDatabase.GameSettingVo.ThrowableCubeSpawnPosition;
            _throwableCube.transform.rotation = Quaternion.Euler(_gameSettingsDatabase.GameSettingVo.ThrowableCubeSpawnRotationEuler);
        }

        public void MoveCube(Vector3 shift)
        {
            var position = _throwableCube.transform.position;
            var dx = shift.x * _gameSettingsDatabase.GameSettingVo.MoveCubeOffset;
            var borderX = _gameSettingsDatabase.GameSettingVo.ThrowableCubeBorderX;
            var targetX = Mathf.Clamp(position.x + dx, -borderX, borderX);

            _throwableCube.transform.position = new Vector3(targetX, position.y, position.z);
        }
        
        public void ThrowCube(Vector3 direction)
        {
            _throwableCube.Rigidbody.isKinematic = false; 
            _throwableCube.Rigidbody.useGravity = true;
            _throwableCube.Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
            _throwableCube.IsThrown = true;
            _throwableCube.Rigidbody.AddForce(direction.normalized * _gameSettingsDatabase.GameSettingVo.CubeThrowImpulse, ForceMode.Impulse);
        }
    }
}