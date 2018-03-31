using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using database;
using prog5_ninja.ViewModel;

namespace prog5_ninja.Converter
{
    [ValueConversion(typeof(object[]), typeof(bool))]
    public class BuyButtonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var ninja = values[0] as ninja;
            var item = values[1] as equipment;

            // If there's no ninja or item selected.
            if (ninja == null || item == null) return false;

            // If the ninja is already wearing this item.
            if (ninja.equipment.Any(e => e.id == item.id)) return false;

            // Filter item from the same category and sum the costs
            return ninja.equipment.Where(e => e.category.name != item.category.name)
                .Sum(e => e.value) + item.value <= NinjaViewModel.TotalGold;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
