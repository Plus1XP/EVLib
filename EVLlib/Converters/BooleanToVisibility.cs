using EVLlib.Enums;
using EVLlib.Interfaces;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EVLlib.Converters
{
    class BooleanToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Visible)
            {
                return true;
            }

            if ((Visibility)value == Visibility.Hidden)
            {
                return false;
            }

            throw new Exception(string.Format("Cannot convert back, unknown value {0}", value));
        }
    }
}
