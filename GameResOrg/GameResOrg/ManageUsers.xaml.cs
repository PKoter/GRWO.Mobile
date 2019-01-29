using System;
using System.Collections.Generic;
using GameResOrg.Glue;
using GameResOrg.Logic.Team.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameResOrg
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ManageUsers : ContentPage
	{
		private ManageUsersController _logic;
		private IList<UserModel> _users;

		public ManageUsers()
		{
			InitializeComponent();
			_logic              = Mold.DependencyMold.Get<ManageUsersController>();
			this.BindingContext = _logic;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			_users                    = await _logic.GetUsers();
			this.userList.ItemsSource = _users;
		}

		private void UserSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (_logic.SelectedUser == null)
			{
				return;
			}
			_logic.SetContext(_logic.SelectedUser);
		}

		private async void SavePrivileges(object sender, EventArgs eventArgs)
		{
			if (_logic.SelectedUser == null)
				return;

			await _logic.SavePrivileges();
		}
	}
}