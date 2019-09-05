using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace CSCodec
{
    /// <summary>
    /// Hardware-Accelerated (or not) array manipulation.
    /// </summary>
    public static partial class MathV
    {
        /// <summary>
        /// Gets a value indicating whether SIMD is supported.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is SIMD supported; otherwise, <c>false</c>.
        /// </value>
        public static bool IsSIMDSupported => Vector.IsHardwareAccelerated;

        static MathV()
        {
        }

        /// <summary>
        /// Copies the real.
        /// </summary>
        /// <param name="complices">The complices.</param>
        /// <param name="destination">The destination.</param>
        /// <exception cref="ArgumentException">complices' length and destination's must be same!!!</exception>
        public static void CopyReal(this Span<Complex> complices, ref Span<double> destination)
        {
            if (complices.Length != destination.Length) throw new ArgumentException("complices' length and destination's must be same!!!");
            for (int i = 0; i < complices.Length; i++)
            {
                destination[i] = complices[i].Real;
            }
        }

        /// <summary>
        /// Scales <paramref name="span"/> with <paramref name="scale"/>.
        /// </summary>
        /// <param name="span">The array to scale.</param>
        /// <param name="scale">The scale.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode()]
        public static void ScaleArray(this Span<int> span, int scale)
        {
            if (Vector<int>.Count > span.Length)
            {
                for (int i = 0; i < span.Length; i++)
                {
                    span[i] *= scale;
                }
            }
            else
            {
                var spanV = MemoryMarshal.Cast<int, Vector<int>>(span);
                var scaleV = new Vector<int>(scale);
                for (int i = 0; i < spanV.Length; i++)
                {
                    spanV[i] *= scaleV;
                }
                var spanR = span.Slice(spanV.Length * Vector<int>.Count);
                for (int i = 0; i < spanR.Length; i++)
                {
                    spanR[i] *= scale;
                }
            }
        }

        /// <summary>
        /// Scales <paramref name="span"/> with <paramref name="scale"/>.
        /// </summary>
        /// <param name="span">The array to scale.</param>
        /// <param name="scale">The scale.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode()]
        public static void ScaleArray(this Span<short> span, short scale)
        {
            if (Vector<short>.Count > span.Length)
            {
                for (int i = 0; i < span.Length; i++)
                {
                    span[i] *= scale;
                }
            }
            else
            {
                var spanV = MemoryMarshal.Cast<short, Vector<short>>(span);
                var scaleV = new Vector<short>(scale);
                for (int i = 0; i < spanV.Length; i++)
                {
                    spanV[i] *= scaleV;
                }
                var spanR = span.Slice(spanV.Length * Vector<short>.Count);
                for (int i = 0; i < spanR.Length; i++)
                {
                    spanR[i] *= scale;
                }
            }
        }

        /// <summary>
        /// Scales <paramref name="span"/> with <paramref name="scale"/>.
        /// </summary>
        /// <param name="span">The array to scale.</param>
        /// <param name="scale">The scale.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode()]
        public static void ScaleArray(this Span<float> span, float scale)
        {
            if (Vector<float>.Count > span.Length)
            {
                for (int i = 0; i < span.Length; i++)
                {
                    span[i] *= scale;
                }
            }
            else
            {
                var spanV = MemoryMarshal.Cast<float, Vector<float>>(span);
                var scaleV = new Vector<float>(scale);
                for (int i = 0; i < spanV.Length; i++)
                {
                    spanV[i] *= scaleV;
                }
                var spanR = span.Slice(spanV.Length * Vector<float>.Count);
                for (int i = 0; i < spanR.Length; i++)
                {
                    spanR[i] *= scale;
                }
            }
        }

        /// <summary>
        /// Scales <paramref name="span"/> with <paramref name="scale"/>.
        /// </summary>
        /// <param name="span">The array to scale.</param>
        /// <param name="scale">The scale.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode()]
        public static void ScaleArray(this Span<double> span, double scale)
        {
            if (Vector<double>.Count > span.Length)
            {
                for (int i = 0; i < span.Length; i++)
                {
                    span[i] *= scale;
                }
            }
            else
            {
                var spanV = MemoryMarshal.Cast<double, Vector<double>>(span);
                var scaleV = new Vector<double>(scale);
                for (int i = 0; i < spanV.Length; i++)
                {
                    spanV[i] *= scaleV;
                }
                var spanR = span.Slice(spanV.Length * Vector<double>.Count);
                for (int i = 0; i < spanR.Length; i++)
                {
                    spanR[i] *= scale;
                }
            }
        }
    }
}
