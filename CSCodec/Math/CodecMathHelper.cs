using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec.Math
{
	/// <summary>
	///
	/// </summary>
	/// TODO Edit XML Comment Template for CodecMathHelper
	public static class CodecMathHelper
	{
		/// <summary>
		/// same as floor(log2(i))
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		public static int CountBits(uint i)
		{
			int y = 0;
			for (; i > 0; y++) i >>= 1;
			return y;
		}

		/// <summary>
		/// Determines whether the specified <paramref name="i"/> is power of two.
		/// </summary>
		/// <param name="i">The value.</param>
		/// <returns>
		///   <c>true</c> if [is power of two] [the specified i]; otherwise, <c>false</c>.
		/// </returns>
		/// TODO Edit XML Comment Template for IsPowerOfTwo
		public static bool IsPowerOfTwo(uint i)
		{
			return i != 0 && ((i & (i - 1)) == 0);
		}
	}
}
