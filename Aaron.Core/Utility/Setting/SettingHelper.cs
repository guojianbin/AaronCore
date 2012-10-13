using System.Configuration;
using System.Web.Configuration;

namespace Aaron.Core.Utility.Setting
{
    public class SettingHelper<T> : ConfigurationSection, ISettingHelper<T> where T : class
    {
        private Configuration config;
        private string _settingName;

        public SettingHelper(string settingName)
        {
            _settingName = settingName;
            config = WebConfigurationManager.OpenWebConfiguration("~");
        }

        public T Setting()
        {
            return config.GetSection(_settingName) as T;
        }

        public void Save()
        {
            config.Save(ConfigurationSaveMode.Modified, true);
        }
    }
}