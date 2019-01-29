using Xamarin.Forms;

namespace GameResOrg.Core.Validations
{
	public class ListRequiredBehaviour : Behavior<ListView>
	{
		static readonly BindableProperty IsValidProperty =
			BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(EntryRequiredBehaviour), false);

		public bool IsValid
		{
			get => (bool)GetValue(IsValidProperty);
			private set => SetValue(IsValidProperty, value);
		}


		protected override void OnAttachedTo(ListView bindable)
		{
			//bindable.TextChanged += HandleTextChanged;
			//bindable. = Color.Red;
		}

		

		protected override void OnDetachingFrom(ListView bindable)
		{
			//bindable.TextChanged -= HandleTextChanged;
		}
	}
}
