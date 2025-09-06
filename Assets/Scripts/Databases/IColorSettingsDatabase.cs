using Models;

namespace Controllers.Databases
{
    public interface IColorSettingsDatabase
    {
        ColorSettingVo GetColorSettingByNumber(int number);
    }
}