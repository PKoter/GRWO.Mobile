using System;
using System.Globalization;
using Xamarin.Forms;

namespace GameResOrg.Controls.Converters
{

	public class NegateBoolConverter: IValueConverter
	{
		public static NegateBoolConverter Instance = new NegateBoolConverter();

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType != typeof(bool))
				throw new InvalidOperationException("The target must be a boolean");

			return !(bool)value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
