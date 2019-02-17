using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	/// <summary>
	/// Utility for <see cref="Span{T}"/>s.
	/// </summary>
	public static class SpanUtils
	{
		/// <summary>Splits the specified array to front and back halfs.</summary>
		/// <param name="source">The source.</param>
		/// <param name="front">The front elements.</param>
		/// <param name="back">The back elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SplitHalf<T>(this in Span<T> source, out Span<T> front, out Span<T> back)
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.SplitHalfInternal(out front, out back);
		}

		/// <summary>Splits the specified array to front and back halfs.</summary>
		/// <param name="source">The source.</param>
		/// <param name="front">The front elements.</param>
		/// <param name="back">The back elements.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		private static void SplitHalfInternal<T>(this in Span<T> source, out Span<T> front, out Span<T> back)
		{
			front = source.Slice(0, source.Length >> 1);
			back = source.Slice(front.Length, front.Length);
		}

		/// <summary>
		/// Splits the specified array to 1-4 quarters.
		/// </summary>
		/// <typeparam name="T">Type of Span</typeparam>
		/// <param name="source">The source.</param>
		/// <param name="head">The head.</param>
		/// <param name="q2">The q2.</param>
		/// <param name="q3">The q3.</param>
		/// <param name="tail">The tail.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SplitQuarter<T>(this in Span<T> source, out Span<T> head, out Span<T> q2, out Span<T> q3, out Span<T> tail)
		{
			if ((source.Length & 3) != 0) throw new ArgumentException($"The {nameof(source)}'s length must be Multiples of 4!");
			source.SplitHalfInternal(out var front, out var back);
			front.SplitHalfInternal(out head, out q2);
			back.SplitHalfInternal(out q3, out tail);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <typeparam name="T">Type of Span</typeparam>
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SplitOddEven<T>(this Span<T> source, out Span<T> even, out Span<T> odd)
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.ArrangeOddEven(out even, out odd);
		}

		/// <summary>Splits the specified array of <c>unmanaged</c> type to odd even index.</summary>
		/// <typeparam name="T">Type of Span that can <c>stackalloc</c>.</typeparam>
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SplitOddEvenSlim<T>(this Span<T> source, out Span<T> even, out Span<T> odd) where T : unmanaged
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.ArrangeOddEvenUnmanaged(out even, out odd);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <typeparam name="T">Type of Span</typeparam>
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		private static void ArrangeOddEven<T>(this Span<T> source, out Span<T> even, out Span<T> odd)
		{
			Span<T> temp = new T[source.Length];
			source.CopyTo(temp);
			even = source.Slice(0, source.Length >> 1);
			odd = source.Slice(even.Length, even.Length);
			int j = 0;
			unchecked
			{
				for (int i = 0; i < even.Length; i++, j += 2)
				{
					even[i] = temp[j];
					odd[i] = temp[j | 1];
				}
			}
			//temp gets released here
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <typeparam name="T">Type of Span</typeparam>
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		private static void ArrangeOddEvenUnmanaged<T>(this Span<T> source, out Span<T> even, out Span<T> odd) where T : unmanaged
		{
			Span<T> temp = stackalloc T[source.Length];
			source.CopyTo(temp);
			even = source.Slice(0, source.Length >> 1);
			odd = source.Slice(even.Length, even.Length);
			int j = 0;
			unchecked
			{
				for (int i = 0; i < even.Length; i++, j += 2)
				{
					even[i] = temp[j];
					odd[i] = temp[j | 1];
				}
			}
			//temp gets released here
		}
	}
}
