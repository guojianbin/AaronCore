using Aaron.Core.Utility.Paging.Config;

namespace Aaron.Core.Utility.Paging
{
    public class PageHelper
    {
        public static int ItemsOnPage(string settingName)
        {
            return new PageSettingContext(settingName).CreateInstance().ItemsOnPage;
        }
    }
}