using Controllers.Databases;
using Controllers.Databases.Impls;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Installers/DatabasesInstaller", fileName = "DatabasesInstaller")]
    public class DatabasesInstaller : ScriptableObjectInstaller
    { 
        [SerializeField] private GameSettingsDatabase gameSettingsDatabase;
        [SerializeField] private ColorSettingsDatabase colorSettingsDatabase;

        public override void InstallBindings()
        {
            Container.Bind<IGameSettingsDatabase>().FromInstance(gameSettingsDatabase).AsSingle();
            Container.Bind<IColorSettingsDatabase>().FromInstance(colorSettingsDatabase).AsSingle();
        }
    }
}