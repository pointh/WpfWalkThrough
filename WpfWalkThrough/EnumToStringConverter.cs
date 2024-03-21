using System.Globalization;
using System.Windows.Data;

namespace WpfWalkThrough
{
    public class EnumToStringConverter : IValueConverter
    {
        public static Dictionary<Stav, string> StavStr { get; set; } = new Dictionary<Stav, string>()
        {
            { Stav.Svobodny, "Svobodný" },
            { Stav.Zenaty, "Ženatý" },
            { Stav.Rozvedeny, "Rozvedený" }
        };

        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (int)value > StavStr.Count)
                return null;

            string s = StavStr[(Stav)value];
            return s;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            // Stav k = MainWindow.StavStr.First(x => x.Value == (string)value).Key;
            return StavStr.FirstOrDefault(x => x.Value == (string)value).Key;

        }
    }
}