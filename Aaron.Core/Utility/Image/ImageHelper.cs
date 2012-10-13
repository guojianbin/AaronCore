using System;
using System.Web;
using System.Web.Helpers;
using System.IO;
using Aaron.Core.Utility.Setting;
using Aaron.Core.Utility.Image.Config;

namespace Aaron.Core.Utility.Image
{
    public class ImageHelper
    {

        public static ImageSetting GetSetting()
        {
            return (ImageSetting)ImageSettingContext.CreateInstance();
        }

        public static string[] ImagePath
        {
            get
            {
                ImageSetting setting = GetSetting();
                return new[] { setting.ImagePath, setting.ThumpPath };
            }
        }

        private static void CreateFolder(ImageSetting setting)
        {
            var fullPath = HttpContext.Current.Server.MapPath("/");

            if (!Directory.Exists(fullPath + setting.ImagePath))
                Directory.CreateDirectory(fullPath + setting.ImagePath);
            if (!Directory.Exists(fullPath + setting.ThumpPath))
                Directory.CreateDirectory(fullPath + setting.ThumpPath);
        }

        public static void SaveLogoOrFavicon(Stream inputStream, string fileName, bool isFavicon)
        {
            ImageSetting setting = GetSetting();

            var fullPath = HttpContext.Current.Server.MapPath("/");

            CreateFolder(setting);

            if (inputStream == null) throw new ArgumentNullException("inputStream");
            var image = new WebImage(inputStream) {FileName = fileName};

            if (image.Width > 500)
            {
                image.Resize(500, ((500 * image.Height) / image.Width));
            }

            var logo = new SettingHelper<IHomeLogoSetting>("homeLogo").Setting();

            var fn = (!isFavicon) ? logo.LogoName : logo.FaviconName;

            image.Save(fullPath + fn);
        }

        public static void Save(Stream inputStream, string fileName, out string imagePath, out string thumpPath, out byte[] binary, out string mimeType)
        {
            ImageSetting setting = GetSetting();
            
            var fullPath = HttpContext.Current.Server.MapPath("/");

            CreateFolder(setting);
            
            if (inputStream == null) throw new ArgumentNullException("inputStream");
            var image = new WebImage(inputStream);

            binary = image.GetBytes();
            mimeType = fileName.GetMimeType();

            image.FileName = fileName;

            if (image.Width > 500)
            {
                image.Resize(500, ((500 * image.Height) / image.Width));
            }

            var fn = Guid.NewGuid() + "-" + CommonHelper.RemoveMarks(Path.GetFileName(fileName));

            thumpPath = imagePath = fn;

            image.Save(fullPath + setting.ImagePath + fn);

            image.Resize(132, 102);
            image.Save(fullPath + setting.ThumpPath + "/1__" + fn);

            image.Resize(53, 53);
            image.Save(fullPath + setting.ThumpPath + "/2__" + fn);
        }
    }
}
