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
		/// Performs The Multi-Level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53MultiLevel(Span<System.Int32> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<System.Int32> even, out Span<System.Int32> odd);
			do
			{
				CDF53Internal(even, odd);
				even.ArrangeOddEven(out even, out odd);
			} while (even.Length > 1);
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void CDF53InverseMultiLevelInternal(Span<System.Int32> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.ArrangeOddEven(out var even2, out var odd2);
					CDF53InverseInternal(even2, odd2);
					return;
				}
				span.ArrangeOddEven(out var even, out var odd);
				CDF53InverseMultiLevelInternal(even);
				CDF53InverseInternal(even, odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53InverseMultiLevel(Span<System.Int32> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			CDF53InverseMultiLevelInternal(span);
		}
		
		/// <summary>
		/// Performs The Multi-Level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53MultiLevel(Span<System.Int16> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<System.Int16> even, out Span<System.Int16> odd);
			do
			{
				CDF53Internal(even, odd);
				even.ArrangeOddEven(out even, out odd);
			} while (even.Length > 1);
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void CDF53InverseMultiLevelInternal(Span<System.Int16> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.ArrangeOddEven(out var even2, out var odd2);
					CDF53InverseInternal(even2, odd2);
					return;
				}
				span.ArrangeOddEven(out var even, out var odd);
				CDF53InverseMultiLevelInternal(even);
				CDF53InverseInternal(even, odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53InverseMultiLevel(Span<System.Int16> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			CDF53InverseMultiLevelInternal(span);
		}
		
		/// <summary>
		/// Performs The Multi-Level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53MultiLevel(Span<System.SByte> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<System.SByte> even, out Span<System.SByte> odd);
			do
			{
				CDF53Internal(even, odd);
				even.ArrangeOddEven(out even, out odd);
			} while (even.Length > 1);
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void CDF53InverseMultiLevelInternal(Span<System.SByte> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.ArrangeOddEven(out var even2, out var odd2);
					CDF53InverseInternal(even2, odd2);
					return;
				}
				span.ArrangeOddEven(out var even, out var odd);
				CDF53InverseMultiLevelInternal(even);
				CDF53InverseInternal(even, odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53InverseMultiLevel(Span<System.SByte> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			CDF53InverseMultiLevelInternal(span);
		}
		
		/// <summary>
		/// Performs The Multi-Level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53MultiLevel(Span<System.Single> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<System.Single> even, out Span<System.Single> odd);
			do
			{
				CDF53Internal(even, odd);
				even.ArrangeOddEven(out even, out odd);
			} while (even.Length > 1);
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void CDF53InverseMultiLevelInternal(Span<System.Single> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.ArrangeOddEven(out var even2, out var odd2);
					CDF53InverseInternal(even2, odd2);
					return;
				}
				span.ArrangeOddEven(out var even, out var odd);
				CDF53InverseMultiLevelInternal(even);
				CDF53InverseInternal(even, odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53InverseMultiLevel(Span<System.Single> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			CDF53InverseMultiLevelInternal(span);
		}
		
		/// <summary>
		/// Performs The Multi-Level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53MultiLevel(Span<System.Double> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<System.Double> even, out Span<System.Double> odd);
			do
			{
				CDF53Internal(even, odd);
				even.ArrangeOddEven(out even, out odd);
			} while (even.Length > 1);
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void CDF53InverseMultiLevelInternal(Span<System.Double> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.ArrangeOddEven(out var even2, out var odd2);
					CDF53InverseInternal(even2, odd2);
					return;
				}
				span.ArrangeOddEven(out var even, out var odd);
				CDF53InverseMultiLevelInternal(even);
				CDF53InverseInternal(even, odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void CDF53InverseMultiLevel(Span<System.Double> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			CDF53InverseMultiLevelInternal(span);
		}
		
		
		/// <summary>
		/// Performs The 1-level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even.</param>
		/// <param name="odd">The odd.</param>
		private static void CDF53Internal(in Span<System.Int16> even, in Span<System.Int16> odd)
		{
			unchecked
			{
				for (int i = 0; i < odd.Length - 1; i++)
				{
					//-Predict
					odd[i] -= (System.Int16)((even[i] + even[i + 1]) >> 1);
				}
				odd[odd.Length - 1] -= even[even.Length - 1];
				even[0] += (System.Int16)((odd[0] + 1) >> 1);
				for (int i = 1; i < even.Length; i++)
				{
					//+Update
					even[i] += (System.Int16)((odd[i - 1] + odd[i] + 2) >> 2);
				}
			}
		}

		/// <summary>
		/// Performs The 1-level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even.</param>
		/// <param name="odd">The odd.</param>
		private static void CDF53InverseInternal(in Span<System.Int16> even, in Span<System.Int16> odd)
		{
			unchecked
			{
				for (int i = 0; i < odd.Length - 1; i++)
				{
					//-Update
					odd[i] -= (System.Int16)((even[i - 1] + even[i] + 2) >> 2);
				}
				odd[odd.Length - 1] -= (System.Int16)((even[0] + 1) >> 1);
				even[0] += odd[even.Length - 1];
				for (int i = 1; i < even.Length; i++)
				{
					//+Predict
					even[i] += (System.Int16)((odd[i] + odd[i + 1]) >> 1);
				}
			}
		}
		
		/// <summary>
		/// Performs The 1-level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even.</param>
		/// <param name="odd">The odd.</param>
		private static void CDF53Internal(in Span<System.SByte> even, in Span<System.SByte> odd)
		{
			unchecked
			{
				for (int i = 0; i < odd.Length - 1; i++)
				{
					//-Predict
					odd[i] -= (System.SByte)((even[i] + even[i + 1]) >> 1);
				}
				odd[odd.Length - 1] -= even[even.Length - 1];
				even[0] += (System.SByte)((odd[0] + 1) >> 1);
				for (int i = 1; i < even.Length; i++)
				{
					//+Update
					even[i] += (System.SByte)((odd[i - 1] + odd[i] + 2) >> 2);
				}
			}
		}

		/// <summary>
		/// Performs The 1-level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even.</param>
		/// <param name="odd">The odd.</param>
		private static void CDF53InverseInternal(in Span<System.SByte> even, in Span<System.SByte> odd)
		{
			unchecked
			{
				for (int i = 0; i < odd.Length - 1; i++)
				{
					//-Update
					odd[i] -= (System.SByte)((even[i - 1] + even[i] + 2) >> 2);
				}
				odd[odd.Length - 1] -= (System.SByte)((even[0] + 1) >> 1);
				even[0] += odd[even.Length - 1];
				for (int i = 1; i < even.Length; i++)
				{
					//+Predict
					even[i] += (System.SByte)((odd[i] + odd[i + 1]) >> 1);
				}
			}
		}
				
		/// <summary>
		/// Performs The 1-level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even.</param>
		/// <param name="odd">The odd.</param>
		private static void CDF53Internal(in Span<System.Single> even, in Span<System.Single> odd)
		{
			unchecked
			{
				for (int i = 0; i < odd.Length - 1; i++)
				{
					//-Predict
					odd[i] -= 0.5f * (even[i] + even[i + 1]);
				}
				odd[odd.Length - 1] -= even[even.Length - 1];
				even[0] += 0.5f * odd[0];
				for (int i = 1; i < even.Length; i++)
				{
					//+Update
					even[i] += 0.25f * (odd[i - 1] + odd[i]);
				}
			}
		}

		/// <summary>
		/// Performs The 1-level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even.</param>
		/// <param name="odd">The odd.</param>
		private static void CDF53InverseInternal(in Span<System.Single> even, in Span<System.Single> odd)
		{
			unchecked
			{
				for (int i = 0; i < odd.Length - 1; i++)
				{
					//-Update
					odd[i] -= 0.25f * (even[i - 1] + even[i]);
				}
				odd[odd.Length - 1] -= 0.5f * even[0];
				even[0] += odd[even.Length - 1];
				for (int i = 1; i < even.Length; i++)
				{
					//+Predict
					even[i] += 0.5f * (odd[i] + odd[i + 1]);
				}
			}
		}
		
		/// <summary>
		/// Performs The 1-level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even.</param>
		/// <param name="odd">The odd.</param>
		private static void CDF53Internal(in Span<System.Double> even, in Span<System.Double> odd)
		{
			unchecked
			{
				for (int i = 0; i < odd.Length - 1; i++)
				{
					//-Predict
					odd[i] -= 0.5f * (even[i] + even[i + 1]);
				}
				odd[odd.Length - 1] -= even[even.Length - 1];
				even[0] += 0.5f * odd[0];
				for (int i = 1; i < even.Length; i++)
				{
					//+Update
					even[i] += 0.25f * (odd[i - 1] + odd[i]);
				}
			}
		}

		/// <summary>
		/// Performs The 1-level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even.</param>
		/// <param name="odd">The odd.</param>
		private static void CDF53InverseInternal(in Span<System.Double> even, in Span<System.Double> odd)
		{
			unchecked
			{
				for (int i = 0; i < odd.Length - 1; i++)
				{
					//-Update
					odd[i] -= 0.25f * (even[i - 1] + even[i]);
				}
				odd[odd.Length - 1] -= 0.5f * even[0];
				even[0] += odd[even.Length - 1];
				for (int i = 1; i < even.Length; i++)
				{
					//+Predict
					even[i] += 0.5f * (odd[i] + odd[i + 1]);
				}
			}
		}
		
	}
}
