﻿using System;
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
        private const double Tau = 2 * Math.PI;

        private const double Sqrt2 = 1.41421356237309504880168872420969807856967187537694807317667973799073247846210703885038753432764157273501384623091229702492483605585073721264412149709993583141322266592750559275579995050115278206057147010955997160597027453459686;

        /// <summary>
        /// Contains (1 &lt;&lt; n) root of unity.
        /// </summary>
        private static readonly ReadOnlyMemory<Complex> PowerRootsOfUnity = new Complex[]
        {
            /*     1 */new Complex(1, 0),
            /*     2 */new Complex(-1, 0),
            /*     4 */new Complex(0, 1),
            /*     8 */new Complex(0.70710678118654757, 0.70710678118654757),
            /*    16 */new Complex(0.92387953251128674, 0.38268343236508978),
            /*    32 */new Complex(0.98078528040323043, 0.19509032201612828),
            /*    64 */new Complex(0.99518472667219693, 0.0980171403295606),
            /*   128 */new Complex(0.99879545620517241, 0.049067674327418015),
            /*   256 */new Complex(0.99969881869620425, 0.024541228522912288),
            /*   512 */new Complex(0.9999247018391445, 0.012271538285719925),
            /*  1024 */new Complex(0.99998117528260111, 0.0061358846491544753),
            /*  2048 */new Complex(0.99999529380957619, 0.0030679567629659761),
            /*  4096 */new Complex(0.99999882345170188, 0.0015339801862847657),
            /*  8192 */new Complex(0.99999970586288223, 0.00076699031874270449),
            /* 16384 */new Complex(0.99999992646571789, 0.00038349518757139556),
            /* 32768 */new Complex(0.99999998161642933, 0.00019174759731070332),
            /* 65536 */new Complex(0.99999999540410733, 9.5873799095977345E-05),
            /*131072 */new Complex(0.99999999885102686, 4.7936899603066881E-05),
            /*262144 */new Complex(0.99999999971275666, 2.3968449808418219E-05),
            /*524288 */new Complex(0.99999999992818922, 1.1984224905069707E-05),
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Complex GetValueToMultiply(int pos)
            => pos >= PowerRootsOfUnity.Length ? Complex.FromPolarCoordinates(1, Tau * 1.0 / (1 << pos))
         : Unsafe.Add(ref MemoryMarshal.GetReference(PowerRootsOfUnity.Span), pos);

        /// <summary>
        /// Bit-Reversal
        /// </summary>
        /// <param name="span">The in/out span.</param>
        public static void Reverse<T>(Span<T> span)
        {
            if (!span.Length.IsPowerOfTwo()) throw new ArgumentException("The length of span must be a power of 2!", nameof(span));
            ReverseInternal(span);
        }

        private static void ReverseInternal<T>(Span<T> span)
        {
            int bits = MathB.CountBits((uint)span.Length);
            int shift = 32 - bits;
            for (int i = span.Length >> (bits >> 1); i < span.Length; i++)
            {
                int index = (int)MathB.ReverseBits((uint)i << shift);
                if (index >= i) continue;
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
            if (span.Length < 2) return;
            Perform2(span);
            if (span.Length < 4) return;
            switch (mode)
            {
                case FftMode.Forward:
                    Perform4Forward(span);
                    break;
                case FftMode.Backward:
                    Perform4Backward(span);
                    break;
            }
            int index = 3;
            for (int m = 8; m <= span.Length; m <<= 1)
            {
                Complex t, u;
                Complex omega = 1;
                int mHalf = m >> 1;
                var omegaM = GetValueToMultiply(index++);
                omegaM = mode == FftMode.Forward ? Complex.Conjugate(omegaM) : omegaM;
                Span<Complex> omegas = stackalloc Complex[mHalf];
                for (int i = 0; i < omegas.Length; i++)
                {
                    omegas[i] = omega;
                    omega *= omegaM;
                }
                for (int k = 0; k < span.Length; k += m)
                {
                    //Preset addressing reduces addition and boundary checks.
                    var spanA = span.Slice(k, omegas.Length);
                    var spanB = span.Slice(k + mHalf, spanA.Length);
                    for (int j = 0; j < omegas.Length; j++)
                    {
                        t = omegas[j] * spanB[j];
                        u = spanA[j];
                        spanA[j] = u + t;
                        spanB[j] = u - t;
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
            double thetaBase = mode == FftMode.Forward ? -Tau : Tau;
            if (span.Length < 2) return;
            Perform2(span);
            if (span.Length < 4) return;
            switch (mode)
            {
                case FftMode.Forward:
                    Perform4Forward(span);
                    break;
                case FftMode.Backward:
                    Perform4Backward(span);
                    break;
            }
            var index = 3;
            for (int m = 8; m <= span.Length; m <<= 1)
            {
                ComplexF t, u;
                Complex omega = 1;
                int mHalf = m >> 1;
                var omegaM = GetValueToMultiply(index++);
                omegaM = mode == FftMode.Forward ? Complex.Conjugate(omegaM) : omegaM;
                Span<ComplexF> omegas = stackalloc ComplexF[mHalf];
                for (int i = 0; i < omegas.Length; i++)
                {
                    omegas[i] = (ComplexF)omega;
                    omega *= omegaM;
                }
                for (int k = 0; k < span.Length; k += m)
                {
                    //Preset addressing reduces addition and boundary checks.
                    var spanA = span.Slice(k, omegas.Length);
                    var spanB = span.Slice(k + mHalf, spanA.Length);
                    for (int j = 0; j < omegas.Length; j++)
                    {
                        t = omegas[j] * spanB[j];
                        u = spanA[j];
                        spanA[j] = u + t;
                        spanB[j] = u - t;
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
