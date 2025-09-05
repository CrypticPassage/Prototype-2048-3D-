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

        public void SetThrowableCube(CubeItem cube)
        {
            _throwableCube = cube;
            _throwableCube.transform.position = _gameSettingsDatabase.GameSettingVo.ThrowableCubeSpawnPosition;
        }

        public void MoveCube(Vector3 shift)
        {
            _throwableCube.gameObject.transform.position += shift * _gameSettingsDatabase.GameSettingVo.MoveCubeOffset;
        }
        
        public void ThrowCube(Vector3 direction)
        {
            _throwableCube.Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;

            _throwableCube.IsThrown = true;
            _throwableCube.Rigidbody.AddForce(direction.normalized * _gameSettingsDatabase.GameSettingVo.CubeThrowImpulse, ForceMode.Impulse);
        }
    }
}