using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WPF.Converter
{
    public class NeedsImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = value as string;
            var path = "/png/android.png";
            var rotation = Rotation.Rotate0;

            if (s != null)
            {
                switch (s)
                {
                    case "Sleep":
                        rotation = Rotation.Rotate90;
                        break;
                    case "Play":
                        path = "/png/android-skateboard.png";
                        break;
                    case "Feed":
                        path = "/png/android-heart.png";
                        break;
                    case "Hug":
                        path = "/png/android-hearteyes.png";
                        break;
                }
            }

            var bimage = new BitmapImage();
            bimage.BeginInit();
            bimage.UriSource = new Uri(path, UriKind.Relative);
            bimage.Rotation = rotation;
            bimage.EndInit();

            return bimage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}