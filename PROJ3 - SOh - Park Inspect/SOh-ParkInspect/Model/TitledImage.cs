using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SOh_ParkInspect.Model
{
    public class TitledImage
    {
        public BitmapImage _image;
        public string _title;

        public string Title { get; set; }
        public BitmapImage Image { get; set; }

        public TitledImage(BitmapImage image, string title)
        {
            _image = image;
            _title = title;
        }
    }
}
