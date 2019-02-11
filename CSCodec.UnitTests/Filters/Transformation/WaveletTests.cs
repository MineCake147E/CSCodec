using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using NUnit.Framework;
using CSCodec.Filters.Transformation;
using CSCodec.Debug;

namespace CSCodec.UnitTests.Filters.Transformation
{
	[TestFixture]
	public partial class WaveletTests
	{
		[TestCase]
		public void TestHaarReversiblityInt()
		{
			int[] array = new int[2048];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (int)(int.MaxValue * Math.Sin(2.0 * Math.PI * i / array.Length));
			}
			int[] copy = new int[array.Length];
			Array.Copy(array, copy, array.Length);

			Span<int> span = new Span<int>(array);

			WaveletTransformation.HaarMultiLevel(span);

			Span<int> transformed = stackalloc int[array.Length];
			span.CopyTo(transformed);

			WaveletTransformation.HaarInverseMultiLevel(span);
			AssertEqualityAndDumpInt(array, copy, transformed);
		}

		[TestCase]
		public void TestCDF53ReversibilityInt()
		{
			int[] array = new int[2048];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (int)(int.MaxValue * Math.Sin(2.0 * Math.PI * i / array.Length));
			}
			int[] copy = new int[array.Length];
			Array.Copy(array, copy, array.Length);
			Span<int> span = new Span<int>(array);

			WaveletTransformation.CDF53MultiLevel(span);

			Span<int> transformed = stackalloc int[array.Length];
			span.CopyTo(transformed);

			WaveletTransformation.CDF53InverseMultiLevel(span);

			AssertEqualityAndDumpInt(array, copy, transformed);
		}

		[TestCase]
		public void TestHaarReversiblityDouble()
		{
			double[] array = new double[2048];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Math.Sin(2.0 * Math.PI * i / array.Length);
			}
			double[] copy = new double[array.Length];
			Array.Copy(array, copy, array.Length);

			Span<double> span = new Span<double>(array);

			WaveletTransformation.HaarMultiLevel(span);
			Span<double> transformed = stackalloc double[array.Length];
			span.CopyTo(transformed);

			WaveletTransformation.HaarInverseMultiLevel(span);

			try
			{
				for (int i = 0; i < array.Length; i++)
				{
					Assert.AreEqual(copy[i], array[i], -1.0 / int.MinValue);
				}
				Console.WriteLine("Source,Transformed");
				for (int i = 0; i < array.Length; i++)
				{
					Console.WriteLine($"{copy[i]}, {transformed[i]}");
				}
			}
			catch (Exception)
			{
				Console.WriteLine("Expected,Actual");
				for (int i = 0; i < array.Length; i++)
				{
					Console.WriteLine($"{copy[i]}, {array[i]}");
				}
				throw;
			}
		}

		[TestCase]
		public void TestCDF53ReversiblityDouble()
		{
			double[] array = new double[2048];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Math.Sin(2.0 * Math.PI * i / array.Length);
			}
			double[] copy = new double[array.Length];
			Array.Copy(array, copy, array.Length);

			Span<double> span = new Span<double>(array);

			WaveletTransformation.CDF53MultiLevel(span);

			Span<double> transformed = stackalloc double[array.Length];
			span.CopyTo(transformed);

			WaveletTransformation.CDF53InverseMultiLevel(span);

			try
			{
				for (int i = 0; i < array.Length; i++)
				{
					Assert.AreEqual(copy[i], array[i], -1.0 / int.MinValue);
				}
				Console.WriteLine("Source,Transformed");
				for (int i = 0; i < array.Length; i++)
				{
					Console.WriteLine($"{copy[i]}, {transformed[i]}");
				}
			}
			catch (Exception)
			{
				Console.WriteLine("Expected,Actual");
				for (int i = 0; i < array.Length; i++)
				{
					Console.WriteLine($"{copy[i]}, {array[i]}");
				}
				throw;
			}
		}

		private static void AssertEqualityAndDumpInt<T>(T[] array, T[] copy, Span<T> transformed)
		{
			try
			{
				for (int i = 0; i < array.Length; i++)
				{
					Assert.AreEqual(copy[i], array[i]);
				}
				Console.WriteLine("Source,Transformed");
				for (int i = 0; i < array.Length; i++)
				{
					Console.WriteLine($"{copy[i]}, {transformed[i]}");
				}
			}
			catch (Exception)
			{
				Console.WriteLine("Expected,Actual");
				for (int i = 0; i < array.Length; i++)
				{
					Console.WriteLine($"{copy[i]}, {array[i]}");
				}
				throw;
			}
		}
	}
}
