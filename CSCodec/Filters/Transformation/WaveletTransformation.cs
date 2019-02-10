using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CSCodec.Filters.Transformation
{
	/// <summary>
	/// Performs Wavelet Transforms.
	/// </summary>
	public static partial class WaveletTransformation
	{
		[Conditional("DUMMY")]
		internal static void Dummy()
		{
		}

#pragma warning disable S1144 // Unused private types or members should be removed

		/// <summary>
		/// Performs The 1-level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even.</param>
		/// <param name="odd">The odd.</param>
		private static void CDF53Internal(in Span<int> even, in Span<int> odd)
		{
			unchecked
			{
				for (int i = 0; i < odd.Length - 1; i++)
				{
					//-Predict
					odd[i] -= (int)(((long)even[i] + even[i + 1]) >> 1);
				}
				odd[odd.Length - 1] -= even[even.Length - 1];
				even[0] += (int)((odd[0] + 1L) >> 1);
				for (int i = 1; i < even.Length; i++)
				{
					//+Update
					even[i] += (int)((2L + odd[i - 1] + odd[i]) >> 2);
				}
			}
		}

		/// <summary>
		/// Performs The 1-level Cohen–Daubechies–Feauveau LeGall 5/3 Wavelet Transform to the specified elements.
		/// </summary>
		/// <param name="even">The even.</param>
		/// <param name="odd">The odd.</param>
		private static void CDF53InverseInternal(in Span<int> even, in Span<int> odd)
		{
			unchecked
			{
				for (int i = 0; i < odd.Length - 1; i++)
				{
					//-Update
					odd[i] -= (int)((2L + even[i - 1] + even[i]) >> 2);
				}
				odd[odd.Length - 1] -= (int)((even[0] + 1L) >> 1);
				even[0] += odd[even.Length - 1];
				for (int i = 1; i < even.Length; i++)
				{
					//+Predict
					even[i] += (int)(((long)odd[i] + odd[i + 1]) >> 1);
				}
			}
		}

#pragma warning restore S1144 // Unused private types or members should be removed
	}
}
