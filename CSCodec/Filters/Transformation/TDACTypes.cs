using System;
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
		public static void PrepareInput(in Span<int> input, in Span<int> output)
		{
			if (output.Length != input.Length / 2) throw new ArgumentException("output must be 1/2 times as long as input.");
			int N = input.Length;
			int d4N = N / 4;        // N / 4
			input.SplitQuarter(out var A, out var B, out var C, out var D);
			output.SplitHalf(out var front, out var back);
			for (int n = 0; n < d4N; n++)
			{
				int nR = d4N - n - 1;
				front[n] = -C[nR] - D[n];
				back[n] = A[n] - B[nR];
			}
		}

		/// <summary>
		/// Processes the specified input for the Time Domain Aliasing Cancellation Calculation.
		/// </summary>
		/// <param name="input">The input buffer.</param>
		/// <param name="output">The output buffer. Must be 2 times as long as input.</param>
		/// <exception cref="ArgumentException">output must be at least 2 times as long as vs.</exception>
		public static void PostProcess(in Span<int> input, in Span<int> output)
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
		
		/// <summary>
		/// Prepares the specified input for the Time Domain Aliasing Cancellation Calculation.
		/// </summary>
		/// <param name="input">The input buffer.</param>
		/// <param name="output">The output buffer. Must be 1/2 times as long as input.</param>
		/// <exception cref="ArgumentException">output must be 1/2 times as long as input.</exception>
		public static void PrepareInput(in Span<float> input, in Span<float> output)
		{
			if (output.Length != input.Length / 2) throw new ArgumentException("output must be 1/2 times as long as input.");
			int N = input.Length;
			int d4N = N / 4;        // N / 4
			input.SplitQuarter(out var A, out var B, out var C, out var D);
			output.SplitHalf(out var front, out var back);
			for (int n = 0; n < d4N; n++)
			{
				int nR = d4N - n - 1;
				front[n] = -C[nR] - D[n];
				back[n] = A[n] - B[nR];
			}
		}

		/// <summary>
		/// Processes the specified input for the Time Domain Aliasing Cancellation Calculation.
		/// </summary>
		/// <param name="input">The input buffer.</param>
		/// <param name="output">The output buffer. Must be 2 times as long as input.</param>
		/// <exception cref="ArgumentException">output must be at least 2 times as long as vs.</exception>
		public static void PostProcess(in Span<float> input, in Span<float> output)
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
				
		/// <summary>
		/// Prepares the specified input for the Time Domain Aliasing Cancellation Calculation.
		/// </summary>
		/// <param name="input">The input buffer.</param>
		/// <param name="output">The output buffer. Must be 1/2 times as long as input.</param>
		/// <exception cref="ArgumentException">output must be 1/2 times as long as input.</exception>
		public static void PrepareInput(in Span<Int24> input, in Span<Int24> output)
		{
			if (output.Length != input.Length / 2) throw new ArgumentException("output must be 1/2 times as long as input.");
			int N = input.Length;
			int t3d4N = 3 * N / 4;  // N * 3 / 4
			int d2N = N / 2;        // N / 2
			int d4N = N / 4;        // N / 4
			for (int n = 0; n < d4N; n++)
			{
				int CRn = t3d4N - n - 1;
				int Dn = t3d4N + n;
				output[n] = (Int24)(-input[CRn] - input[Dn]);
				int An = n;
				int BRn = d2N - n - 1;
				output[n + d4N] = (Int24)(input[An] - input[BRn]);
			}
		}

		/// <summary>
		/// Processes the specified input for the Time Domain Aliasing Cancellation Calculation.
		/// </summary>
		/// <param name="input">The input buffer.</param>
		/// <param name="output">The output buffer. Must be 2 times as long as input.</param>
		/// <exception cref="ArgumentException">output must be at least 2 times as long as vs.</exception>
		public static void PostProcess(in Span<Int24> input, in Span<Int24> output)
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
				BsAR[n] = (Int24)(-back[nR]);
				CaDR[n] = (Int24)(-front[nR]);
				DaCR[n] = (Int24)(-front[n]);
			}
		}
		
		/// <summary>
		/// Prepares the specified input for the Time Domain Aliasing Cancellation Calculation.
		/// </summary>
		/// <param name="input">The input buffer.</param>
		/// <param name="output">The output buffer. Must be 1/2 times as long as input.</param>
		/// <exception cref="ArgumentException">output must be 1/2 times as long as input.</exception>
		public static void PrepareInput(in Span<short> input, in Span<short> output)
		{
			if (output.Length != input.Length / 2) throw new ArgumentException("output must be 1/2 times as long as input.");
			int N = input.Length;
			int t3d4N = 3 * N / 4;  // N * 3 / 4
			int d2N = N / 2;        // N / 2
			int d4N = N / 4;        // N / 4
			for (int n = 0; n < d4N; n++)
			{
				int CRn = t3d4N - n - 1;
				int Dn = t3d4N + n;
				output[n] = (short)(-input[CRn] - input[Dn]);
				int An = n;
				int BRn = d2N - n - 1;
				output[n + d4N] = (short)(input[An] - input[BRn]);
			}
		}

		/// <summary>
		/// Processes the specified input for the Time Domain Aliasing Cancellation Calculation.
		/// </summary>
		/// <param name="input">The input buffer.</param>
		/// <param name="output">The output buffer. Must be 2 times as long as input.</param>
		/// <exception cref="ArgumentException">output must be at least 2 times as long as vs.</exception>
		public static void PostProcess(in Span<short> input, in Span<short> output)
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
				BsAR[n] = (short)(-back[nR]);
				CaDR[n] = (short)(-front[nR]);
				DaCR[n] = (short)(-front[n]);
			}
		}
		
		/// <summary>
		/// Prepares the specified input for the Time Domain Aliasing Cancellation Calculation.
		/// </summary>
		/// <param name="input">The input buffer.</param>
		/// <param name="output">The output buffer. Must be 1/2 times as long as input.</param>
		/// <exception cref="ArgumentException">output must be 1/2 times as long as input.</exception>
		public static void PrepareInput(in Span<sbyte> input, in Span<sbyte> output)
		{
			if (output.Length != input.Length / 2) throw new ArgumentException("output must be 1/2 times as long as input.");
			int N = input.Length;
			int t3d4N = 3 * N / 4;  // N * 3 / 4
			int d2N = N / 2;        // N / 2
			int d4N = N / 4;        // N / 4
			for (int n = 0; n < d4N; n++)
			{
				int CRn = t3d4N - n - 1;
				int Dn = t3d4N + n;
				output[n] = (sbyte)(-input[CRn] - input[Dn]);
				int An = n;
				int BRn = d2N - n - 1;
				output[n + d4N] = (sbyte)(input[An] - input[BRn]);
			}
		}

		/// <summary>
		/// Processes the specified input for the Time Domain Aliasing Cancellation Calculation.
		/// </summary>
		/// <param name="input">The input buffer.</param>
		/// <param name="output">The output buffer. Must be 2 times as long as input.</param>
		/// <exception cref="ArgumentException">output must be at least 2 times as long as vs.</exception>
		public static void PostProcess(in Span<sbyte> input, in Span<sbyte> output)
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
				BsAR[n] = (sbyte)(-back[nR]);
				CaDR[n] = (sbyte)(-front[nR]);
				DaCR[n] = (sbyte)(-front[n]);
			}
		}
		
	}
}
