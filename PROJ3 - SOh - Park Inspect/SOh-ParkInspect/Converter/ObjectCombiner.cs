﻿using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SOh_ParkInspect.Converter
{
    public class ObjectCombiner: IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.ToList();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
