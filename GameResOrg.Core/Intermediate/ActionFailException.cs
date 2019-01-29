using System;
using System.Runtime.CompilerServices;

namespace GameResOrg.Intermediate
{
	/// <summary>
	/// describes a fail that actions may encounter, that is possible within properly workin system.
	/// </summary>
	/// message prefix: e
	public class ActionFailException : Exception
	{
		public ActionFailException(string message) : base(message)
		{
		}
	}

	public static class FailExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ThrowIfFail(this ActionFailException ex, string failCode = null)
		{
			if (ex != null)
			{
				if(failCode != null && ex.Message != failCode)
					throw new ActionFailException(failCode);
				throw ex;
			}
		}
	}
}
