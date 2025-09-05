using Models;
using UnityEngine;

namespace Controllers.Databases.Impls
{
    [CreateAssetMenu(menuName = "Databases/GameSettingsDatabase", fileName = "GameSettingsDatabase")] 
    public class GameSettingsDatabase : ScriptableObject, IGameSettingsDatabase
    {
        [SerializeField] private GameSettingVo gameSettingVo;

        public GameSettingVo GameSettingVo => gameSettingVo;
    }
}