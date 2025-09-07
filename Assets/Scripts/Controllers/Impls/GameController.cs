using Controllers.Databases;
using Services;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zenject;

namespace Controllers.Impls
{
    public class GameController : MonoBehaviour, IGameController
    {
        private ICubeItemsService _cubeItemsService;
        private ICubeItemsInteractService _cubeItemsInteractService;
        private IThrowableCubeItemService _throwableCubeItemService;
        private IInputService _inputService;
        private IGameSettingsDatabase _gameSettingsDatabase;

        private TMP_Text _scoreAmountText;
        private TMP_Text _winText;
        private Button _replayButton;
        private bool _isGameOver;
        private bool _isInputAble = true;
        
        [Inject]
        public void Construct(ICubeItemsService cubeItemsService,
            ICubeItemsInteractService cubeItemsInteractService,
            IThrowableCubeItemService throwableCubeItemService,
            IInputService inputService,
            [Inject(Id = ZenjectUids.Score)] TMP_Text scoreAmountText,
            [Inject(Id = ZenjectUids.Win)] TMP_Text winText,
            Button replayButton, 
            IGameSettingsDatabase gameSettingsDatabase)
        {
            _cubeItemsService = cubeItemsService;
            _cubeItemsInteractService = cubeItemsInteractService;
            _throwableCubeItemService = throwableCubeItemService;
            _inputService = inputService;
            _scoreAmountText = scoreAmountText;
            _winText = winText;
            _replayButton = replayButton;
            _gameSettingsDatabase = gameSettingsDatabase;
        }

        public void OnGameOver()
        {
            _winText.gameObject.SetActive(true);
            _isGameOver = true;
            _isInputAble = false;
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
            if (signal.CubeItemThatEnteredCollision.IsThrown && !_isGameOver)
                SetNewThrowableCube();
        }
        
        public void OnCubeItemCollisionWithOtherCubeItem(SignalCubeItemCollisionWithOtherCubeItem signal)
        {
            if (signal.CubeItemThatEnteredCollision.IsThrown && !_isGameOver)
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
            _winText.gameObject.SetActive(false);
            _throwableCubeItemService.DisableCube();
            _cubeItemsService.RemoveAllCubeItems();
            _throwableCubeItemService.SetCube(_cubeItemsService.GetCube());
            _isGameOver = false;
            _isInputAble = true;
        }
    }
}