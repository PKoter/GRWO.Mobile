using System;
using System.Collections.Generic;
using GameResOrg.Data.Project;
using GameResOrg.Glue;
using JetBrains.Annotations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameResOrg
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddThread : ContentPage
	{
		private AddThreadController _logic;
		private Group _group;
		private List<ThreadMember> _members;

		public Group Group { get; set; }

		public AddThread ()
		{
			InitializeComponent ();
			_logic = Mold.DependencyMold.Get<AddThreadController>();
			this.BindingContext = _logic;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			_logic.Group = _group;
			_members = _logic.GetIncludableMembers();
			this.userList.ItemsSource = _members;
		}

		public void SetGroup([NotNull] Group group)
		{
			_group = group;
			_logic.Group = _group;
		}

		private async void OnSave(object sender, EventArgs e)
		{
			var error = _logic.Validate(_members);
			this.errorText.Text = error == 2 ? "Must include at least two users" : "";
			if (error > 0)
				return;

			var result = await _logic.Save(_members);
			if (result)
				await Navigation.PopAsync();
		}
	}
}