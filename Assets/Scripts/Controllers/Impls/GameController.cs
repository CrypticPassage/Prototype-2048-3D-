using Controllers.Databases;
using Objects;
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

        private GameObject _frontBorder;
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
            GameObject frontBorder,
            [Inject(Id = ZenjectUids.Score)] TMP_Text scoreAmountText,
            [Inject(Id = ZenjectUids.Win)] TMP_Text winText,
            Button replayButton, 
            IGameSettingsDatabase gameSettingsDatabase)
        {
            _cubeItemsService = cubeItemsService;
            _cubeItemsInteractService = cubeItemsInteractService;
            _throwableCubeItemService = throwableCubeItemService;
            _inputService = inputService;
            _frontBorder = frontBorder;
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
            _cubeItemsService.RemoveCubeItem(signal);
        }
        
        public void OnCubeItemCollision(SignalCubeItemCollision signal)
        {
            if (signal.Collision.collider.gameObject == _frontBorder && signal.CubeItemThatEnteredCollision.IsThrown && !_isGameOver)
            {
                SetNewThrowableCubeItem();
                return;
            }

            var otherCubeItem = signal.Collision.collider.GetComponent<CubeItem>();
            
            if (otherCubeItem == null)
                return;
            
            var impactForce = signal.Collision.impulse.magnitude / Time.fixedDeltaTime;
            
            if (signal.CubeItemThatEnteredCollision.IsThrown && !_isGameOver)
                SetNewThrowableCubeItem();

            _cubeItemsInteractService.MergeCubeItems(signal.CubeItemThatEnteredCollision, otherCubeItem, impactForce);
        }

        private void SetNewThrowableCubeItem()
        {
            _throwableCubeItemService.ResetCubeItem();
            _throwableCubeItemService.SetCubeItem(_cubeItemsService.GetCubeItem());
            _isInputAble = true;
        }

        private void Start()
        {
            _replayButton.onClick.AddListener(OnReplayButtonClick);
            
            _throwableCubeItemService.SetCubeItem(_cubeItemsService.GetCubeItem());
        }
        
        private void Update()
        {
            if (!_isInputAble)
                return;
            
            if (_inputService.IsClickHeld())
            {
                var clickScreenDelta = _inputService.GetClickDelta();
                
                _throwableCubeItemService.MoveCubeItem(new Vector3(clickScreenDelta.x, 0f, 0f));
            }

            if (_inputService.IsClickUp())
            {
                var clickPositionOnScreen = _inputService.GetClickPositionOnScreen();
                
                if (clickPositionOnScreen.y > _gameSettingsDatabase.GameSettingVo.ClickPositionMaxYToThrow)
                    return;
                    
                _isInputAble = false;
                _throwableCubeItemService.ThrowCubeItem(Vector3.forward);
            }
        }

        private void OnReplayButtonClick()
        {
            _scoreAmountText.text = "0";
            _winText.gameObject.SetActive(false);
            _throwableCubeItemService.ResetCubeItem();
            _cubeItemsService.RemoveAllCubeItems();
            _throwableCubeItemService.SetCubeItem(_cubeItemsService.GetCubeItem());
            _isGameOver = false;
            _isInputAble = true;
        }
    }
}