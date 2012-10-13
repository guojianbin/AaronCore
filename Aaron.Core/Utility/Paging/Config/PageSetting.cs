using System.Configuration;
using Aaron.Core.Utility.Setting;

namespace Aaron.Core.Utility.Paging.Config
{
    public class PageSetting : ConfigurationSection, IPageSetting
    {
        [ConfigurationProperty("itemsOnPage", IsRequired = true)]
        public int ItemsOnPage
        {
            get
            {
                return (int)this["itemsOnPage"];
            }
            set
            {
                this["itemsOnPage"] = value;
            }
        }
    }
}
