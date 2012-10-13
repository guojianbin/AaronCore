using Aaron.Core.Utility.Setting;

namespace Aaron.Core.Utility.Paging.Config
{
    public class PageSettingContext
    {
        ISettingHelper<IPageSetting> _pageSetting;

        public PageSettingContext(string settingName)
        {
            _pageSetting = new SettingHelper<IPageSetting>(settingName);
        }

        public IPageSetting CreateInstance()
        {
            return _pageSetting.Setting();
        }

        public void Save()
        {
            _pageSetting.Save();
        }
    }
}
