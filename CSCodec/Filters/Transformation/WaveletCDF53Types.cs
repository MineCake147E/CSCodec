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
		/// Performs The Multi-Level Cohen–Daubechies–Feauveau (LeGall) 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53MultiLevel(Span<int> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<int> even, out Span<int> odd);
			do
			{
				CDF53Internal(even, odd);
				even.ArrangeOddEven(out var newEven, out odd);
				even = newEven;
			} while (even.Length > 1);
			CDF53Internal(even, odd);	//Last Loop is equivalent to Haar transform
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Cohen–Daubechies–Feauveau (LeGall) 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void CDF53InverseMultiLevelInternal(Span<int> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.SplitHalfInternal(out var even2, out var odd2);
					CDF53InverseInternal(even2, odd2);	//Last Loop is equivalent to Haar transform
					return;
				}
				span.SplitHalfInternal(out var front, out var back);
				CDF53InverseMultiLevelInternal(front);
				CDF53InverseInternal(front, back);
				span.MergeOddEvenInternal(out var even, out var odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Cohen–Daubechies–Feauveau (LeGall) 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53InverseMultiLevel(Span<int> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			CDF53InverseMultiLevelInternal(span);
		}

		
		/// <summary>
		/// Performs The Multi-Level Cohen–Daubechies–Feauveau (LeGall) 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53MultiLevel(Span<Int24> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<Int24> even, out Span<Int24> odd);
			do
			{
				CDF53Internal(even, odd);
				even.ArrangeOddEven(out var newEven, out odd);
				even = newEven;
			} while (even.Length > 1);
			CDF53Internal(even, odd);	//Last Loop is equivalent to Haar transform
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Cohen–Daubechies–Feauveau (LeGall) 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void CDF53InverseMultiLevelInternal(Span<Int24> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.SplitHalfInternal(out var even2, out var odd2);
					CDF53InverseInternal(even2, odd2);	//Last Loop is equivalent to Haar transform
					return;
				}
				span.SplitHalfInternal(out var front, out var back);
				CDF53InverseMultiLevelInternal(front);
				CDF53InverseInternal(front, back);
				span.MergeOddEvenInternal(out var even, out var odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Cohen–Daubechies–Feauveau (LeGall) 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53InverseMultiLevel(Span<Int24> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			CDF53InverseMultiLevelInternal(span);
		}

		
		/// <summary>
		/// Performs The Multi-Level Cohen–Daubechies–Feauveau (LeGall) 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53MultiLevel(Span<short> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<short> even, out Span<short> odd);
			do
			{
				CDF53Internal(even, odd);
				even.ArrangeOddEven(out var newEven, out odd);
				even = newEven;
			} while (even.Length > 1);
			CDF53Internal(even, odd);	//Last Loop is equivalent to Haar transform
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Cohen–Daubechies–Feauveau (LeGall) 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void CDF53InverseMultiLevelInternal(Span<short> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.SplitHalfInternal(out var even2, out var odd2);
					CDF53InverseInternal(even2, odd2);	//Last Loop is equivalent to Haar transform
					return;
				}
				span.SplitHalfInternal(out var front, out var back);
				CDF53InverseMultiLevelInternal(front);
				CDF53InverseInternal(front, back);
				span.MergeOddEvenInternal(out var even, out var odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Cohen–Daubechies–Feauveau (LeGall) 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53InverseMultiLevel(Span<short> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			CDF53InverseMultiLevelInternal(span);
		}

		
		/// <summary>
		/// Performs The Multi-Level Cohen–Daubechies–Feauveau (LeGall) 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53MultiLevel(Span<sbyte> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<sbyte> even, out Span<sbyte> odd);
			do
			{
				CDF53Internal(even, odd);
				even.ArrangeOddEven(out var newEven, out odd);
				even = newEven;
			} while (even.Length > 1);
			CDF53Internal(even, odd);	//Last Loop is equivalent to Haar transform
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Cohen–Daubechies–Feauveau (LeGall) 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void CDF53InverseMultiLevelInternal(Span<sbyte> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.SplitHalfInternal(out var even2, out var odd2);
					CDF53InverseInternal(even2, odd2);	//Last Loop is equivalent to Haar transform
					return;
				}
				span.SplitHalfInternal(out var front, out var back);
				CDF53InverseMultiLevelInternal(front);
				CDF53InverseInternal(front, back);
				span.MergeOddEvenInternal(out var even, out var odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Cohen–Daubechies–Feauveau (LeGall) 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53InverseMultiLevel(Span<sbyte> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			CDF53InverseMultiLevelInternal(span);
		}

		
		/// <summary>
		/// Performs The Multi-Level Cohen–Daubechies–Feauveau (LeGall) 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53MultiLevel(Span<float> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<float> even, out Span<float> odd);
			do
			{
				CDF53Internal(even, odd);
				even.ArrangeOddEven(out var newEven, out odd);
				even = newEven;
			} while (even.Length > 1);
			CDF53Internal(even, odd);	//Last Loop is equivalent to Haar transform
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Cohen–Daubechies–Feauveau (LeGall) 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void CDF53InverseMultiLevelInternal(Span<float> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.SplitHalfInternal(out var even2, out var odd2);
					CDF53InverseInternal(even2, odd2);	//Last Loop is equivalent to Haar transform
					return;
				}
				span.SplitHalfInternal(out var front, out var back);
				CDF53InverseMultiLevelInternal(front);
				CDF53InverseInternal(front, back);
				span.MergeOddEvenInternal(out var even, out var odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Cohen–Daubechies–Feauveau (LeGall) 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53InverseMultiLevel(Span<float> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			CDF53InverseMultiLevelInternal(span);
		}

		
		/// <summary>
		/// Performs The Multi-Level Cohen–Daubechies–Feauveau (LeGall) 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53MultiLevel(Span<double> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<double> even, out Span<double> odd);
			do
			{
				CDF53Internal(even, odd);
				even.ArrangeOddEven(out var newEven, out odd);
				even = newEven;
			} while (even.Length > 1);
			CDF53Internal(even, odd);	//Last Loop is equivalent to Haar transform
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Cohen–Daubechies–Feauveau (LeGall) 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void CDF53InverseMultiLevelInternal(Span<double> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.SplitHalfInternal(out var even2, out var odd2);
					CDF53InverseInternal(even2, odd2);	//Last Loop is equivalent to Haar transform
					return;
				}
				span.SplitHalfInternal(out var front, out var back);
				CDF53InverseMultiLevelInternal(front);
				CDF53InverseInternal(front, back);
				span.MergeOddEvenInternal(out var even, out var odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Cohen–Daubechies–Feauveau (LeGall) 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53InverseMultiLevel(Span<double> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			CDF53InverseMultiLevelInternal(span);
		}

		
		
		/// <summary>
		/// Performs The 1-level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even.</param>
		/// <param name="odd">The odd.</param>
		private static void CDF53Internal(in Span<Int24> even, in Span<Int24> odd)
		{
			unchecked
			{
				for (int i = 0; i < odd.Length - 1; i++)
				{
					//-Predict
					odd[i] -= (Int24)((even[i] + even[i + 1]) >> 1);
				}
				odd[odd.Length - 1] -= even[even.Length - 1];
				even[0] += (Int24)((odd[0] + 1) >> 1);
				for (int i = 1; i < even.Length; i++)
				{
					//+Update
					even[i] += (Int24)((odd[i - 1] + odd[i] + 2) >> 2);
				}
			}
		}

		/// <summary>
		/// Performs The Inverse 1-level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even.</param>
		/// <param name="odd">The odd.</param>
		private static void CDF53InverseInternal(in Span<Int24> even, in Span<Int24> odd)
		{
			unchecked
			{
				even[0] -= (Int24)((odd[0] + 1) >> 1);
				for (int i = 1; i < even.Length; i++)
				{
					//-Update
					even[i] -= (Int24)((odd[i - 1] + odd[i] + 2) >> 2);
				}
				for (int i = 0; i < even.Length - 1; i++)
				{
					//+Predict
					odd[i] += (Int24)((even[i] + even[i + 1]) >> 1);
				}
				odd[odd.Length - 1] += even[even.Length - 1];
			}
		}
		
		/// <summary>
		/// Performs The 1-level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even.</param>
		/// <param name="odd">The odd.</param>
		private static void CDF53Internal(in Span<short> even, in Span<short> odd)
		{
			unchecked
			{
				for (int i = 0; i < odd.Length - 1; i++)
				{
					//-Predict
					odd[i] -= (short)((even[i] + even[i + 1]) >> 1);
				}
				odd[odd.Length - 1] -= even[even.Length - 1];
				even[0] += (short)((odd[0] + 1) >> 1);
				for (int i = 1; i < even.Length; i++)
				{
					//+Update
					even[i] += (short)((odd[i - 1] + odd[i] + 2) >> 2);
				}
			}
		}

		/// <summary>
		/// Performs The Inverse 1-level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even.</param>
		/// <param name="odd">The odd.</param>
		private static void CDF53InverseInternal(in Span<short> even, in Span<short> odd)
		{
			unchecked
			{
				even[0] -= (short)((odd[0] + 1) >> 1);
				for (int i = 1; i < even.Length; i++)
				{
					//-Update
					even[i] -= (short)((odd[i - 1] + odd[i] + 2) >> 2);
				}
				for (int i = 0; i < even.Length - 1; i++)
				{
					//+Predict
					odd[i] += (short)((even[i] + even[i + 1]) >> 1);
				}
				odd[odd.Length - 1] += even[even.Length - 1];
			}
		}
		
		/// <summary>
		/// Performs The 1-level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even.</param>
		/// <param name="odd">The odd.</param>
		private static void CDF53Internal(in Span<sbyte> even, in Span<sbyte> odd)
		{
			unchecked
			{
				for (int i = 0; i < odd.Length - 1; i++)
				{
					//-Predict
					odd[i] -= (sbyte)((even[i] + even[i + 1]) >> 1);
				}
				odd[odd.Length - 1] -= even[even.Length - 1];
				even[0] += (sbyte)((odd[0] + 1) >> 1);
				for (int i = 1; i < even.Length; i++)
				{
					//+Update
					even[i] += (sbyte)((odd[i - 1] + odd[i] + 2) >> 2);
				}
			}
		}

		/// <summary>
		/// Performs The Inverse 1-level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even.</param>
		/// <param name="odd">The odd.</param>
		private static void CDF53InverseInternal(in Span<sbyte> even, in Span<sbyte> odd)
		{
			unchecked
			{
				even[0] -= (sbyte)((odd[0] + 1) >> 1);
				for (int i = 1; i < even.Length; i++)
				{
					//-Update
					even[i] -= (sbyte)((odd[i - 1] + odd[i] + 2) >> 2);
				}
				for (int i = 0; i < even.Length - 1; i++)
				{
					//+Predict
					odd[i] += (sbyte)((even[i] + even[i + 1]) >> 1);
				}
				odd[odd.Length - 1] += even[even.Length - 1];
			}
		}
				
		/// <summary>
		/// Performs The 1-level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even.</param>
		/// <param name="odd">The odd.</param>
		private static void CDF53Internal(in Span<float> even, in Span<float> odd)
		{
			unchecked
			{
				int i;
				for (i = 0; i < odd.Length - 1; i++)
				{
					//-Predict
					odd[i] -= 0.5f * (even[i] + even[i + 1]);
				}
				odd[i] -= even[i];

				even[0] += 0.5f * odd[0];
				for (i = 1; i < even.Length; i++)
				{
					//+Update
					even[i] += 0.25f * (odd[i - 1] + odd[i]);
				}
			}
		}

		/// <summary>
		/// Performs The Inverse 1-level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even.</param>
		/// <param name="odd">The odd.</param>
		private static void CDF53InverseInternal(in Span<float> even, in Span<float> odd)
		{
			unchecked
			{
				int i;
				even[0] -= 0.5f * odd[0];
				for (i = 1; i < odd.Length; i++)
				{
					//-Update
					even[i] -= 0.25f * (odd[i - 1] + odd[i]);
				}

				for (i = 0; i < even.Length - 1; i++)
				{
					//+Predict
					odd[i] += 0.5f * (even[i] + even[i + 1]);
				}
				odd[i] += even[i];
			}
		}
		
		/// <summary>
		/// Performs The 1-level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even.</param>
		/// <param name="odd">The odd.</param>
		private static void CDF53Internal(in Span<double> even, in Span<double> odd)
		{
			unchecked
			{
				int i;
				for (i = 0; i < odd.Length - 1; i++)
				{
					//-Predict
					odd[i] -= 0.5f * (even[i] + even[i + 1]);
				}
				odd[i] -= even[i];

				even[0] += 0.5f * odd[0];
				for (i = 1; i < even.Length; i++)
				{
					//+Update
					even[i] += 0.25f * (odd[i - 1] + odd[i]);
				}
			}
		}

		/// <summary>
		/// Performs The Inverse 1-level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even.</param>
		/// <param name="odd">The odd.</param>
		private static void CDF53InverseInternal(in Span<double> even, in Span<double> odd)
		{
			unchecked
			{
				int i;
				even[0] -= 0.5f * odd[0];
				for (i = 1; i < odd.Length; i++)
				{
					//-Update
					even[i] -= 0.25f * (odd[i - 1] + odd[i]);
				}

				for (i = 0; i < even.Length - 1; i++)
				{
					//+Predict
					odd[i] += 0.5f * (even[i] + even[i + 1]);
				}
				odd[i] += even[i];
			}
		}
		
	}
}
