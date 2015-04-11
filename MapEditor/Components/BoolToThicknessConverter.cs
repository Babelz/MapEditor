using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MapEditor.Components
{
    public sealed class BoolToThicknessConverter : IValueConverter
    {
        public BoolToThicknessConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;

            return !boolValue ? new Thickness(0.0) : new Thickness(2.0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Thickness thickness = (Thickness)value;

            return thickness.Top == 0.0 ? false : true;
        }
    }
}
