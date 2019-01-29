using System;
using System.Threading;
using System.Threading.Tasks;
using GameResOrg.Debug;
using JetBrains.Annotations;

namespace GameResOrg.Intermediate
{
	public class TaskBouncer
	{
		private static int _tasksRunning;
		private object _work;
		protected bool _atWork;

		public bool AtWork
		{
			get
			{
				lock(this) return _atWork;
			}
		}

		public static bool AnyTaskRunning => _tasksRunning > 0;

		public Task StartSingleTask([NotNull] Action action)
		{
			@Asure.NotNull(action);
			var work = _work;
			//if ((work as Action) == action)
			//	return null;

			lock (this)
			{
				_atWork = true;
				_work   = action;
				return Task.Factory.StartNew(Run<object>, action);
			}
		}
		
		public Task<T> StartSingleTask<T>([NotNull] Func<T> action)
		{
			@Asure.NotNull(action);
			var work = _work;
			//if ((work as Func<T>) == action)
			//	return null;

			lock (this)
			{
				_atWork = true;
				_work   = action;
				return Task.Factory.StartNew(Run<T>, action);
			}
		}

		private TR Run<TR>(object a)
		{
			lock (this)
			{
				try
				{
					Interlocked.Increment(ref _tasksRunning);
					TaskState(false);
					if (a is Func<TR> func)
					{
						return func.Invoke();
					}
					((Action)a).Invoke();
				}
				finally
				{
					TaskState(true);
					_atWork = false;
					_work   = null;
					Interlocked.Decrement(ref _tasksRunning);
				}
			}
			return default;
		}

		protected virtual void TaskState(bool done){}
	}
}
