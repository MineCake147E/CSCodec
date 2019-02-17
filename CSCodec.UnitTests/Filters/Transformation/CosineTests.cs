using CSCodec.Filters.Transformation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec.UnitTests.Filters.Transformation
{
	[TestFixture]
	public class CosineTests
	{
		[TestCase]
		public void DCTIVTestDouble()
		{
			Span<double> array = stackalloc double[2048];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Math.Sin(2.0 * Math.PI * i / array.Length);
			}
			Span<double> copy = stackalloc double[array.Length];
			array.CopyTo(copy);

			DiscreteCosineTransformation.PerformIV(array);
			DiscreteCosineTransformation.PerformInverseIV(array);
			for (int i = 0; i < array.Length; i++)
			{
				Assert.AreEqual(copy[i], array[i], -1.0 / int.MinValue);
			}
		}

		[TestCase]
		public void DCTIVTestFloat()
		{
			Span<float> array = stackalloc float[2048];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (float)Math.Sin(2.0 * Math.PI * i / array.Length);
			}
			Span<float> copy = stackalloc float[array.Length];
			array.CopyTo(copy);

			DiscreteCosineTransformation.PerformIV(array);
			DiscreteCosineTransformation.PerformInverseIV(array);
			for (int i = 0; i < array.Length; i++)
			{
				Assert.AreEqual(copy[i], array[i], -1.0 / short.MinValue);
			}
		}
	}
}
