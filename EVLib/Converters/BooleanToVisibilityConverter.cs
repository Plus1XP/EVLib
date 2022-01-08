using EVLib.Enums;
using EVLib.Interfaces;

using System;
using System.Globalization;

namespace EVLib.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Convert Boolean to Visibility.
        /// </summary>
        /// <param name="value">Boolean</param>
        /// <param name="targetType">Visibility</param>
        /// <param name="parameter">Null</param>
        /// <param name="culture">Null</param>
        /// <returns>Visible or Hidden</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// Convert Visibility to Boolean.
        /// </summary>
        /// <param name="value">Visibility</param>
        /// <param name="targetType">Boolean</param>
        /// <param name="parameter">Null</param>
        /// <param name="culture">Null</param>
        /// <returns>Boolean Value</returns>
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
