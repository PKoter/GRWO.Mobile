using System.Collections.Generic;
using JetBrains.Annotations;

namespace System.Linq
{
	public static class CollectionExtensions
	{
		/*
		/// <summary>
		/// Creates a new list with elements from s and specified capacity.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="s"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		[NotNull]
		public static List<T> ToList<T>([NotNull] this IEnumerable<T> s, int count)
		{
			if (count < 0) throw new ArgumentOutOfRangeException("count");
			var list = new List<T>(count);
			list.AddRange(s);
			return list;
		}*/

		/// <summary>
		/// if list is empty, returns null
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="s"></param>
		/// <returns></returns>
		[CanBeNull]
		public static List<T> ToListOrNull<T>([NotNull] this IEnumerable<T> s)
		{
			var list = s.ToList();
			return list.Count > 0 ? list : null;
		}
	}
}
