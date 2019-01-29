using System;
using System.Collections.Generic;
using System.Linq;
using GameResOrg.Controls.Converters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameResOrg.Controls.Api
{
	public enum ConverterType
	{
		/// <summary>
		/// if true then visible i guess
		/// </summary>
		BoolToVisible,
		/// <summary>
		/// if null, then false.
		/// </summary>
		NullToBool,
		/// <summary>
		/// if text is null or empty, returns true. True inverts condition.
		/// </summary>
		TextToVisible,
		NegateBool,
		Avatar,
		CountToVisible
	}

	public sealed class ConverterExtension : IMarkupExtension<IValueConverter>
	{
		private static KeyValuePair<ConverterType, IValueConverter>[] _converters = 
		{
			//new KeyValuePair<ConverterType, IValueConverter>(ConverterType.BoolToVisible, 
			//												 BoolToVisibility.Instance), 
			new KeyValuePair<ConverterType, IValueConverter>(ConverterType.NullToBool, 
															 NullToBool.Instance),
			new KeyValuePair<ConverterType, IValueConverter>(ConverterType.TextToVisible, 
															 TextToVisibility.Instance),
			new KeyValuePair<ConverterType, IValueConverter>(ConverterType.NegateBool, 
															 NegateBoolConverter.Instance),
			new KeyValuePair<ConverterType, IValueConverter>(ConverterType.Avatar, 
															 AvatarConverter.Instance),
			new KeyValuePair<ConverterType, IValueConverter>(ConverterType.CountToVisible, 
															 ItemsToVisibilityConverter.Instance),
		};
		public ConverterType Type { get; set; }

		public ConverterExtension() {}

		public ConverterExtension(ConverterType type)
		{
			Type = type;
		}

		IValueConverter IMarkupExtension<IValueConverter>.ProvideValue(IServiceProvider serviceProvider)
		{
			return ProvideValue(serviceProvider) as IValueConverter;
		}

		public object ProvideValue(IServiceProvider serviceProvider)
		{
			var converter = _converters.First(r => r.Key == Type).Value;
			return converter;
		}
	}
}
