using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CSCodec
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
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountBits(uint i)
		{
			int y = 0;
			for (; i > 0; y++) i >>= 1;
			return y;
		}

		/// <summary>
		/// Reverses the bits of the specified value in specified width.
		/// </summary>
		/// <param name="i">The value.</param>
		/// <param name="width">The width in bits.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ReverseBits(uint i, int width)
		{
			int shift = 32 - width;
			return ReverseBits(i << shift);
		}

		/// <summary>
		/// Reverses the bits of the specified value in 32bit.
		/// </summary>
		/// <param name="i">The i.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ReverseBits(uint i)
		{
			i = (i & 0x55555555) << 1 | (i >> 1) & 0x55555555;
			i = (i & 0x33333333) << 2 | (i >> 2) & 0x33333333;
			i = (i & 0x0f0f0f0f) << 4 | (i >> 4) & 0x0f0f0f0f;
			i = (i << 24) | ((i & 0xff00) << 8) | ((i >> 8) & 0xff00) | (i >> 24);
			return i;
		}

		/// <summary>
		/// Determines whether the specified <paramref name="i"/> is power of two.
		/// </summary>
		/// <param name="i">The value.</param>
		/// <returns>
		///   <c>true</c> if the specified value is power of two; otherwise, <c>false</c>.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsPowerOfTwo(this uint i)
		{
			return i != 0 && ((i & (i - 1)) == 0);
		}

		/// <summary>
		/// Determines whether the specified <paramref name="i"/> is power of two.
		/// </summary>
		/// <param name="i">The value.</param>
		/// <returns>
		///   <c>true</c> if the specified value is power of two; otherwise, <c>false</c>.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsPowerOfTwo(this int i)
		{
			return i != 0 && ((i & (i - 1)) == 0);
		}
	}
}
