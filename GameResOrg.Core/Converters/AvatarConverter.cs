using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;
using Xamarin.Forms;

namespace GameResOrg.Controls.Converters
{
	public class AvatarConverter : IValueConverter
    {
		public static readonly AvatarConverter Instance = new AvatarConverter();
		private static ImageSource _defaultAvt = ImageSource.FromResource("GameResOrg.Core.Icons.user_avatar_w.png", Assembly.GetExecutingAssembly());

		[NotNull]
		public static ImageSource LoadBitmapForUser([CanBeNull] MemoryStream photoStream)
		{
			//if (photoStream != null && photoStream.CanRead)
			//{
			//	return ImageSource.FromStream(() => photoStream);
			//}
			if (photoStream != null)
			{
				return ImageSource.FromStream(() =>
					{
						var arr = photoStream.ToArray();
						return new MemoryStream(arr, 0, arr.Length, false);
					});
			}
			return _defaultAvt;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return LoadBitmapForUser(value as MemoryStream);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
