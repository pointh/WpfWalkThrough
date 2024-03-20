using System.Globalization;
using System.Windows.Data;

namespace WpfWalkThrough
{
    public class EnumToStringConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (int)value > MainWindow.StavStr.Count)
                return null;

            string s = MainWindow.StavStr[(Stav)value];
            return s;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            Stav k = MainWindow.StavStr.First(x => x.Value == (string)value).Key;
            return k;

        }
    }
}