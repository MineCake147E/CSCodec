using System;
using System.Diagnostics;
using NUnit.Framework;
using Math = System.Math;

namespace CSCodec.UnitTests
{
	[TestFixture]
	public class CalculationTests
	{
		/// <summary>
		/// Tests the SIMD Support Scaling for <c>double</c>.
		/// This test measures both precision and performance(when Release).
		/// If SIMD-Aided Calculation is slower or more inaccurate than normal array operations, this test fails.
		/// </summary>
		[TestCase]
		public void TestSIMDScaleDouble()
		{
			double[] array = new double[2048];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Math.Sin(2.0 * Math.PI * i / array.Length);
			}
			double[] copy = new double[2048];
			Array.Copy(array, copy, array.Length);

			var sw = new Stopwatch();

			sw.Start();
			for (int j = 0; j < 131072; j++)
			{
				array.ScaleArray(0.5);
				array.ScaleArray(2.0);
			}
			sw.Stop();

			var timeSIMD = sw.Elapsed;
			Console.WriteLine(sw.Elapsed);
			for (int i = 0; i < array.Length; i++)
			{
				Assert.AreEqual(array[i], copy[i]);
			}
			sw.Reset();
			sw.Restart();
			for (int j = 0; j < 131072; j++)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] *= 0.5;
				}
				for (int i = 0; i < array.Length; i++)
				{
					array[i] *= 2;
				}
			}
			sw.Stop();
			Console.WriteLine(sw.Elapsed);
			Assert.LessOrEqual(timeSIMD.TotalMilliseconds, sw.Elapsed.TotalMilliseconds);
		}

		/// <summary>
		/// Tests the SIMD Support Scaling for <c>float</c>.
		/// This test measures both precision and performance(when Release).
		/// If SIMD-Aided Calculation is slower or more inaccurate than normal array operations, this test fails.
		/// </summary>
		[TestCase]
		public void TestSIMDScaleFloat()
		{
			float[] array = new float[2048];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (float)Math.Sin(2.0 * Math.PI * i / array.Length);
			}
			float[] copy = new float[2048];
			Array.Copy(array, copy, array.Length);

			var sw = new Stopwatch();

			sw.Start();
			for (int j = 0; j < 131072; j++)
			{
				array.ScaleArray(0.5f);
				array.ScaleArray(2.0f);
			}
			sw.Stop();

			var timeSIMD = sw.Elapsed;
			Console.WriteLine(sw.Elapsed);
			for (int i = 0; i < array.Length; i++)
			{
				Assert.AreEqual(array[i], copy[i]);
			}
			sw.Reset();
			sw.Restart();
			for (int j = 0; j < 131072; j++)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] *= 0.5f;
				}
				for (int i = 0; i < array.Length; i++)
				{
					array[i] *= 2;
				}
			}
			sw.Stop();
			Console.WriteLine(sw.Elapsed);
			Assert.LessOrEqual(timeSIMD.TotalMilliseconds, sw.Elapsed.TotalMilliseconds);
		}
	}
}
