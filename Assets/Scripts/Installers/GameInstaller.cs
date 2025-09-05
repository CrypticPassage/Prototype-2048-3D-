using Controllers;
using Controllers.Impls;
using Factories;
using Objects;
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
            Container.DeclareSignal<SignalThrowedCubeCollissionStart>();
        }

        private void BindSignals()
        {
            Container.BindSignal<SignalThrowedCubeCollissionStart>()
                .ToMethod<IGameController>(x => x.OnCubeThrowCollisionStart).FromResolve();
        }

        private void BindFactories()
        {
            Container.BindFactory<CubeItem, CubeItemFactory>()
                .FromComponentInNewPrefab(cubeItem)
                .AsTransient();
        }
        
        private void BindControllers()
        {
            Container.Bind<IGameController>().To<GameController>().FromNewComponentOn(scriptsTransform.gameObject).AsSingle().NonLazy();
        }

        private void BindServices()
        {
            Container.Bind<ICubeItemsService>().To<CubeItemsService>().FromNewComponentOn(scriptsTransform.gameObject).AsSingle().NonLazy();
            Container.Bind<IThrowableCubeItemService>().To<ThrowableCubeItemService>().FromNewComponentOn(scriptsTransform.gameObject).AsSingle().NonLazy();
            Container.Bind<IInputService>().To<InputService>().FromNewComponentOn(scriptsTransform.gameObject).AsSingle().NonLazy();
        }
    }
}