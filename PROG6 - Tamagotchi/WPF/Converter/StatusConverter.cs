using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using WPF.TamagotchiService;

namespace WPF.Converter
{
    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tamagotchi = value as Tamagotchi;

            if (tamagotchi == null) return string.Empty;

            if (tamagotchi.Deceased) return "Deceased";

            if (tamagotchi.Statuses.Any())
            {
                return string.Join("\r\n", tamagotchi.Statuses);
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
