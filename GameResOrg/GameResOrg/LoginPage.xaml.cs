using System;
using GameResOrg.Glue;
using Xamarin.Forms;

namespace GameResOrg
{
	public partial class LoginPage : ContentPage
	{
		private LoginPageController _logic;

		public LoginPage ()
		{
			InitializeComponent ();
			_logic = Mold.DependencyMold.Get<LoginPageController>();
			this.BindingContext = _logic;
		}

		private async void OnLoginClick(object sender, EventArgs e)
		{
			if (_logic.Validate() == false)
				return;

			this.errorText.Text = "";
			this.IsBusy = true;
			var error = await _logic.TryLogin();
			this.IsBusy = false;

			var errorMessage = (string)error;
			if (errorMessage == "eBadLogin")
				errorMessage = "Email or password error";

			this.errorText.Text = errorMessage;
			if(error == null)
				App.Current.MainPage = new ProjectSelect();
		}
	}
}