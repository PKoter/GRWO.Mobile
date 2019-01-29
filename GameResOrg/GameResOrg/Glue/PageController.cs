using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Xamarin.Forms;

namespace GameResOrg.Glue
{
	public class PageController : INotifyPropertyChanged
	{
		protected PageController()
		{
			Bouncer = new ProcessBouncer(this);
		}

		internal ProcessBouncer Bouncer { get; set; }

		private bool _busy;

		public bool IsBusy
		{
			get { return _busy; }
			set
			{
				if (value != _busy)
				{
					_busy = value;
					OnPropertyChanged("IsBusy");
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			Device.BeginInvokeOnMainThread(() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)));
		}
	}
}
