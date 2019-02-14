using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec.Debug
{
	/// <summary>
	/// Debugging utilities
	/// </summary>
	public static class DebugUtils
	{
		/// <summary>
		/// Dumps the array to the Console.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array">The array.</param>
		/// <param name="format">The formatting codes.</param>
		public static void DumpArray<T>(Span<T> array, string format)
		{
			for (int i = 0; i < array.Length; i++)
			{
				Console.WriteLine(format, array[i]);
			}
		}
	}
}
