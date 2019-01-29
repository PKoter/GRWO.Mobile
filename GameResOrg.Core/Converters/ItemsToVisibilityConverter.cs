using System;
using System.Collections;
using System.Globalization;
using GameResOrg.Debug;
using Xamarin.Forms;

namespace GameResOrg.Controls.Converters
{
	/// <summary>
	/// binds to count, items, ItemsSource, and returns visible when count > 0. param=True to invert.
	/// </summary>
	public sealed class ItemsToVisibilityConverter : IValueConverter
	{
		public static ItemsToVisibilityConverter Instance = new ItemsToVisibilityConverter();

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var invert = string.Equals(parameter as string, "!");
			if (parameter is bool invertBool)
				invert = invertBool;

			bool result = false;
			if (value is int count)
				result = count > 0;
			else if (value is IEnumerable enumerable)
			{
				if (enumerable is ICollection collection)
					result = collection.Count > 0;
				else
				{
					foreach (var item in enumerable)
					{
						result = true;
						break;
					}
				}
			}
			else if(value != null) throw new SomethingsNoYes(value.ToString());

			return result ^ invert;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
