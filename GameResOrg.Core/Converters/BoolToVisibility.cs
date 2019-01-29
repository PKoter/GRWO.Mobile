using System;
using System.Globalization;
using Xamarin.Forms;

namespace GameResOrg.Controls.Converters
{

	public sealed class BoolToVisibility : IValueConverter
	{
		public static readonly BoolToVisibility Instance = new BoolToVisibility();

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool invert = parameter?.ToString().Equals("!") ?? false;

			bool flag = false;
			if (value is bool b)
				flag = b;

			return flag ^ invert;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
