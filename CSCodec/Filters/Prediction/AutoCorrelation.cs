using CSCodec.Core;
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
        public static void CalculateAutocorrelation(ReadOnlySpan<double> data, out Span<double> acorr, int order, bool useFftIfPossible = true)
        {
            acorr = new double[order + 1];
            if (useFftIfPossible && data.Length.IsPowerOfTwo() && order + 1 > MathB.CountBits((uint)data.Length))
            {
                CalculateAutocorrelationUsingFFT(data, acorr);
            }
            else
            {
                CalculateAutocorrelationDirectly(data, acorr);
            }
        }

        private static void CalculateAutocorrelationDirectly(ReadOnlySpan<double> data, Span<double> acorr)
        {
            for (int delay = 0; delay < acorr.Length; delay++)  //acorr.Length = order + 1
            {
                acorr[delay] = 0;
                var delayedData = data.Slice(delay);
                for (int index = 0; index < delayedData.Length; index++)
                {
                    acorr[delay] += delayedData[index] * data[index];
                }
            }
        }

        private static void CalculateAutocorrelationUsingFFT(ReadOnlySpan<double> data, Span<double> acorr)
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

        /// <summary>
        /// Calculates the autocorrelation of specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="acorr">The resulting autocorrelation.</param>
        /// <param name="order">The order.</param>
        /// <param name="useFftIfPossible">if set to <c>true</c> it uses FFT if possible.</param>
        public static void CalculateAutocorrelation(ReadOnlySpan<float> data, out Span<float> acorr, int order, bool useFftIfPossible = true)
        {
            acorr = new float[order + 1];
            if (useFftIfPossible && data.Length.IsPowerOfTwo() && order + 1 > MathB.CountBits((uint)data.Length))
            {
                CalculateAutocorrelationUsingFFT(data, acorr);
            }
            else
            {
                CalculateAutocorrelationDirectly(data, acorr);
            }
        }

        private static void CalculateAutocorrelationDirectly(ReadOnlySpan<float> data, Span<float> acorr)
        {
            for (int delay = 0; delay < acorr.Length; delay++)  //acorr.Length = order + 1
            {
                acorr[delay] = 0;
                var delayedData = data.Slice(delay);
                for (int index = 0; index < delayedData.Length; index++)
                {
                    acorr[delay] += delayedData[index] * data[index];
                }
            }
        }

        private static void CalculateAutocorrelationUsingFFT(ReadOnlySpan<float> data, Span<float> acorr)
        {
            Span<SingleComplex> dataComplex = stackalloc SingleComplex[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                dataComplex[i] = data[i];   //convert float into double
            }
            FastFourierTransformation.FFT(dataComplex);
            for (int i = 0; i < dataComplex.Length; i++)
            {
                dataComplex[i] *= SingleComplex.Conjugate(dataComplex[i]);
            }
            FastFourierTransformation.FFT(dataComplex, FftMode.Backward);
            for (int i = 0; i < acorr.Length; i++)
            {
                acorr[i] = dataComplex[i].Real;
            }
        }
    }
}
