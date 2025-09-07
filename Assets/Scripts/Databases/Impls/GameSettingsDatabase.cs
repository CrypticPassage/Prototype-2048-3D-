using Models;
using UnityEngine;

namespace Controllers.Databases.Impls
{
    /// <summary>
    /// Дана База Даних зберігає різноманітні конфігураційні значення гри, пов'язані в основному з Кубом та його взаємодією.
    /// Хардкод винесений в Дану Базу Даних.
    /// </summary>
    [CreateAssetMenu(menuName = "Databases/GameSettingsDatabase", fileName = "GameSettingsDatabase")] 
    public class GameSettingsDatabase : ScriptableObject, IGameSettingsDatabase
    {
        [SerializeField] private GameSettingVo gameSettingVo;

        public GameSettingVo GameSettingVo => gameSettingVo;
    }
}