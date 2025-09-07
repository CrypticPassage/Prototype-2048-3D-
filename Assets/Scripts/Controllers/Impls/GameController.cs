using Controllers.Databases;
using Services;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controllers.Impls
{
    public class GameController : MonoBehaviour, IGameController
    {
        private SignalBus _signalBus;
        private ICubeItemsService _cubeItemsService;
        private ICubeItemsInteractService _cubeItemsInteractService;
        private IThrowableCubeItemService _throwableCubeItemService;
        private IInputService _inputService;
        private IGameSettingsDatabase _gameSettingsDatabase;

        private TMP_Text _scoreAmountText;
        private Button _replayButton;
        private bool _isInputAble = true;
        
        [Inject]
        public void Construct(SignalBus signalBus,
            ICubeItemsService cubeItemsService,
            ICubeItemsInteractService cubeItemsInteractService,
            IThrowableCubeItemService throwableCubeItemService,
            IInputService inputService,
            TMP_Text scoreAmountText,
            Button replayButton, 
            IGameSettingsDatabase gameSettingsDatabase)
        {
            _signalBus = signalBus;
            _cubeItemsService = cubeItemsService;
            _cubeItemsInteractService = cubeItemsInteractService;
            _throwableCubeItemService = throwableCubeItemService;
            _inputService = inputService;
            _scoreAmountText = scoreAmountText;
            _replayButton = replayButton;
            _gameSettingsDatabase = gameSettingsDatabase;
        }

        public void OnCubeItemMerged(SignalCubeItemMerged signal)
        {
            var scoreAmount = int.Parse(_scoreAmountText.text);

            scoreAmount += 1;
            _scoreAmountText.text = scoreAmount.ToString();
            
            _cubeItemsService.OnCubeItemMerged(signal);
        }

        public void OnCubeItemCollisionWithBorder(SignalCubeItemCollisionWithBorder signal)
        {
            if (signal.CubeItemThatEnteredCollision.IsThrown)
                SetNewThrowableCube();
        }
        
        public void OnCubeItemCollisionWithOtherCubeItem(SignalCubeItemCollisionWithOtherCubeItem signal)
        {
            if (signal.CubeItemThatEnteredCollision.IsThrown)
                SetNewThrowableCube();

            _cubeItemsInteractService.MergeCubeItems(signal.CubeItemThatEnteredCollision, signal.OtherCubeItem, signal.ImpactForce);
        }

        private void SetNewThrowableCube()
        {
            _throwableCubeItemService.DisableCube();
            _throwableCubeItemService.SetCube(_cubeItemsService.GetCube()); 
            _isInputAble = true;
        }

        private void Start()
        {
            _replayButton.onClick.AddListener(OnReplayButtonClick);
            
            _throwableCubeItemService.SetCube(_cubeItemsService.GetCube());
        }
        
        private void Update()
        {
            if (!_isInputAble)
                return;
            
            if (_inputService.IsClickHeld())
            {
                var clickScreenDelta = _inputService.GetClickDelta();
                
                _throwableCubeItemService.MoveCube(new Vector3(clickScreenDelta.x, 0f, 0f));
            }

            if (_inputService.IsClickUp())
            {
                var clickPositionOnScreen = _inputService.GetClickPositionOnScreen();
                
                if (clickPositionOnScreen.y > _gameSettingsDatabase.GameSettingVo.ClickPositionMaxYToThrow)
                    return;
                    
                _isInputAble = false;
                _throwableCubeItemService.ThrowCube(Vector3.forward);
            }
        }

        private void OnReplayButtonClick()
        {
            _scoreAmountText.text = "0";
            _throwableCubeItemService.DisableCube();
            _cubeItemsService.RemoveAllCubeItems();
            _throwableCubeItemService.SetCube(_cubeItemsService.GetCube());
            _isInputAble = true;
        }
    }
}