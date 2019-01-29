using Xamarin.Forms;

namespace GameResOrg.Core.Validations
{
	public class EntryRequiredBehaviour : Behavior<Entry>
	{
		static readonly BindableProperty IsValidProperty =
			BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(EntryRequiredBehaviour), false);

		public bool IsValid
		{
			get => (bool)GetValue(IsValidProperty);
			private set => SetValue(IsValidProperty, value);
		}


		protected override void OnAttachedTo(Entry bindable)
		{
			bindable.TextChanged += HandleTextChanged;
			bindable.PlaceholderColor = Color.Red;
		}

		void HandleTextChanged(object sender, TextChangedEventArgs e)
		{
			IsValid = !string.IsNullOrEmpty(e.NewTextValue);
			((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
			((Entry)sender).PlaceholderColor = IsValid ? Color.Default : Color.Red;
		}

		protected override void OnDetachingFrom(Entry bindable)
		{
			bindable.TextChanged -= HandleTextChanged;
		}
	}
}
