using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace CSCodec.Filters.Transformation
{
    /// <summary>
    /// Implements Discrete Cosine Transformation(DCT).
    /// </summary>
    public static class DiscreteCosineTransformation
	{
		/// <summary>
		/// Calculates Discrete Cosine Transform Type-IV for real input <paramref name="span"/>.
		/// </summary>
		/// <param name="span">The specified input.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void PerformIV(Span<double> span)
		{
			int N = span.Length;
			if (!((uint)N).IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			Span<Complex> fftInArray = stackalloc Complex[8 * N];
			fftInArray.Fill(Complex.Zero);
			for (int n = 0; n < N; n++)
			{
				fftInArray[2 * n + 1] = fftInArray[8 * N - 2 * n - 1] = new Complex(span[n], 0);
				fftInArray[4 * N + 2 * n + 1] = fftInArray[4 * N - 2 * n - 1] = new Complex(-span[n], 0);
			}
			FastFourierTransformation.FFT(fftInArray, FftMode.Forward);
			for (int n = 0; n < span.Length; n++)
			{
				span[n] = fftInArray[2 * n + 1].Real;
			}
			//fftInArray will be released here
		}

		/// <summary>
		/// Calculates Inverse Discrete Cosine Transform Type-IV for real input <paramref name="span"/>.
		/// </summary>
		/// <param name="span">The specified input.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void PerformInverseIV(Span<double> span)
		{
			int N = span.Length;
			if (!((uint)N).IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			Span<Complex> fftInArray = stackalloc Complex[8 * N];
			fftInArray.Fill(Complex.Zero);
			for (int n = 0; n < N; n++)
			{
				fftInArray[2 * n + 1] = fftInArray[8 * N - 2 * n - 1] = new Complex(span[n], 0);
				fftInArray[4 * N + 2 * n + 1] = fftInArray[4 * N - 2 * n - 1] = new Complex(-span[n], 0);
			}
			FastFourierTransformation.FFT(fftInArray, FftMode.Forward);
			double ratio = N * 8;
			for (int n = 0; n < span.Length; n++)
			{
				span[n] = ratio * fftInArray[2 * n + 1].Real;
			}
			//fftInArray will be released here
		}

		/// <summary>
		/// Calculates Discrete Cosine Transform Type-IV for real input <paramref name="span"/>.
		/// </summary>
		/// <param name="span">The specified input.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void PerformIV(Span<float> span)
		{
			int N = span.Length;
			if (!((uint)N).IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			Span<ComplexF> fftInArray = stackalloc ComplexF[8 * N];
			fftInArray.Fill(ComplexF.Zero);
			for (int n = 0; n < N; n++)
			{
				fftInArray[2 * n + 1] = fftInArray[8 * N - 2 * n - 1] = new ComplexF(span[n], 0);
				fftInArray[4 * N + 2 * n + 1] = fftInArray[4 * N - 2 * n - 1] = new ComplexF(-span[n], 0);
			}
			FastFourierTransformation.FFT(fftInArray, FftMode.Forward);
			for (int n = 0; n < span.Length; n++)
			{
				span[n] = fftInArray[2 * n + 1].Real;
			}
			//fftInArray will be released here
		}

		/// <summary>
		/// Calculates Inverse Discrete Cosine Transform Type-IV for real input <paramref name="span"/>.
		/// </summary>
		/// <param name="span">The specified input.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void PerformInverseIV(Span<float> span)
		{
			int N = span.Length;
			if (!((uint)N).IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
			Span<ComplexF> fftInArray = stackalloc ComplexF[8 * N];
			fftInArray.Fill(ComplexF.Zero);
			for (int n = 0; n < N; n++)
			{
				fftInArray[2 * n + 1] = fftInArray[8 * N - 2 * n - 1] = new ComplexF(span[n], 0);
				fftInArray[4 * N + 2 * n + 1] = fftInArray[4 * N - 2 * n - 1] = new ComplexF(-span[n], 0);
			}
			FastFourierTransformation.FFT(fftInArray, FftMode.Forward);
			float ratio = N * 8;
			for (int n = 0; n < span.Length; n++)
			{
				span[n] = ratio * fftInArray[2 * n + 1].Real;
			}
			//fftInArray will be released here?
		}
	}
}
