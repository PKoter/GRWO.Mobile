using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GameResOrg.Intermediate
{
	public static class Caller
	{
		public static async Task<TaskResult<T>> AwaitCall<T>(this Task<T> task)
		{
			try
			{
				var result = await task.ConfigureAwait(false);
				return new TaskResult<T>(result);
			}
			catch (ActionFailException actionEx)
			{
				return new TaskResult<T>(default, actionEx.Message);
			}
			catch (AggregateException e)
			{
				return new TaskResult<T>(default, FetchErrorMessage(e));
			}
			catch (Exception ex)
			{
				return new TaskResult<T>(default, "Error - "+ex.Message);
			}
		}

		[ItemCanBeNull]
		public static async Task<FailInfo> AwaitCall(this Task task)
		{
			try
			{
				if(task == null) // task probably blocked by task bouncer 
					return new FailInfo("eTaskBlocked");
				await task.ConfigureAwait(false);
				return null;
			}
			catch (ActionFailException actionEx)
			{
				return new FailInfo(actionEx.Message);
			}
			catch (AggregateException e)
			{
				return new FailInfo(FetchErrorMessage(e));
			}
			catch (Exception ex)
			{
				return new FailInfo(ex.Message);
			}
		}

		private static string FetchErrorMessage(AggregateException e)
		{
			var inner = e.InnerException is AggregateException ? e.Flatten().InnerExceptions : e.InnerExceptions;
			var actionEx = inner.FirstOrDefault(ex => ex is ActionFailException);
			if (actionEx != null)
			{
				return actionEx.Message;
			}
			// now we got problem - which exception to choose?
			return inner.First().Message;
		}
	} 
}
