using CSCodec.Filters.Transformation;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace CSCodec.Filters.Prediction
{
	/// <summary>
	///
	/// </summary>
	public static class Autocorrelation
	{
		/// <summary>
		/// Calculates the autocorrelation of specified data.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="acorr">The resulting autocorrelation.</param>
		/// <param name="order">The order.</param>
		/// <param name="useFftIfPossible">if set to <c>true</c> it uses FFT if possible.</param>
		public static void CalculateAutocorrelation(in ReadOnlySpan<double> data, out Span<double> acorr, int order, bool useFftIfPossible = true)
		{
			acorr = new double[order + 1];
			if (useFftIfPossible && data.Length.IsPowerOfTwo() && order + 1 > CodecMathHelper.CountBits((uint)data.Length))
			{
				CalculateAutoCorrelationUsingFFT(data, ref acorr);
			}
			else
			{
				CalculateAutoCorrelationDirectly(data, ref acorr);
			}
		}

		private static void CalculateAutoCorrelationDirectly(in ReadOnlySpan<double> data, ref Span<double> acorr)
		{
			for (int delay = 0; delay < acorr.Length; delay++)  //acorr.Length = order
			{
				acorr[delay] = 0;
				var delayedData = data.Slice(delay);
				for (int index = 0; index < delayedData.Length; index++)
				{
					acorr[delay] += delayedData[index] * data[index];
				}
			}
		}

		private static void CalculateAutoCorrelationUsingFFT(in ReadOnlySpan<double> data, ref Span<double> acorr)
		{
			Span<Complex> dataComplex = stackalloc Complex[data.Length];
			for (int i = 0; i < data.Length; i++)
			{
				dataComplex[i] = data[i];
			}
			FastFourierTransformation.FFT(dataComplex);
			for (int i = 0; i < dataComplex.Length; i++)
			{
				dataComplex[i] *= Complex.Conjugate(dataComplex[i]);
			}
			FastFourierTransformation.FFT(dataComplex, FftMode.Backward);
			for (int i = 0; i < acorr.Length; i++)
			{
				acorr[i] = dataComplex[i].Real;
			}
		}
	}
}
