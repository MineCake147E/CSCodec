using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec.Filters.Transformation
{
	/// <summary>
	/// Time Domain Aliasing Cancellation Helper class.
	/// In fact, not only DCT-IV, any reversible transform (e.g. wavelet transform) can be applied for TDAC.
	/// </summary>
	public static partial class TimeDomainAliasingCancellation
	{
		//Reference Implementation for double.

		/// <summary>
		/// Prepares the specified input for the Time Domain Aliasing Cancellation Calculation.
		/// </summary>
		/// <param name="input">The input buffer.</param>
		/// <param name="output">The output buffer. Must be 1/2 times as long as input.</param>
		/// <exception cref="ArgumentException">output must be 1/2 times as long as input.</exception>
		public static void PrepareInput(in Span<double> input, in Span<double> output)
		{
			if (output.Length != input.Length / 2) throw new ArgumentException("output must be 1/2 times as long as input.");
			int N = input.Length;
			for (int n = 0; n < N / 4; n++)
			{
				int CRn = 3 * N / 4 - n - 1;
				int Dn = 3 * N / 4 + n;
				output[n] = -input[CRn] - input[Dn];
				int An = n;
				int BRn = N / 2 - n - 1;
				output[n + N / 2] = input[An] - input[BRn];
			}
		}

		/// <summary>
		/// Processes the specified input for the Time Domain Aliasing Cancellation Calculation.
		/// </summary>
		/// <param name="input">The input buffer.</param>
		/// <param name="output">The output buffer. Must be 2 times as long as input.</param>
		/// <exception cref="ArgumentException">output must be at least 2 times as long as vs.</exception>
		public static void PostProcess(in Span<double> input, in Span<double> output)
		{
			int N = input.Length;
			if (output.Length != N * 2) throw new ArgumentException("output must be at least 2 times as long as vs.");
			for (int n = 0; n < N / 2; n++)
			{
				int AsBRn = N / 2 + n;
				int BsARn = N - n - 1;
				int CaDRn = N / 2 - n - 1;
				int DaCRn = n;
				output[n] = input[AsBRn];
				output[n + N / 2] = -input[BsARn];
				output[n + N] = -input[CaDRn];
				output[n + 3 * N / 2] = -input[DaCRn];
			}
		}
	}
}
