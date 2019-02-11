using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace CSCodec
{
	/// <summary>
	/// Hardware-Accelerated (or not) array manipulation.
	/// </summary>
	public static partial class ArrayCalculationExtensions
	{
		/// <summary>
		/// Gets a value indicating whether SIMD is supported.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is SIMD supported; otherwise, <c>false</c>.
		/// </value>
		public static bool IsSIMDSupported => Vector.IsHardwareAccelerated;

		static ArrayCalculationExtensions()
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

		private static readonly int StrideInt = Vector<int>.Count;

		/// <summary>
		/// Scales <paramref name="buffer"/> with <paramref name="scale"/>.
		/// </summary>
		/// <param name="buffer">The array to scale.</param>
		/// <param name="scale">The scale.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void ScaleArray(this int[] buffer, int scale)
		{
			int lenNonSIMD = buffer.Length % StrideInt;
			int lenSIMD = buffer.Length - lenNonSIMD;
			int i;
			var vsc = new Vector<int>(scale);
			for (i = 0; i < lenSIMD; i += StrideInt)
			{
				var v = new Vector<int>(buffer, i);
				Vector.Multiply(vsc, v).CopyTo(buffer, i);
			}
			for (; i < buffer.Length; i++)
			{
				buffer[i] *= scale;
			}
		}

		private static readonly int StrideShort = Vector<short>.Count;

		/// <summary>
		/// Scales <paramref name="buffer"/> with <paramref name="scale"/>.
		/// </summary>
		/// <param name="buffer">The array to scale.</param>
		/// <param name="scale">The scale.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void ScaleArray(this short[] buffer, short scale)
		{
			int lenNonSIMD = buffer.Length % StrideShort;
			int lenSIMD = buffer.Length - lenNonSIMD;
			int i;
			var vsc = new Vector<short>(scale);
			for (i = 0; i < lenSIMD; i += StrideShort)
			{
				var v = new Vector<short>(buffer, i);
				Vector.Multiply(vsc, v).CopyTo(buffer, i);
			}
			for (; i < buffer.Length; i++)
			{
				buffer[i] *= scale;
			}
		}

		private static readonly int StrideSingle = Vector<float>.Count;

		/// <summary>
		/// Scales <paramref name="buffer"/> with <paramref name="scale"/>.
		/// </summary>
		/// <param name="buffer">The array to scale.</param>
		/// <param name="scale">The scale.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void ScaleArray(this float[] buffer, float scale)
		{
			int lenNonSIMD = buffer.Length % StrideSingle;
			int lenSIMD = buffer.Length - lenNonSIMD;
			int i;
			var vsc = new Vector<float>(scale);    //Assert.AreEqualによると、scaleで埋めてくれるらしい
			for (i = 0; i < lenSIMD; i += StrideSingle)
			{
				var v = new Vector<float>(buffer, i);
				Vector.Multiply(vsc, v).CopyTo(buffer, i);
			}
			for (; i < buffer.Length; i++)
			{
				buffer[i] *= scale;
			}
		}

		private static readonly int StrideDouble = Vector<double>.Count;

		/// <summary>
		/// Scales <paramref name="buffer"/> with <paramref name="scale"/>.
		/// </summary>
		/// <param name="buffer">The array to scale.</param>
		/// <param name="scale">The scale.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void ScaleArray(this double[] buffer, double scale)
		{
			int lenNonSIMD = buffer.Length % StrideDouble;
			int lenSIMD = buffer.Length - lenNonSIMD;
			int i;
			var vsc = new Vector<double>(scale);    //Assert.AreEqualによると、scaleで埋めてくれるらしい
			for (i = 0; i < lenSIMD; i += StrideDouble)
			{
				var v = new Vector<double>(buffer, i);
				Vector.Multiply(vsc, v).CopyTo(buffer, i);
			}
			for (; i < buffer.Length; i++)
			{
				buffer[i] *= scale;
			}
		}
	}
}
