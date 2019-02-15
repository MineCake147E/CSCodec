using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Buffers.Binary;
using System.Text;

namespace CSCodec
{
	/// <summary>
	/// Supports some bit arithmetics.
	/// </summary>
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

		#region ReverseBits

		/// <summary>
		/// Reverses the bits of the specified value in specified width.
		/// </summary>
		/// <param name="i">The value.</param>
		/// <param name="width">The width in bits.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ReverseBits(this uint i, int width)
		{
			int shift = 32 - width;
			return ReverseBits(i << shift);
		}

		/// <summary>
		/// Reverses the bits of the specified value in 64bit.
		/// </summary>
		/// <param name="i">The value to reverse bit order.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong ReverseBits(this ulong i)
		{
			i = ((i & 0x5555555555555555) << 1) | ((i >> 1) & 0x5555555555555555);
			i = ((i & 0x3333333333333333) << 2) | ((i >> 2) & 0x3333333333333333);
			i = ((i & 0x0f0f0f0f0f0f0f0f) << 4) | ((i >> 4) & 0x0f0f0f0f0f0f0f0f);
			return BinaryPrimitives.ReverseEndianness(i);
		}

		/// <summary>
		/// Reverses the bits of the specified value in 32bit.
		/// </summary>
		/// <param name="i">The value to reverse bit order.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ReverseBits(this uint i)
		{
			i = ((i & 0x55555555) << 1) | ((i >> 1) & 0x55555555);
			i = ((i & 0x33333333) << 2) | ((i >> 2) & 0x33333333);
			i = ((i & 0x0f0f0f0f) << 4) | ((i >> 4) & 0x0f0f0f0f);
			return BinaryPrimitives.ReverseEndianness(i);
		}

		/// <summary>
		/// Reverses the bits of the specified value in 16bit.
		/// </summary>
		/// <param name="i">The value to reverse bit order.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ushort ReverseBits(this ushort i)
		{
			int j = ((i & 0x5555) << 1) | ((i >> 1) & 0x5555);
			j = ((j & 0x3333) << 2) | ((j >> 2) & 0x3333);
			j = ((j & 0x0f0f) << 4) | ((j >> 4) & 0x0f0f);
			return BinaryPrimitives.ReverseEndianness(unchecked((ushort)(0xffff & j)));
		}

		/// <summary>
		/// Reverses the bits of the specified value in 8bit.
		/// </summary>
		/// <param name="i">The value to reverse bit order.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static byte ReverseBits(this byte i)
		{
			int j = ((i & 0x55) << 1) | ((i >> 1) & 0x55);
			j = ((j & 0x33) << 2) | ((j >> 2) & 0x33);
			j = ((j & 0x0f) << 4) | ((j >> 4) & 0x0f);
			return BinaryPrimitives.ReverseEndianness(unchecked((byte)(0xff & j)));
		}

		#endregion ReverseBits

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

		#region SignedToFromUnsigned

		/// <summary>
		/// Converts to unsigned integer bitwisely(-1L => 0xFFFFFFFFFFFFFFFFUL without exception).
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong ToUnsigned(this long value) => unchecked((ulong)value);

		/// <summary>
		/// Converts to unsigned integer bitwisely(-1 => 0xFFFFFFFFu without exception).
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ToUnsigned(this int value) => unchecked((uint)value);

		/// <summary>
		/// Converts to unsigned integer bitwisely(-1 => 65535 without exception).
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ushort ToUnsigned(this short value) => unchecked((ushort)value);

		/// <summary>
		/// Converts to unsigned integer bitwisely(-1 => 255 without exception).
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static byte ToUnsigned(this sbyte value) => unchecked((byte)value);

		/// <summary>
		/// Converts to signed integer bitwisely(0xFFFFFFFFFFFFFFFFUL => -1L without exception).
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ToSigned(this ulong value) => unchecked((long)value);

		/// <summary>
		/// Converts to signed integer bitwisely(0xFFFFFFFFu => -1 without exception).
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ToSigned(this uint value) => unchecked((int)value);

		/// <summary>
		/// Converts to signed integer bitwisely(65535 => -1 without exception).
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static short ToSigned(this ushort value) => unchecked((short)value);

		/// <summary>
		/// Converts to signed integer bitwisely(255 => -1 without exception).
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static sbyte ToSigned(this byte value) => unchecked((sbyte)value);

		#endregion SignedToFromUnsigned
	}
}
