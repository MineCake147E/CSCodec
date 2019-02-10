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
		public static void HaarMultiLevel(Span<System.Int32> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<System.Int32> even, out Span<System.Int32> odd);
			do
			{
				HaarInternal(even, odd);
				even.ArrangeOddEven(out even, out odd);
			} while (even.Length > 1);
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void HaarInverseMultiLevelInternal(Span<System.Int32> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.ArrangeOddEven(out var even2, out var odd2);
					HaarInverseInternal(even2, odd2);
					return;
				}
				span.ArrangeOddEven(out var even, out var odd);
				HaarInverseMultiLevelInternal(even);
				HaarInverseInternal(even, odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarInverseMultiLevel(Span<System.Int32> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			HaarInverseMultiLevelInternal(span);
		}
		
		/// <summary>
		/// Performs The Multi-Level Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarMultiLevel(Span<System.Int16> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<System.Int16> even, out Span<System.Int16> odd);
			do
			{
				HaarInternal(even, odd);
				even.ArrangeOddEven(out even, out odd);
			} while (even.Length > 1);
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void HaarInverseMultiLevelInternal(Span<System.Int16> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.ArrangeOddEven(out var even2, out var odd2);
					HaarInverseInternal(even2, odd2);
					return;
				}
				span.ArrangeOddEven(out var even, out var odd);
				HaarInverseMultiLevelInternal(even);
				HaarInverseInternal(even, odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarInverseMultiLevel(Span<System.Int16> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			HaarInverseMultiLevelInternal(span);
		}
		
		/// <summary>
		/// Performs The Multi-Level Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarMultiLevel(Span<System.SByte> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<System.SByte> even, out Span<System.SByte> odd);
			do
			{
				HaarInternal(even, odd);
				even.ArrangeOddEven(out even, out odd);
			} while (even.Length > 1);
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void HaarInverseMultiLevelInternal(Span<System.SByte> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.ArrangeOddEven(out var even2, out var odd2);
					HaarInverseInternal(even2, odd2);
					return;
				}
				span.ArrangeOddEven(out var even, out var odd);
				HaarInverseMultiLevelInternal(even);
				HaarInverseInternal(even, odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarInverseMultiLevel(Span<System.SByte> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			HaarInverseMultiLevelInternal(span);
		}
		
		/// <summary>
		/// Performs The Multi-Level Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarMultiLevel(Span<System.Single> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<System.Single> even, out Span<System.Single> odd);
			do
			{
				HaarInternal(even, odd);
				even.ArrangeOddEven(out even, out odd);
			} while (even.Length > 1);
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void HaarInverseMultiLevelInternal(Span<System.Single> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.ArrangeOddEven(out var even2, out var odd2);
					HaarInverseInternal(even2, odd2);
					return;
				}
				span.ArrangeOddEven(out var even, out var odd);
				HaarInverseMultiLevelInternal(even);
				HaarInverseInternal(even, odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarInverseMultiLevel(Span<System.Single> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			HaarInverseMultiLevelInternal(span);
		}
		
		/// <summary>
		/// Performs The Multi-Level Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarMultiLevel(Span<System.Double> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			span.ArrangeOddEven(out Span<System.Double> even, out Span<System.Double> odd);
			do
			{
				HaarInternal(even, odd);
				even.ArrangeOddEven(out even, out odd);
			} while (even.Length > 1);
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		private static void HaarInverseMultiLevelInternal(Span<System.Double> span)
		{
			unchecked
			{
				if (span.Length == 2)
				{
					span.ArrangeOddEven(out var even2, out var odd2);
					HaarInverseInternal(even2, odd2);
					return;
				}
				span.ArrangeOddEven(out var even, out var odd);
				HaarInverseMultiLevelInternal(even);
				HaarInverseInternal(even, odd);
			}
		}

		/// <summary>
		/// Performs The Multi-Level Inverse Haar Transform to the specified elements.
		/// </summary>
		/// <param name="span">The Transforming span.</param>
		[DebuggerNonUserCode()]
		public static void HaarInverseMultiLevel(Span<System.Double> span)
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
		private static void HaarInverseInternal(in Span<System.Int16> even, in Span<System.Int16> odd)
		{
			unchecked
			{
				for (int i = 0; i < even.Length; i++)
				{
					//-Update
					even[i] -= (System.Int16)(odd[i] >> 1);
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
		private static void HaarInternal(in Span<System.Int16> even, in Span<System.Int16> odd)
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
					even[i] += (System.Int16)(odd[i] >> 1);
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
		private static void HaarInverseInternal(in Span<System.SByte> even, in Span<System.SByte> odd)
		{
			unchecked
			{
				for (int i = 0; i < even.Length; i++)
				{
					//-Update
					even[i] -= (System.SByte)(odd[i] >> 1);
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
		private static void HaarInternal(in Span<System.SByte> even, in Span<System.SByte> odd)
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
					even[i] += (System.SByte)(odd[i] >> 1);
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
		private static void HaarInverseInternal(in Span<System.Int32> even, in Span<System.Int32> odd)
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
		private static void HaarInternal(in Span<System.Int32> even, in Span<System.Int32> odd)
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
		private static void HaarInverseInternal(in Span<System.Single> even, in Span<System.Single> odd)
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
		private static void HaarInternal(in Span<System.Single> even, in Span<System.Single> odd)
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
		private static void HaarInverseInternal(in Span<System.Double> even, in Span<System.Double> odd)
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
		private static void HaarInternal(in Span<System.Double> even, in Span<System.Double> odd)
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
