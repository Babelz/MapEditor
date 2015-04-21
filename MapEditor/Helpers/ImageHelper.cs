using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MapEditor.Helpers
{
    public static class ImageHelper
    {
        public static BitmapImage Load(string path)
        {
            BitmapImage image = new BitmapImage(new Uri(path));

            return image;
        }

        public static BitmapImage LoadToMemory(string path)
        {
            BitmapImage image = new BitmapImage();

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                image.BeginInit();

                image.StreamSource = stream;
                image.CacheOption = BitmapCacheOption.OnLoad;

                image.EndInit();
            }

            return image;
        }
    }
}
