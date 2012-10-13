using System;
using System.Configuration;
using Aaron.Core.Utility.Setting;

namespace Aaron.Core.Utility.Image.Config
{
    public class HomeLogoSetting : ConfigurationSection, IHomeLogoSetting
    {
        [ConfigurationProperty("logoName", IsRequired = true)]
        public string LogoName
        {
            get
            {
                return this["logoName"].ToString();
            }
            set
            {
                this["logoName"] = value;
            }
        }

        [ConfigurationProperty("faviconName", IsRequired = true)]
        public string FaviconName
        {
            get
            {
                return this["faviconName"].ToString();
            }
            set
            {
                this["faviconName"] = value;
            }
        }
    }
}
