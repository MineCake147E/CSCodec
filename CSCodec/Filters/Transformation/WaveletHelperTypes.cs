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

		/// <summary>Splits the specified array to front and back halfs.</summary>
		/// <param name="source">The source.</param>
		/// <param name="front">The front elements.</param>
		/// <param name="back">The back elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SplitHalf(Span<int> source, out Span<int> front, out Span<int> back)
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.SplitHalfInternal(out front, out back);
		}

		/// <summary>Splits the specified array to front and back halfs.</summary>
		/// <param name="source">The source.</param>
		/// <param name="front">The front elements.</param>
		/// <param name="back">The back elements.</param>
		[DebuggerNonUserCode()]
		private static void SplitHalfInternal(this in Span<int> source, out Span<int> front, out Span<int> back)
		{
			front = source.Slice(0, source.Length >> 1);
			back = source.Slice(front.Length, front.Length);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="destination">The destination.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void MergeOddEven(Span<int> destination, out Span<int> even, out Span<int> odd)
		{
			if ((destination.Length & 1) == 1) throw new ArgumentException($"The {nameof(destination)}'s length must be even!");
			destination.MergeOddEvenInternal(out even, out odd);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="destination">The destination.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[DebuggerNonUserCode()]
		private static void MergeOddEvenInternal(this in Span<int> destination, out Span<int> even, out Span<int> odd)
		{
			even = destination.Slice(0, destination.Length >> 1);
			Span<int> temp = stackalloc int[even.Length];
			odd = destination.Slice(even.Length, even.Length);
			even.CopyTo(temp);
			int j = 0;
			unchecked
			{
				for (int i = 0; i < even.Length; i++, j += 2)
				{
					destination[j] = temp[i];
					destination[j | 1] = odd[i];
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

		/// <summary>Splits the specified array to front and back halfs.</summary>
		/// <param name="source">The source.</param>
		/// <param name="front">The front elements.</param>
		/// <param name="back">The back elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SplitHalf(Span<Int24> source, out Span<Int24> front, out Span<Int24> back)
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.SplitHalfInternal(out front, out back);
		}

		/// <summary>Splits the specified array to front and back halfs.</summary>
		/// <param name="source">The source.</param>
		/// <param name="front">The front elements.</param>
		/// <param name="back">The back elements.</param>
		[DebuggerNonUserCode()]
		private static void SplitHalfInternal(this in Span<Int24> source, out Span<Int24> front, out Span<Int24> back)
		{
			front = source.Slice(0, source.Length >> 1);
			back = source.Slice(front.Length, front.Length);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="destination">The destination.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void MergeOddEven(Span<Int24> destination, out Span<Int24> even, out Span<Int24> odd)
		{
			if ((destination.Length & 1) == 1) throw new ArgumentException($"The {nameof(destination)}'s length must be even!");
			destination.MergeOddEvenInternal(out even, out odd);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="destination">The destination.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[DebuggerNonUserCode()]
		private static void MergeOddEvenInternal(this in Span<Int24> destination, out Span<Int24> even, out Span<Int24> odd)
		{
			even = destination.Slice(0, destination.Length >> 1);
			Span<Int24> temp = stackalloc Int24[even.Length];
			odd = destination.Slice(even.Length, even.Length);
			even.CopyTo(temp);
			int j = 0;
			unchecked
			{
				for (int i = 0; i < even.Length; i++, j += 2)
				{
					destination[j] = temp[i];
					destination[j | 1] = odd[i];
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

		/// <summary>Splits the specified array to front and back halfs.</summary>
		/// <param name="source">The source.</param>
		/// <param name="front">The front elements.</param>
		/// <param name="back">The back elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SplitHalf(Span<short> source, out Span<short> front, out Span<short> back)
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.SplitHalfInternal(out front, out back);
		}

		/// <summary>Splits the specified array to front and back halfs.</summary>
		/// <param name="source">The source.</param>
		/// <param name="front">The front elements.</param>
		/// <param name="back">The back elements.</param>
		[DebuggerNonUserCode()]
		private static void SplitHalfInternal(this in Span<short> source, out Span<short> front, out Span<short> back)
		{
			front = source.Slice(0, source.Length >> 1);
			back = source.Slice(front.Length, front.Length);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="destination">The destination.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void MergeOddEven(Span<short> destination, out Span<short> even, out Span<short> odd)
		{
			if ((destination.Length & 1) == 1) throw new ArgumentException($"The {nameof(destination)}'s length must be even!");
			destination.MergeOddEvenInternal(out even, out odd);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="destination">The destination.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[DebuggerNonUserCode()]
		private static void MergeOddEvenInternal(this in Span<short> destination, out Span<short> even, out Span<short> odd)
		{
			even = destination.Slice(0, destination.Length >> 1);
			Span<short> temp = stackalloc short[even.Length];
			odd = destination.Slice(even.Length, even.Length);
			even.CopyTo(temp);
			int j = 0;
			unchecked
			{
				for (int i = 0; i < even.Length; i++, j += 2)
				{
					destination[j] = temp[i];
					destination[j | 1] = odd[i];
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

		/// <summary>Splits the specified array to front and back halfs.</summary>
		/// <param name="source">The source.</param>
		/// <param name="front">The front elements.</param>
		/// <param name="back">The back elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SplitHalf(Span<sbyte> source, out Span<sbyte> front, out Span<sbyte> back)
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.SplitHalfInternal(out front, out back);
		}

		/// <summary>Splits the specified array to front and back halfs.</summary>
		/// <param name="source">The source.</param>
		/// <param name="front">The front elements.</param>
		/// <param name="back">The back elements.</param>
		[DebuggerNonUserCode()]
		private static void SplitHalfInternal(this in Span<sbyte> source, out Span<sbyte> front, out Span<sbyte> back)
		{
			front = source.Slice(0, source.Length >> 1);
			back = source.Slice(front.Length, front.Length);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="destination">The destination.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void MergeOddEven(Span<sbyte> destination, out Span<sbyte> even, out Span<sbyte> odd)
		{
			if ((destination.Length & 1) == 1) throw new ArgumentException($"The {nameof(destination)}'s length must be even!");
			destination.MergeOddEvenInternal(out even, out odd);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="destination">The destination.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[DebuggerNonUserCode()]
		private static void MergeOddEvenInternal(this in Span<sbyte> destination, out Span<sbyte> even, out Span<sbyte> odd)
		{
			even = destination.Slice(0, destination.Length >> 1);
			Span<sbyte> temp = stackalloc sbyte[even.Length];
			odd = destination.Slice(even.Length, even.Length);
			even.CopyTo(temp);
			int j = 0;
			unchecked
			{
				for (int i = 0; i < even.Length; i++, j += 2)
				{
					destination[j] = temp[i];
					destination[j | 1] = odd[i];
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

		/// <summary>Splits the specified array to front and back halfs.</summary>
		/// <param name="source">The source.</param>
		/// <param name="front">The front elements.</param>
		/// <param name="back">The back elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SplitHalf(Span<float> source, out Span<float> front, out Span<float> back)
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.SplitHalfInternal(out front, out back);
		}

		/// <summary>Splits the specified array to front and back halfs.</summary>
		/// <param name="source">The source.</param>
		/// <param name="front">The front elements.</param>
		/// <param name="back">The back elements.</param>
		[DebuggerNonUserCode()]
		private static void SplitHalfInternal(this in Span<float> source, out Span<float> front, out Span<float> back)
		{
			front = source.Slice(0, source.Length >> 1);
			back = source.Slice(front.Length, front.Length);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="destination">The destination.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void MergeOddEven(Span<float> destination, out Span<float> even, out Span<float> odd)
		{
			if ((destination.Length & 1) == 1) throw new ArgumentException($"The {nameof(destination)}'s length must be even!");
			destination.MergeOddEvenInternal(out even, out odd);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="destination">The destination.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[DebuggerNonUserCode()]
		private static void MergeOddEvenInternal(this in Span<float> destination, out Span<float> even, out Span<float> odd)
		{
			even = destination.Slice(0, destination.Length >> 1);
			Span<float> temp = stackalloc float[even.Length];
			odd = destination.Slice(even.Length, even.Length);
			even.CopyTo(temp);
			int j = 0;
			unchecked
			{
				for (int i = 0; i < even.Length; i++, j += 2)
				{
					destination[j] = temp[i];
					destination[j | 1] = odd[i];
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

		/// <summary>Splits the specified array to front and back halfs.</summary>
		/// <param name="source">The source.</param>
		/// <param name="front">The front elements.</param>
		/// <param name="back">The back elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SplitHalf(Span<double> source, out Span<double> front, out Span<double> back)
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.SplitHalfInternal(out front, out back);
		}

		/// <summary>Splits the specified array to front and back halfs.</summary>
		/// <param name="source">The source.</param>
		/// <param name="front">The front elements.</param>
		/// <param name="back">The back elements.</param>
		[DebuggerNonUserCode()]
		private static void SplitHalfInternal(this in Span<double> source, out Span<double> front, out Span<double> back)
		{
			front = source.Slice(0, source.Length >> 1);
			back = source.Slice(front.Length, front.Length);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="destination">The destination.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		/// <exception cref="ArgumentException">The source's length was odd.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void MergeOddEven(Span<double> destination, out Span<double> even, out Span<double> odd)
		{
			if ((destination.Length & 1) == 1) throw new ArgumentException($"The {nameof(destination)}'s length must be even!");
			destination.MergeOddEvenInternal(out even, out odd);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="destination">The destination.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[DebuggerNonUserCode()]
		private static void MergeOddEvenInternal(this in Span<double> destination, out Span<double> even, out Span<double> odd)
		{
			even = destination.Slice(0, destination.Length >> 1);
			Span<double> temp = stackalloc double[even.Length];
			odd = destination.Slice(even.Length, even.Length);
			even.CopyTo(temp);
			int j = 0;
			unchecked
			{
				for (int i = 0; i < even.Length; i++, j += 2)
				{
					destination[j] = temp[i];
					destination[j | 1] = odd[i];
				}
			}
			//temp gets released here
		}
	
	}
}
