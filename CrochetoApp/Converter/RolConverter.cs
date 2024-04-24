using System.Globalization;

namespace CrochetoApp.Converter
{
    public class RolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (int.TryParse(value.ToString(), out int rol))
            {
                switch (rol)
                {
                    case 0:
                        return "Usuario";
                    case 1:
                        return "Admin";
                }
            }

            return "No sabo";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
