using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using CSCodec.Core;

namespace CSCodec.Filters.Transformation
{
	/// <summary>
	/// Functions that performs Fast Fourier Transform.
	/// </summary>
	public static class FastFourierTransformation
	{
		/// <summary>
		/// Bit-Reversal
		/// </summary>
		/// <param name="span">The in/out span.</param>
		public static void Reverse<T>(Span<T> span)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			ReverseInternal(span);
		}

		private static void ReverseInternal<T>(Span<T> span)
		{
			int width = 33 - CodecMathHelper.CountBits((uint)span.Length);
			for (int i = 0; i < span.Length; i++)
			{
				int index = (int)CodecMathHelper.ReverseBits((uint)i << width);
				if (index < i) continue;
				var v = span[i];
				span[i] = span[index];
				span[index] = v;
			}
		}

		/// <summary>
		/// Performs forward transform to the specified span.
		/// </summary>
		/// <param name="span">The span.</param>
		/// <param name="mode">The FFT's Mode.</param>
		private static void Perform(Span<Complex> span, FftMode mode)
		{
			double thetaBase = 2 * Math.PI * (mode == FftMode.Forward ? -1 : 1);
			int index;
			Complex t, u, omega, omegaM;
			for (int m = 2; m <= span.Length; m <<= 1)
			{
				double theta = thetaBase / m;
				omegaM = Complex.FromPolarCoordinates(1, theta);
				for (int k = 0; k < span.Length; k += m)
				{
					omega = 1;
					int mHalf = m >> 1;
					for (int j = 0; j < mHalf; j++)
					{
						index = k + j;
						t = omega * span[index + mHalf];
						u = span[index];
						span[index] = u + t;
						span[index + mHalf] = u - t;
						omega *= omegaM;
					}
				}
			}
		}

		/// <summary>
		/// Transforms the specified span using Cooley-Tukey algorithm.
		/// </summary>
		/// <param name="span">The buffer.</param>
		/// <param name="mode">The FFT's Mode.</param>
		/// <exception cref="ArgumentException">The length of span must be power of 2! - span</exception>
		public static void FFT(Span<Complex> span, FftMode mode = FftMode.Forward)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			ReverseInternal(span);
			Perform(span, mode);
			if (mode == FftMode.Forward)
			{
				double scale = 1.0 / span.Length;
				for (int i = 0; i < span.Length; i++)
				{
					span[i] *= scale;
				}
			}
		}

		/// <summary>
		/// Transforms the specified buffer using Cooley-Tukey algorithm.
		/// </summary>
		/// <param name="buffer">The buffer.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="count">The count.</param>
		public static void FFT(Complex[] buffer, int offset, int count) => FFT(new Span<Complex>(buffer, offset, count));

		/// <summary>
		/// Performs forward transform to the specified span.
		/// </summary>
		/// <param name="span">The span.</param>
		/// <param name="mode">The FFT's Mode.</param>
		private static void Perform(Span<SingleComplex> span, FftMode mode)
		{
			double thetaBase = 2 * Math.PI * (mode == FftMode.Forward ? -1 : 1);
			int index;
			SingleComplex t, u, omega, omegaM;
			for (int m = 2; m <= span.Length; m <<= 1)
			{
				double theta = thetaBase / m;
				omegaM = SingleComplex.FromPolarCoordinates(1, theta);
				for (int k = 0; k < span.Length; k += m)
				{
					omega = 1;
					int mHalf = m >> 1;
					for (int j = 0; j < mHalf; j++)
					{
						index = k + j;
						t = omega * span[index + mHalf];
						u = span[index];
						span[index] = u + t;
						span[index + mHalf] = u - t;
						omega *= omegaM;
					}
				}
			}
		}

		/// <summary>
		/// Transforms the specified span using Cooley-Tukey algorithm.
		/// </summary>
		/// <param name="span">The buffer.</param>
		/// <param name="mode">The FFT's Mode.</param>
		/// <exception cref="ArgumentException">The length of span must be power of 2! - span</exception>
		public static void FFT(Span<SingleComplex> span, FftMode mode = FftMode.Forward)
		{
			if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			ReverseInternal(span);
			Perform(span, mode);
			if (mode == FftMode.Forward)
			{
				float scale = 1.0f / span.Length;
				for (int i = 0; i < span.Length; i++)
				{
					span[i] *= scale;
				}
			}
		}

		/// <summary>
		/// Transforms the specified buffer using Cooley-Tukey algorithm.
		/// </summary>
		/// <param name="buffer">The buffer.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="count">The count.</param>
		public static void FFT(SingleComplex[] buffer, int offset, int count) => FFT(new Span<SingleComplex>(buffer, offset, count));
	}
}
