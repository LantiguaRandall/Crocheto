using System.Globalization;

namespace CrochetoApp.Converter
{
    public class EstadoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (int.TryParse(value.ToString(), out int estado))
            {
                switch (estado)
                {
                    case 0:
                        return "Inactivo";
                    case 1:
                        return "Activo";
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
