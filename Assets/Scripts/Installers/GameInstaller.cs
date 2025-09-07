using Controllers;
using Controllers.Impls;
using Factories;
using Objects;
using Services;
using Services.Impls;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Header("Objects & Items")] 
        [SerializeField] private GameObject frontBorder;
        [SerializeField] private CubeItem cubeItem;
        [Header("Canvas")] 
        [SerializeField] private TMP_Text scoreAmountText;
        [SerializeField] private TMP_Text winText;
        [SerializeField] private Button replayButton;
        [Header ("Other")]
        [SerializeField] private Transform scriptsTransform;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            DeclareSignals();
            BindUi();
            BindObjects();
            BindFactories();
            BindServices();
            BindControllers();
            BindSignals();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<SignalCubeItemCollision>();
            Container.DeclareSignal<SignalCubeItemMerged>();
            Container.DeclareSignal<SignalGameOver>();
        }
        
        private void BindSignals()
        {
            Container.BindSignal<SignalCubeItemCollision>()
                .ToMethod<IGameController>(x => x.OnCubeItemCollision).FromResolve();
            Container.BindSignal<SignalCubeItemMerged>()
                .ToMethod<IGameController>(x => x.OnCubeItemMerged).FromResolve();
            Container.BindSignal<SignalGameOver>()
                .ToMethod<IGameController>(x => x.OnGameOver).FromResolve();
        }
        
        private void BindUi()
        {
            Container.BindInstance(scoreAmountText).WithId(ZenjectUids.Score);
            Container.BindInstance(winText).WithId(ZenjectUids.Win);
            Container.Bind<Button>().FromInstance(replayButton).AsSingle();
        }
        
        private void BindObjects()
        {
            Container.Bind<GameObject>().FromInstance(frontBorder).AsSingle();
        }
        
        private void BindFactories()
        {
            Container.BindFactory<CubeItem, CubeItemFactory>().FromComponentInNewPrefab(cubeItem).AsTransient();
        }
        
        private void BindControllers()
        {
            Container.Bind<IGameController>().To<GameController>().FromNewComponentOn(scriptsTransform.gameObject).AsSingle().NonLazy();
        }

        private void BindServices()
        {
            Container.Bind<ICubeItemsService>().To<CubeItemsService>().FromNewComponentOn(scriptsTransform.gameObject).AsSingle().NonLazy();
            Container.Bind<ICubeItemsInteractService>().To<CubeItemsInteractService>().FromNewComponentOn(scriptsTransform.gameObject).AsSingle().NonLazy();
            Container.Bind<IThrowableCubeItemService>().To<ThrowableCubeItemService>().FromNewComponentOn(scriptsTransform.gameObject).AsSingle().NonLazy();
            Container.Bind<IInputService>().To<InputService>().FromNewComponentOn(scriptsTransform.gameObject).AsSingle().NonLazy();
        }
    }
}