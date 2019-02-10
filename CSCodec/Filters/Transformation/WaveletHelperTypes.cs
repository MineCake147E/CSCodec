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
		public static void SplitOddEven(Span<System.Int32> source, out Span<System.Int32> even, out Span<System.Int32> odd)
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.ArrangeOddEven(out even, out odd);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[DebuggerNonUserCode()]
		private static void ArrangeOddEven(this in Span<System.Int32> source, out Span<System.Int32> even, out Span<System.Int32> odd)
		{
			Span<System.Int32> temp = stackalloc System.Int32[source.Length];
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
		public static void SplitOddEven(Span<System.Int64> source, out Span<System.Int64> even, out Span<System.Int64> odd)
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.ArrangeOddEven(out even, out odd);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[DebuggerNonUserCode()]
		private static void ArrangeOddEven(this in Span<System.Int64> source, out Span<System.Int64> even, out Span<System.Int64> odd)
		{
			Span<System.Int64> temp = stackalloc System.Int64[source.Length];
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
		public static void SplitOddEven(Span<System.Int16> source, out Span<System.Int16> even, out Span<System.Int16> odd)
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.ArrangeOddEven(out even, out odd);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[DebuggerNonUserCode()]
		private static void ArrangeOddEven(this in Span<System.Int16> source, out Span<System.Int16> even, out Span<System.Int16> odd)
		{
			Span<System.Int16> temp = stackalloc System.Int16[source.Length];
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
		public static void SplitOddEven(Span<System.SByte> source, out Span<System.SByte> even, out Span<System.SByte> odd)
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.ArrangeOddEven(out even, out odd);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[DebuggerNonUserCode()]
		private static void ArrangeOddEven(this in Span<System.SByte> source, out Span<System.SByte> even, out Span<System.SByte> odd)
		{
			Span<System.SByte> temp = stackalloc System.SByte[source.Length];
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
		public static void SplitOddEven(Span<System.Single> source, out Span<System.Single> even, out Span<System.Single> odd)
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.ArrangeOddEven(out even, out odd);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[DebuggerNonUserCode()]
		private static void ArrangeOddEven(this in Span<System.Single> source, out Span<System.Single> even, out Span<System.Single> odd)
		{
			Span<System.Single> temp = stackalloc System.Single[source.Length];
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
		public static void SplitOddEven(Span<System.Double> source, out Span<System.Double> even, out Span<System.Double> odd)
		{
			if ((source.Length & 1) == 1) throw new ArgumentException($"The {nameof(source)}'s length must be even!");
			source.ArrangeOddEven(out even, out odd);
		}

		/// <summary>Splits the specified array to odd even index.</summary>
		/// <param name="source">The source.</param>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[DebuggerNonUserCode()]
		private static void ArrangeOddEven(this in Span<System.Double> source, out Span<System.Double> even, out Span<System.Double> odd)
		{
			Span<System.Double> temp = stackalloc System.Double[source.Length];
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
