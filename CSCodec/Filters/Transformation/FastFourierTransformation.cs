using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace CSCodec.Filters.Transformation
{
    /// <summary>
    /// Functions that performs Fast Fourier Transform.
    /// </summary>
    public static class FastFourierTransformation
    {
        private const double tau = 2 * Math.PI;

        private const double sqrt2 = 1.41421356237309504880168872420969807856967187537694807317667973799073247846210703885038753432764157273501384623091229702492483605585073721264412149709993583141322266592750559275579995050115278206057147010955997160597027453459686;

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
            int width = 32 - MathB.CountBits((uint)span.Length);
            for (int i = 0; i < span.Length; i++)
            {
                int index = (int)MathB.ReverseBits((uint)i << width);
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
            Complex omega, omegaM;
            if (span.Length >= 2)
            {
                Perform2(span);
                if (span.Length >= 4)
                {
                    switch (mode)
                    {
                        case FftMode.Forward:
                            Perform4Forward(span);
                            break;
                        case FftMode.Backward:
                            Perform4Backward(span);
                            break;
                    }
                    for (int m = 8; m <= span.Length; m <<= 1)
                    {
                        Complex t, u;
                        double theta = thetaBase / m;
                        omegaM = Complex.FromPolarCoordinates(1, theta);
                        for (int k = 0; k < span.Length; k += m)
                        {
                            omega = 1;
                            int mHalf = m >> 1;
                            //Preset addressing reduces addition and boundary checks.
                            var spanA = span.Slice(k, mHalf);
                            var spanB = span.Slice(k + mHalf, spanA.Length);
                            for (int j = 0; j < spanA.Length; j++)
                            {
                                t = omega * spanB[j];
                                u = spanA[j];
                                spanA[j] = u + t;
                                spanB[j] = u - t;
                                omega *= omegaM;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Performs forward transform to the specified span.
        /// </summary>
        /// <param name="span">The span.</param>
        /// <param name="mode">The FFT's Mode.</param>
        private static void Perform(Span<ComplexF> span, FftMode mode)
        {
            double thetaBase = mode == FftMode.Forward ? -tau : tau;
            ComplexF omega, omegaM;
            if (span.Length >= 2)
            {
                Perform2(span);
                if (span.Length >= 4)
                {
                    switch (mode)
                    {
                        case FftMode.Forward:
                            Perform4Forward(span);
                            break;
                        case FftMode.Backward:
                            Perform4Backward(span);
                            break;
                    }
                    for (int m = 8; m <= span.Length; m <<= 1)
                    {
                        ComplexF t, u;
                        double theta = thetaBase / m;
                        omegaM = ComplexF.FromPolarCoordinates(1, theta);
                        for (int k = 0; k < span.Length; k += m)
                        {
                            omega = 1;
                            int mHalf = m >> 1;
                            //Preset addressing reduces addition and boundary checks.
                            var spanA = span.Slice(k, mHalf);
                            var spanB = span.Slice(k + mHalf, spanA.Length);
                            for (int j = 0; j < spanA.Length; j++)
                            {
                                t = omega * spanB[j];
                                u = spanA[j];
                                spanA[j] = u + t;
                                spanB[j] = u - t;
                                omega *= omegaM;
                            }
                        }
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Perform4Backward(Span<Complex> span)
        {
            var sQ = MemoryMarshal.Cast<Complex, (Complex s0, Complex s1, Complex s2, Complex s3)>(span);
            for (int k = 0; k < sQ.Length; k++)
            {
                ref var sA = ref sQ[k];
                var v = new Complex(-sA.s3.Imaginary, sA.s3.Real);
                var t = sA.s2;
                var w = sA.s1;
                var u = sA.s0;
                sA.s0 = u + t;
                sA.s1 = w + v;
                sA.s2 = u - t;
                sA.s3 = w - v;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Perform4Backward(Span<ComplexF> span)
        {
            var sQ = MemoryMarshal.Cast<ComplexF, (ComplexF, ComplexF, ComplexF, ComplexF)>(span);
            for (int k = 0; k < sQ.Length; k++)
            {
                ref var sA = ref sQ[k];
                var v = new ComplexF(-sA.Item4.Imaginary, sA.Item4.Real);
                var t = sA.Item3;
                var w = sA.Item2;
                var u = sA.Item1;
                sA.Item1 = u + t;
                sA.Item2 = w + v;
                sA.Item3 = u - t;
                sA.Item4 = w - v;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Perform4Forward(Span<Complex> span)
        {
            var sQ = MemoryMarshal.Cast<Complex, (Complex, Complex, Complex, Complex)>(span);
            for (int k = 0; k < sQ.Length; k++)
            {
                ref var sA = ref sQ[k];
                var v = new Complex(sA.Item4.Imaginary, -sA.Item4.Real);
                var t = sA.Item3;
                var w = sA.Item2;
                var u = sA.Item1;
                sA.Item1 = u + t;
                sA.Item2 = w + v;
                sA.Item3 = u - t;
                sA.Item4 = w - v;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Perform4Forward(Span<ComplexF> span)
        {
            var sQ = MemoryMarshal.Cast<ComplexF, (ComplexF, ComplexF, ComplexF, ComplexF)>(span);
            for (int k = 0; k < sQ.Length; k++)
            {
                ref var sA = ref sQ[k];
                var v = new ComplexF(sA.Item4.Imaginary, -sA.Item4.Real);
                var t = sA.Item3;
                var w = sA.Item2;
                var u = sA.Item1;
                sA.Item1 = u + t;
                sA.Item2 = w + v;
                sA.Item3 = u - t;
                sA.Item4 = w - v;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Perform2(Span<Complex> span)
        {
            var sD = MemoryMarshal.Cast<Complex, (Complex, Complex)>(span);
            for (int k = 0; k < sD.Length; k++)
            {
                ref var sA = ref sD[k];
                var t = sA.Item2;
                var u = sA.Item1;
                sA.Item2 = u - t;
                sA.Item1 = u + t;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Perform2(Span<ComplexF> span)
        {
            var sD = MemoryMarshal.Cast<ComplexF, (ComplexF, ComplexF)>(span);
            for (int k = 0; k < sD.Length; k++)
            {
                ref var sA = ref sD[k];
                var t = sA.Item2;
                var u = sA.Item1;
                sA.Item1 = u + t;
                sA.Item2 = u - t;
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
                var ds = MemoryMarshal.Cast<Complex, double>(span);
                ds.ScaleArray(scale);
            }
        }

        /// <summary>
        /// Transforms the specified span using Cooley-Tukey algorithm.
        /// </summary>
        /// <param name="span">The buffer.</param>
        /// <param name="mode">The FFT's Mode.</param>
        /// <exception cref="ArgumentException">The length of span must be power of 2! - span</exception>
        public static void FFT(Span<ComplexF> span, FftMode mode = FftMode.Forward)
        {
            if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be power of 2!", nameof(span));
            ReverseInternal(span);
            Perform(span, mode);
            if (mode == FftMode.Forward)
            {
                float scale = 1.0f / span.Length;
                var ds = MemoryMarshal.Cast<ComplexF, float>(span);
                ds.ScaleArray(scale);
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
        /// Transforms the specified buffer using Cooley-Tukey algorithm.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count">The count.</param>
        public static void FFT(ComplexF[] buffer, int offset, int count) => FFT(new Span<ComplexF>(buffer, offset, count));
    }
}
