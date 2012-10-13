using Aaron.Core.Utility.Setting;

namespace Aaron.Core.Utility.Image.Config
{
    public static class ImageSettingContext
    {
        private static IImageSetting _imageSetting;

        public static IImageSetting CreateInstance()
        {
            return _imageSetting ?? (_imageSetting = new SettingHelper<IImageSetting>("imgPath").Setting());
        }
    }
}
