using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace CSCodec
{
	/// <summary>
	/// Hardware-Accelerated array manipulation.
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
	}
}
