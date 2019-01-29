using System;
using GameResOrg.Glue;
using Xamarin.Forms;

namespace GameResOrg
{
	public partial class MainPage : ContentPage
	{
		private MainPageController _logic;

		public MainPage()
		{
			InitializeComponent();
			_logic = Mold.DependencyMold.Get<MainPageController>();
			this.BindingContext = _logic;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			this.manageUsers.IsEnabled = _logic.CanManagePrivileges;
			var count = await _logic.GetNewMessagesCount();
			this.msgCount.Text = count.ToString();
		}

		private async void OnManageUsersClick(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ManageUsers(), true);
		}

		private async void OnGroupChatClick(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new GroupChat(), true);
		}
	}
}
