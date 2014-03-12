using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Editor
{

    public class EditorModeConverter : IValueConverter
    {
        public object Convert(object _value, Type targetType, object _parameter, System.Globalization.CultureInfo culture)
        {
            var parameter = (EditorModes)_parameter;
            var value = (EditorModes)_value;
            return value == parameter;
        }
        public object ConvertBack(object _value, Type targetType, object _parameter, System.Globalization.CultureInfo culture)
        {
            var parameter = (EditorModes)_parameter;
            var value = (bool?)_value;
            if (!value.HasValue || !value.Value)
                return DependencyProperty.UnsetValue;
            return parameter;
        }
    }

    public class PausedToStringConverter : IValueConverter
    {
        public object Convert(object _value, Type targetType, object _parameter, System.Globalization.CultureInfo culture)
        {
            var value = (bool)_value;
            return value ? "Play" : "Pause";
        }
        public object ConvertBack(object _value, Type targetType, object _parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
