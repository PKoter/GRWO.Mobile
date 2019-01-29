using System;
using System.Collections;
using System.Collections.Generic;
using GameResOrg.Data.Project;
using GameResOrg.Glue;
using GameResOrg.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameResOrg
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GroupChat : ContentPage
	{
		private GroupChatController _logic;
		private IList<Group> _groups;

		public GroupChat ()
		{
			InitializeComponent ();
			_logic = Mold.DependencyMold.Get<GroupChatController>();
			this.BindingContext = _logic;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			_groups = await _logic.GetGroups();
			this.groupList.ItemsSource = (IList)_groups;
			if (_groups?.Count == 1)
				this.groupList.SelectedIndex = 0;
		}

		private async void OnThreadSelected(object sender, EventArgs e)
		{
			if (_logic.SelectedThread == null)
				return;

			await _logic.GetMessagesForThread(_logic.SelectedThread);
			await _logic.ThreadSelected(_logic.SelectedThread);
		}

		private async void OnGroupSelected(object sender, EventArgs e)
		{
			if (_logic.SelectedGroup == null)
				return;

			_logic.SetContext(_logic.SelectedGroup);
			await _logic.GetConversations();
			this.threadList.ItemsSource = _logic.Threads;
			if (_logic.Threads?.Count == 1)
				this.threadList.SelectedIndex = 0;
		}

		private async void OnMessageSend(object sender, EventArgs e)
		{
			if (_logic.MessageText.IsNullOrWhitespace())
				return;

			await _logic.SendMessage();
		}

		private async void OnNewThreadClick(object sender, EventArgs e)
		{
			if (this.groupList.SelectedIndex == -1)
				return;

			var page = new AddThread();
			page.SetGroup(_logic.SelectedGroup);
			await Navigation.PushAsync(page);
		}
	}
}