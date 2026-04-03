using System.Globalization;

namespace GestionnaireLivresMAUI.Converters
{
    public class GenreToColorConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value?.ToString() switch
            {
                "Roman" => Color.FromArgb("#3B82F6"),   // Bleu
                "SF" => Color.FromArgb("#8B5CF6"),      // Violet
                "Fantasy" => Color.FromArgb("#22C55E"), // Vert
                "Policier" => Color.FromArgb("#EF4444"),// Rouge
                _ => Color.FromArgb("#6B7280"),         // Gris (Autre)
            };
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}