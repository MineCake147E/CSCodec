using CSCodec.Filters.Transformation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace CSCodec.UnitTests.Filters.Transformation
{
	[TestFixture]
	public class FourierTests
	{
		[TestCase]
		public void FFTTest()
		{
			Complex[] array = new Complex[16];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Math.Sin(2.0 * Math.PI * i / array.Length);
			}
			Complex[] copy = new Complex[array.Length];
			Array.Copy(array, copy, array.Length);
			Span<Complex> span = new Span<Complex>(array);

			FastFourierTransformation.FFT(span);
			Span<Complex> transformed = stackalloc Complex[array.Length];
			span.CopyTo(transformed);
			FastFourierTransformation.FFT(span, FftMode.Backward);

			try
			{
				for (int i = 0; i < array.Length; i++)
				{
					Assert.AreEqual(copy[i].Real, array[i].Real, -1.0 / int.MinValue);
					Assert.AreEqual(copy[i].Imaginary, array[i].Imaginary, -1.0 / int.MinValue);
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
