using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using NUnit.Framework;
using CSCodec.Filters.Transformation;
using CSCodec.Debug;

namespace CSCodec.UnitTests.Filters.Transformation
{

	public partial class WaveletTests
	{
				[TestCase]
		public void TestHaarReversiblityInt24()
		{
			Int24[] array = new Int24[2048];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (Int24)(Int24.MaxValue * Math.Sin(2.0 * Math.PI * i / array.Length));
			}
			Int24[] copy = new Int24[array.Length];
			Array.Copy(array, copy, array.Length);

			Span<Int24> span = new Span<Int24>(array);

			WaveletTransformation.HaarMultiLevel(span);

			Span<Int24> transformed = stackalloc Int24[array.Length];
			span.CopyTo(transformed);

			WaveletTransformation.HaarInverseMultiLevel(span);

			AssertEqualityAndDumpInt(array, copy, transformed);
		}

		[TestCase]
		public void TestCDF53ReversibilityInt24()
		{
			Int24[] array = new Int24[2048];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (Int24)(Int24.MaxValue * Math.Sin(2.0 * Math.PI * i / array.Length));
			}
			Int24[] copy = new Int24[array.Length];
			Array.Copy(array, copy, array.Length);
			Span<Int24> span = new Span<Int24>(array);

			WaveletTransformation.CDF53MultiLevel(span);

			Span<Int24> transformed = stackalloc Int24[array.Length];
			span.CopyTo(transformed);

			WaveletTransformation.CDF53InverseMultiLevel(span);

			AssertEqualityAndDumpInt(array, copy, transformed);
		}
				[TestCase]
		public void TestHaarReversiblityInt16()
		{
			Int16[] array = new Int16[2048];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (Int16)(Int16.MaxValue * Math.Sin(2.0 * Math.PI * i / array.Length));
			}
			Int16[] copy = new Int16[array.Length];
			Array.Copy(array, copy, array.Length);

			Span<Int16> span = new Span<Int16>(array);

			WaveletTransformation.HaarMultiLevel(span);

			Span<Int16> transformed = stackalloc Int16[array.Length];
			span.CopyTo(transformed);

			WaveletTransformation.HaarInverseMultiLevel(span);

			AssertEqualityAndDumpInt(array, copy, transformed);
		}

		[TestCase]
		public void TestCDF53ReversibilityInt16()
		{
			Int16[] array = new Int16[2048];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (Int16)(Int16.MaxValue * Math.Sin(2.0 * Math.PI * i / array.Length));
			}
			Int16[] copy = new Int16[array.Length];
			Array.Copy(array, copy, array.Length);
			Span<Int16> span = new Span<Int16>(array);

			WaveletTransformation.CDF53MultiLevel(span);

			Span<Int16> transformed = stackalloc Int16[array.Length];
			span.CopyTo(transformed);

			WaveletTransformation.CDF53InverseMultiLevel(span);

			AssertEqualityAndDumpInt(array, copy, transformed);
		}
				[TestCase]
		public void TestHaarReversiblitySByte()
		{
			SByte[] array = new SByte[2048];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (SByte)(SByte.MaxValue * Math.Sin(2.0 * Math.PI * i / array.Length));
			}
			SByte[] copy = new SByte[array.Length];
			Array.Copy(array, copy, array.Length);

			Span<SByte> span = new Span<SByte>(array);

			WaveletTransformation.HaarMultiLevel(span);

			Span<SByte> transformed = stackalloc SByte[array.Length];
			span.CopyTo(transformed);

			WaveletTransformation.HaarInverseMultiLevel(span);

			AssertEqualityAndDumpInt(array, copy, transformed);
		}

		[TestCase]
		public void TestCDF53ReversibilitySByte()
		{
			SByte[] array = new SByte[2048];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (SByte)(SByte.MaxValue * Math.Sin(2.0 * Math.PI * i / array.Length));
			}
			SByte[] copy = new SByte[array.Length];
			Array.Copy(array, copy, array.Length);
			Span<SByte> span = new Span<SByte>(array);

			WaveletTransformation.CDF53MultiLevel(span);

			Span<SByte> transformed = stackalloc SByte[array.Length];
			span.CopyTo(transformed);

			WaveletTransformation.CDF53InverseMultiLevel(span);

			AssertEqualityAndDumpInt(array, copy, transformed);
		}
				
		[TestCase]
		public void TestHaarReversiblitySingle()
		{
			Single[] array = new Single[2048];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (Single)Math.Sin(2.0 * Math.PI * i / array.Length);
			}
			Single[] copy = new Single[array.Length];
			Array.Copy(array, copy, array.Length);

			Span<Single> span = new Span<Single>(array);

			WaveletTransformation.HaarMultiLevel(span);
			Span<Single> transformed = stackalloc Single[array.Length];
			span.CopyTo(transformed);

			WaveletTransformation.HaarInverseMultiLevel(span);

			try
			{
				for (int i = 0; i < array.Length; i++)
				{
					Assert.AreEqual(copy[i], array[i], -1.0 / short.MinValue);
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
		public void TestCDF53ReversiblitySingle()
		{
			Single[] array = new Single[2048];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (Single)Math.Sin(2.0 * Math.PI * i / array.Length);
			}
			Single[] copy = new Single[array.Length];
			Array.Copy(array, copy, array.Length);

			Span<Single> span = new Span<Single>(array);

			WaveletTransformation.CDF53MultiLevel(span);

			Span<Single> transformed = stackalloc Single[array.Length];
			span.CopyTo(transformed);

			WaveletTransformation.CDF53InverseMultiLevel(span);

			try
			{
				for (int i = 0; i < array.Length; i++)
				{
					Assert.AreEqual(copy[i], array[i], -1.0 / short.MinValue);
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
