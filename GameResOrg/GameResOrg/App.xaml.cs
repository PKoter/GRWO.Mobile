using GameResOrg.Glue;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GameResOrg
{
	public partial class App : Application
	{
		private Mold _mold;

		public App()
		{
			InitializeComponent();
			_mold = new Mold();
			MainPage = new LoginPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
