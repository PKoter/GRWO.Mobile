using JetBrains.Annotations;

namespace GameResOrg.Intermediate
{
	public struct TaskResult<T>
	{
		[CanBeNull]
		public FailInfo Fail { get; }
		public T Value { get; }

		public TaskResult(T value, string error = null, object data = null)
		{
			Value = value;
			Fail = error == null ? null : new FailInfo(error) { Data = data };
		}

		public static explicit operator string(TaskResult<T> result)
		{
			return (string)result.Fail;
		}
	}
}
