using System;
using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Controllers.Databases.Impls
{
    /// <summary>
    /// Дана База Даних зберігає кольори кубів в залежності від номеру куба.
    /// </summary>
    [CreateAssetMenu(menuName = "Databases/ColorSettingsDatabase", fileName = "ColorSettingsDatabase")] 
    public class ColorSettingsDatabase : ScriptableObject, IColorSettingsDatabase
    {
        [SerializeField] private ColorSettingVo[] _colorSettings;
        private Dictionary<int, ColorSettingVo> _colorSettingsDictionary;
        
        private void OnEnable() 
        { 
            _colorSettingsDictionary = new Dictionary<int, ColorSettingVo>();
            
            foreach (var colorSettingVo in _colorSettings) 
                _colorSettingsDictionary.Add(colorSettingVo.Number, colorSettingVo);
        }
        
        public ColorSettingVo GetColorSettingByNumber(int number)
        { 
            try 
            { 
                return _colorSettingsDictionary[number];
            }
            catch (Exception e) 
            { 
                throw new Exception(
                    $"[{nameof(ColorSettingsDatabase)}] ColorSettingVo by id {number} is not present in the dictionary. {e.StackTrace}"); 
            } 
        } 
    }
}