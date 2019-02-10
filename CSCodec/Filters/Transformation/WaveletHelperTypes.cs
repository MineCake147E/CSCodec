using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
namespace CSCodec.Filters.Transformation
{
	public static partial class WaveletTransformation
	{
		//Permutation Functions
			/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SplitOddEven(Span<int> source, out Span<int> even, out Span<int> odd)
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.ArrangeOddEven(out even, out odd);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[DebuggerNonUserCode()]
		private static void ArrangeOddEven(this in Span<int> source, out Span<int> even, out Span<int> odd)
		{
			Span<int> temp = stackalloc int[source.Length];
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
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SplitOddEven(Span<Int24> source, out Span<Int24> even, out Span<Int24> odd)
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.ArrangeOddEven(out even, out odd);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[DebuggerNonUserCode()]
		private static void ArrangeOddEven(this in Span<Int24> source, out Span<Int24> even, out Span<Int24> odd)
		{
			Span<Int24> temp = stackalloc Int24[source.Length];
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
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SplitOddEven(Span<short> source, out Span<short> even, out Span<short> odd)
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.ArrangeOddEven(out even, out odd);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[DebuggerNonUserCode()]
		private static void ArrangeOddEven(this in Span<short> source, out Span<short> even, out Span<short> odd)
		{
			Span<short> temp = stackalloc short[source.Length];
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
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SplitOddEven(Span<sbyte> source, out Span<sbyte> even, out Span<sbyte> odd)
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.ArrangeOddEven(out even, out odd);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[DebuggerNonUserCode()]
		private static void ArrangeOddEven(this in Span<sbyte> source, out Span<sbyte> even, out Span<sbyte> odd)
		{
			Span<sbyte> temp = stackalloc sbyte[source.Length];
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
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SplitOddEven(Span<float> source, out Span<float> even, out Span<float> odd)
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.ArrangeOddEven(out even, out odd);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[DebuggerNonUserCode()]
		private static void ArrangeOddEven(this in Span<float> source, out Span<float> even, out Span<float> odd)
		{
			Span<float> temp = stackalloc float[source.Length];
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
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SplitOddEven(Span<double> source, out Span<double> even, out Span<double> odd)
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.ArrangeOddEven(out even, out odd);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[DebuggerNonUserCode()]
		private static void ArrangeOddEven(this in Span<double> source, out Span<double> even, out Span<double> odd)
		{
			Span<double> temp = stackalloc double[source.Length];
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
