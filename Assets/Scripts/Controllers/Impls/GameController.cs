using Controllers.Databases;
using Services;
using UnityEngine;
using Zenject;

namespace Controllers.Impls
{
    public class GameController : MonoBehaviour, IGameController
    {
        private SignalBus _signalBus;
        private ICubeItemsService _cubeItemsService;
        private IThrowableCubeItemService _throwableCubeItemService;
        private IInputService _inputService;
        private IGameSettingsDatabase _gameSettingsDatabase;

        private bool _isInputAble = true;
        
        [Inject]
        public void Construct(SignalBus signalBus,
            ICubeItemsService cubeItemsService,
            IThrowableCubeItemService throwableCubeItemService,
            IInputService inputService,
            IGameSettingsDatabase gameSettingsDatabase)
        {
            _signalBus = signalBus;
            _cubeItemsService = cubeItemsService;
            _throwableCubeItemService = throwableCubeItemService;
            _inputService = inputService;
            _gameSettingsDatabase = gameSettingsDatabase;
        }

        public void OnCubeThrowCollisionStart()
        {
            _throwableCubeItemService.SetThrowableCube(_cubeItemsService.GetCube());
            _isInputAble = true;
        }

        private void Start()
        {
            _throwableCubeItemService.SetThrowableCube(_cubeItemsService.GetCube());
        }

        private void Update()
        {
            if (!_isInputAble)
                return;
            
            if (_inputService.IsClickHeld())
            {
                var clickScreenPosition = _inputService.GetClickDelta();
                
                _throwableCubeItemService.MoveCube(new Vector3(clickScreenPosition.x, 0f, 0f));
            }

            if (_inputService.IsClickUp())
            {
                _isInputAble = false;
                _throwableCubeItemService.ThrowCube(Vector3.forward);
            }
        }
    }
}