using Models;

namespace Controllers.Databases
{
    public interface IGameSettingsDatabase
    {
        GameSettingVo GameSettingVo { get; }
    }
}