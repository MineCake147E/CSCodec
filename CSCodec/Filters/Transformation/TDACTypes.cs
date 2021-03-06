﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace CSCodec.Filters.Transformation
{
	public static partial class TimeDomainAliasingCancellation
	{
		
		/// <summary>
		/// Prepares the specified input for the Time Domain Aliasing Cancellation Calculation.
		/// </summary>
		/// <param name="input">The input buffer.</param>
		/// <param name="output">The output buffer. Must be 1/2 times as long as input.</param>
		/// <exception cref="ArgumentException">output must be 1/2 times as long as input.</exception>
		public static void PrepareInput(Span<float> input, Span<float> output)
		{
			if (output.Length != input.Length / 2) throw new ArgumentException("output must be 1/2 times as long as input.");
			int N = input.Length;
			int d4N = N / 4;        // N / 4
			input.SplitQuarter(out var A, out var B, out var C, out var D);
			output.SplitHalf(out var front, out var back);
			for (int n = 0; n < d4N; n++)
			{
				int nR = d4N - n - 1;
				front[n] = -(C[nR] + D[n]);
				back[n] = A[n] - B[nR];
			}
		}

		/// <summary>
		/// Processes the specified input for the Time Domain Aliasing Cancellation Calculation.
		/// </summary>
		/// <param name="input">The input buffer.</param>
		/// <param name="output">The output buffer. Must be 2 times as long as input.</param>
		/// <exception cref="ArgumentException">output must be at least 2 times as long as vs.</exception>
		public static void PostProcess(Span<float> input, Span<float> output)
		{
			int N = input.Length;
			if (output.Length != N * 2) throw new ArgumentException("output must be at least 2 times as long as vs.");
			output.SplitQuarter(out var AsBR, out var BsAR, out var CaDR, out var DaCR);
			input.SplitHalf(out var front, out var back);
			int d2N = N / 2;
			for (int n = 0; n < d2N; n++)
			{
				int nR = d2N - n - 1;
				AsBR[n] = back[n];
				BsAR[n] = -back[nR];
				CaDR[n] = -front[nR];
				DaCR[n] = -front[n];
			}
		}
		
	}
}
