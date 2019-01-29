using System.Timers;
using GameResOrg.Intermediate;

namespace GameResOrg.Glue
{
	public sealed class ProcessBouncer : TaskBouncer
	{
		private int _done;
		private Timer _timer;
		private PageController _controller;

		public ProcessBouncer(PageController controller)
		{
			_controller = controller;
		}

		protected override void TaskState(bool done)
		{
			_done += done ? -1 : 1;
			if (_timer == null)
			{
				_timer = new Timer();
				_timer.AutoReset = false;
				_timer.Interval = 100;
				_timer.Start();
				_timer.Elapsed += (sender, args) => { _controller.IsBusy = _done > 0; };
			}
			else if(done == false)
			{
				_timer.Start();
			}
			else
				_timer.Stop();

			if (_done == 0)
				_controller.IsBusy = false;
		}
	}
}
