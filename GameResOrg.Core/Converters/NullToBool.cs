using System;
using System.Globalization;
using Xamarin.Forms;

namespace GameResOrg.Controls.Converters
{
	/// <summary>
	/// If value is null, then false, otherwise true. Inverted if parameter is true.
	/// </summary>
	public class NullToBool : IValueConverter
    {
		public static readonly NullToBool Instance = new NullToBool();

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var  s      = value;
			bool invert = parameter?.ToString().Equals("!") ?? false;

			return s != null ^ invert;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
    }
}
