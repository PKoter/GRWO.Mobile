using System;
using System.Globalization;
using Xamarin.Forms;

namespace GameResOrg.Controls.Converters
{
	/// <summary>
	/// If text is null or empty, returns visible, otherwise collapsed. Parameter of true/True inverts condition.
	/// </summary>
	public sealed class TextToVisibility: IValueConverter
	{
		public static readonly TextToVisibility Instance = new TextToVisibility();

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var s = value as string;
			bool invert = parameter?.ToString().Equals("!") ?? false;

			return string.IsNullOrEmpty(s) ^ invert;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
