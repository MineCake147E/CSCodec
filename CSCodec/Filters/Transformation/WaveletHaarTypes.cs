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
		/// <summary>
		/// Performs The Multi-Level Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarMultiLevel(Span<int> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<int> even, out Span<int> odd);
			do
			{
				HaarInternal(even, odd);
				even.ArrangeOddEven(out var newEven, out odd);
				even = newEven;
			} while (even.Length > 1);
			HaarInternal(even, odd);
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void HaarInverseMultiLevelInternal(Span<int> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.SplitHalfInternal(out var even2, out var odd2);
					HaarInverseInternal(even2, odd2);
					return;
				}
				span.SplitHalfInternal(out var front, out var back);
				HaarInverseMultiLevelInternal(front);
				HaarInverseInternal(front, back);
				span.MergeOddEvenInternal(out var even, out var odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarInverseMultiLevel(Span<int> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			HaarInverseMultiLevelInternal(span);
		}

		/// <summary>
		/// Performs The Multi-Level Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarMultiLevel(Span<Int24> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<Int24> even, out Span<Int24> odd);
			do
			{
				HaarInternal(even, odd);
				even.ArrangeOddEven(out var newEven, out odd);
				even = newEven;
			} while (even.Length > 1);
			HaarInternal(even, odd);
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void HaarInverseMultiLevelInternal(Span<Int24> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.SplitHalfInternal(out var even2, out var odd2);
					HaarInverseInternal(even2, odd2);
					return;
				}
				span.SplitHalfInternal(out var front, out var back);
				HaarInverseMultiLevelInternal(front);
				HaarInverseInternal(front, back);
				span.MergeOddEvenInternal(out var even, out var odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarInverseMultiLevel(Span<Int24> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			HaarInverseMultiLevelInternal(span);
		}

		/// <summary>
		/// Performs The Multi-Level Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarMultiLevel(Span<short> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<short> even, out Span<short> odd);
			do
			{
				HaarInternal(even, odd);
				even.ArrangeOddEven(out var newEven, out odd);
				even = newEven;
			} while (even.Length > 1);
			HaarInternal(even, odd);
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void HaarInverseMultiLevelInternal(Span<short> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.SplitHalfInternal(out var even2, out var odd2);
					HaarInverseInternal(even2, odd2);
					return;
				}
				span.SplitHalfInternal(out var front, out var back);
				HaarInverseMultiLevelInternal(front);
				HaarInverseInternal(front, back);
				span.MergeOddEvenInternal(out var even, out var odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarInverseMultiLevel(Span<short> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			HaarInverseMultiLevelInternal(span);
		}

		/// <summary>
		/// Performs The Multi-Level Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarMultiLevel(Span<sbyte> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<sbyte> even, out Span<sbyte> odd);
			do
			{
				HaarInternal(even, odd);
				even.ArrangeOddEven(out var newEven, out odd);
				even = newEven;
			} while (even.Length > 1);
			HaarInternal(even, odd);
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void HaarInverseMultiLevelInternal(Span<sbyte> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.SplitHalfInternal(out var even2, out var odd2);
					HaarInverseInternal(even2, odd2);
					return;
				}
				span.SplitHalfInternal(out var front, out var back);
				HaarInverseMultiLevelInternal(front);
				HaarInverseInternal(front, back);
				span.MergeOddEvenInternal(out var even, out var odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarInverseMultiLevel(Span<sbyte> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			HaarInverseMultiLevelInternal(span);
		}

		/// <summary>
		/// Performs The Multi-Level Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarMultiLevel(Span<float> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<float> even, out Span<float> odd);
			do
			{
				HaarInternal(even, odd);
				even.ArrangeOddEven(out var newEven, out odd);
				even = newEven;
			} while (even.Length > 1);
			HaarInternal(even, odd);
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void HaarInverseMultiLevelInternal(Span<float> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.SplitHalfInternal(out var even2, out var odd2);
					HaarInverseInternal(even2, odd2);
					return;
				}
				span.SplitHalfInternal(out var front, out var back);
				HaarInverseMultiLevelInternal(front);
				HaarInverseInternal(front, back);
				span.MergeOddEvenInternal(out var even, out var odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarInverseMultiLevel(Span<float> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			HaarInverseMultiLevelInternal(span);
		}

		/// <summary>
		/// Performs The Multi-Level Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarMultiLevel(Span<double> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<double> even, out Span<double> odd);
			do
			{
				HaarInternal(even, odd);
				even.ArrangeOddEven(out var newEven, out odd);
				even = newEven;
			} while (even.Length > 1);
			HaarInternal(even, odd);
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void HaarInverseMultiLevelInternal(Span<double> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.SplitHalfInternal(out var even2, out var odd2);
					HaarInverseInternal(even2, odd2);
					return;
				}
				span.SplitHalfInternal(out var front, out var back);
				HaarInverseMultiLevelInternal(front);
				HaarInverseInternal(front, back);
				span.MergeOddEvenInternal(out var even, out var odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarInverseMultiLevel(Span<double> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			HaarInverseMultiLevelInternal(span);
		}

		/// <summary>
		/// Performs The 1-level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		private static void HaarInverseInternal(in Span<Int24> even, in Span<Int24> odd)
		{
			unchecked
			{
				for (int i = 0; i < even.Length; i++)
				{
					//-Update
					even[i] -= (Int24)(odd[i] >> 1);
				}
				for (int i = 0; i < odd.Length; i++)
				{
					//+Predict
					odd[i] += even[i];
				}
			}
		}

		/// <summary>
		/// Performs The 1-level Haar Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		private static void HaarInternal(in Span<Int24> even, in Span<Int24> odd)
		{
			unchecked
			{
				for (int i = 0; i < odd.Length; i++)
				{
					//-Predict
					odd[i] -= even[i];
				}
				for (int i = 0; i < even.Length; i++)
				{
					//+Update
					even[i] += (Int24)(odd[i] >> 1);
				}
			}
		}

		/// <summary>
		/// Performs The 1-level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		private static void HaarInverseInternal(in Span<short> even, in Span<short> odd)
		{
			unchecked
			{
				for (int i = 0; i < even.Length; i++)
				{
					//-Update
					even[i] -= (short)(odd[i] >> 1);
				}
				for (int i = 0; i < odd.Length; i++)
				{
					//+Predict
					odd[i] += even[i];
				}
			}
		}

		/// <summary>
		/// Performs The 1-level Haar Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		private static void HaarInternal(in Span<short> even, in Span<short> odd)
		{
			unchecked
			{
				for (int i = 0; i < odd.Length; i++)
				{
					//-Predict
					odd[i] -= even[i];
				}
				for (int i = 0; i < even.Length; i++)
				{
					//+Update
					even[i] += (short)(odd[i] >> 1);
				}
			}
		}

		/// <summary>
		/// Performs The 1-level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		private static void HaarInverseInternal(in Span<sbyte> even, in Span<sbyte> odd)
		{
			unchecked
			{
				for (int i = 0; i < even.Length; i++)
				{
					//-Update
					even[i] -= (sbyte)(odd[i] >> 1);
				}
				for (int i = 0; i < odd.Length; i++)
				{
					//+Predict
					odd[i] += even[i];
				}
			}
		}

		/// <summary>
		/// Performs The 1-level Haar Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		private static void HaarInternal(in Span<sbyte> even, in Span<sbyte> odd)
		{
			unchecked
			{
				for (int i = 0; i < odd.Length; i++)
				{
					//-Predict
					odd[i] -= even[i];
				}
				for (int i = 0; i < even.Length; i++)
				{
					//+Update
					even[i] += (sbyte)(odd[i] >> 1);
				}
			}
		}

		/// <summary>
		/// Performs The 1-level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		private static void HaarInverseInternal(in Span<int> even, in Span<int> odd)
		{
			unchecked
			{
				for (int i = 0; i < even.Length; i++)
				{
					//-Update
					even[i] -= odd[i] / 2;
				}
				for (int i = 0; i < odd.Length; i++)
				{
					//+Predict
					odd[i] += even[i];
				}
			}
		}

		/// <summary>
		/// Performs The 1-level Haar Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		private static void HaarInternal(in Span<int> even, in Span<int> odd)
		{
			unchecked
			{
				for (int i = 0; i < odd.Length; i++)
				{
					//-Predict
					odd[i] -= even[i];
				}
				for (int i = 0; i < even.Length; i++)
				{
					//+Update
					even[i] += odd[i] / 2;  //even = (even + odd) / 2
				}
			}
		}

		/// <summary>
		/// Performs The 1-level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		private static void HaarInverseInternal(in Span<float> even, in Span<float> odd)
		{
			unchecked
			{
				for (int i = 0; i < even.Length; i++)
				{
					//-Update
					even[i] -= odd[i] / 2;
				}
				for (int i = 0; i < odd.Length; i++)
				{
					//+Predict
					odd[i] += even[i];
				}
			}
		}

		/// <summary>
		/// Performs The 1-level Haar Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		private static void HaarInternal(in Span<float> even, in Span<float> odd)
		{
			unchecked
			{
				for (int i = 0; i < odd.Length; i++)
				{
					//-Predict
					odd[i] -= even[i];
				}
				for (int i = 0; i < even.Length; i++)
				{
					//+Update
					even[i] += odd[i] / 2;
				}
			}
		}

		/// <summary>
		/// Performs The 1-level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		private static void HaarInverseInternal(in Span<double> even, in Span<double> odd)
		{
			unchecked
			{
				for (int i = 0; i < even.Length; i++)
				{
					//-Update
					even[i] -= odd[i] / 2;
				}
				for (int i = 0; i < odd.Length; i++)
				{
					//+Predict
					odd[i] += even[i];
				}
			}
		}

		/// <summary>
		/// Performs The 1-level Haar Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even elements.</param>
		/// <param name="odd">The odd elements.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		private static void HaarInternal(in Span<double> even, in Span<double> odd)
		{
			unchecked
			{
				for (int i = 0; i < odd.Length; i++)
				{
					//-Predict
					odd[i] -= even[i];
				}
				for (int i = 0; i < even.Length; i++)
				{
					//+Update
					even[i] += odd[i] / 2;
				}
			}
		}
	}
}
