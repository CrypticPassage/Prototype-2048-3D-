using Controllers;
using Controllers.Impls;
using Factories;
using Objects;
using Pools;
using Services;
using Services.Impls;
using Signals;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Header("Items")]
        [SerializeField] private CubeItem cubeItem;
        [Header ("Other")]
        [SerializeField] private Transform scriptsTransform;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            DeclareSignals();
            BindFactories();
            BindServices();
            BindControllers();
            BindSignals();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<SignalCubeItemCollisionWithBorder>();
            Container.DeclareSignal<SignalCubeItemCollisionWithOtherCubeItem>();
            Container.DeclareSignal<SignalCubeItemMerged>();
        }

        private void BindSignals()
        {
            Container.BindSignal<SignalCubeItemCollisionWithBorder>()
                .ToMethod<IGameController>(x => x.OnCubeItemCollisionWithBorder).FromResolve();
            Container.BindSignal<SignalCubeItemCollisionWithOtherCubeItem>()
                .ToMethod<IGameController>(x => x.OnCubeItemCollisionWithOtherCubeItem).FromResolve();
            Container.BindSignal<SignalCubeItemMerged>()
                .ToMethod<ICubeItemsService>(x => x.OnCubeItemMerged).FromResolve();
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