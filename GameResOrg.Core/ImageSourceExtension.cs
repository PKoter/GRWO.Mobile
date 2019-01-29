using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameResOrg.Controls.Api
{
	public sealed class ImageSourceExtension : IMarkupExtension<ImageSource>
	{
		public string Path { get; set; }

		public ImageSource ProvideValue(IServiceProvider serviceProvider)
		{
			return FileImageSource.FromResource(Path, Assembly.GetExecutingAssembly());
		}

		object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
		{
			return ProvideValue(serviceProvider);
		}
	}
}
