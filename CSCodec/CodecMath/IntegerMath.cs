using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CSCodec
{
	/// <summary>
	/// Arithmetic utilities for integral types.
	/// </summary>
	public static partial class IntegerMath
	{
		/// <summary>
		/// Calculates Average for the specified values.
		/// </summary>
		/// <param name="left">The left value.</param>
		/// <param name="right">The right value.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Average(int left, int right)
		{
			unchecked
			{
				return (int)Math.Round(0.5 * left + 0.5 * right);
			}
		}
	}
}
