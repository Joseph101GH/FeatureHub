using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace FeatureHub
{
    public class ProportionalWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var listViewWidth = (double)values[0];
            var factor = System.Convert.ToDouble(parameter);
            var totalFactors = values.Skip(1).Sum(x => System.Convert.ToDouble(x));
            return (listViewWidth - 100) * factor / totalFactors; // 100 is the fixed width for the "Stop" column
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
