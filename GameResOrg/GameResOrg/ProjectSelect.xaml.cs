using System;
using GameResOrg.Glue;
using Xamarin.Forms;

namespace GameResOrg
{
	public partial class ProjectSelect : ContentPage
	{
		private ProjectSelectController _logic;

		public ProjectSelect()
		{
			InitializeComponent();
			_logic = Mold.DependencyMold.Get<ProjectSelectController>();
			this.BindingContext = _logic;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			this.projList.ItemsSource = await _logic.GetProjects();
		}

		private async void OnSelectClick(object sender, EventArgs e)
		{
			if (_logic.SelectedProject == null)
				return;

			var error = await _logic.SelectProject();
			if(error == null)
				App.Current.MainPage = new NavigationPage(new MainPage() );
		}
	}
}