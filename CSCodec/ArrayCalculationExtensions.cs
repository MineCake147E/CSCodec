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
	}
}
