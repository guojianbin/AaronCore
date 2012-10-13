using System;
using System.Configuration;
using Aaron.Core.Utility.Setting;

namespace Aaron.Core.Utility.Image.Config
{
    public class ImageSetting : ConfigurationSection, IImageSetting
    {
        [ConfigurationProperty("imagePath", IsRequired = true)]
        public string ImagePath
        {
            get
            {
                return this["imagePath"].ToString();
            }
            set
            {
                this["imagePath"] = value;
            }
        }

        [ConfigurationProperty("thumpPath", IsRequired = true)]
        public string ThumpPath
        {
            get
            {
                return this["thumpPath"].ToString();
            }
            set
            {
                this["thumpPath"] = value;
            }
        }
    }
}
