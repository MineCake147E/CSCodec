using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using CSCodec.Filters.Transformation;

namespace CSCodec.UnitTests.Filters.Transformation
{
	[TestFixture]
	public class TdacTests
	{
		/// <summary>
		/// Tests the TDAC for double.
		/// </summary>
		/// <seealso cref="TimeDomainAliasingCancellation.PrepareInput(in Span{double}, in Span{double})"/>
		[TestCase]
		public void TdacTestDouble()
		{
			var rng = new Random();
			Span<double> TDACData = stackalloc double[2048];
			for (int i = 0; i < TDACData.Length; i++)
			{
				TDACData[i] = rng.NextDouble();
			}
			var span1 = TDACData.Slice(0, TDACData.Length / 2);
			var span2 = TDACData.Slice(TDACData.Length / 4, TDACData.Length / 2);
			var span3 = TDACData.Slice(TDACData.Length / 2, TDACData.Length / 2);
			Span<double> copy = new double[TDACData.Length];
			TDACData.CopyTo(copy);
			Span<double> after1 = stackalloc double[span1.Length / 2];
			Span<double> after2 = stackalloc double[span1.Length / 2];
			Span<double> after3 = stackalloc double[span1.Length / 2];
			TimeDomainAliasingCancellation.PrepareInput(span1, after1);
			TimeDomainAliasingCancellation.PrepareInput(span2, after2);
			TimeDomainAliasingCancellation.PrepareInput(span3, after3);
			TDACData.Fill(0);
			Span<double> backBuffer = stackalloc double[span1.Length];
			TimeDomainAliasingCancellation.PostProcess(after1, backBuffer);
			for (int i = 0; i < backBuffer.Length; i++)
			{
				span1[i] += 0.5 * backBuffer[i];
			}
			backBuffer.Fill(0);
			TimeDomainAliasingCancellation.PostProcess(after2, backBuffer);
			for (int i = 0; i < backBuffer.Length; i++)
			{
				span2[i] += 0.5 * backBuffer[i];
			}
			backBuffer.Fill(0);
			TimeDomainAliasingCancellation.PostProcess(after3, backBuffer);
			for (int i = 0; i < backBuffer.Length; i++)
			{
				span3[i] += 0.5 * backBuffer[i];
			}
			var copy2 = copy.Slice(after1.Length, span1.Length);
			for (int i = 0; i < span2.Length; i++)
			{
				Assert.AreEqual(copy2[i], span2[i], -1.0 / int.MinValue);
			}
		}

		/// <summary>
		/// Tests the TDAC for <c>float</c>.
		/// </summary>
		/// <seealso cref="TimeDomainAliasingCancellation.PrepareInput(in Span{float}, in Span{float})"/>
		[TestCase]
		public void TdacTestFloat()
		{
			var rng = new Random();
			Span<float> TDACData = stackalloc float[2048];
			for (int i = 0; i < TDACData.Length; i++)
			{
				TDACData[i] = (float)rng.NextDouble();
			}
			var span1 = TDACData.Slice(0, TDACData.Length / 2);
			var span2 = TDACData.Slice(TDACData.Length / 4, TDACData.Length / 2);
			var span3 = TDACData.Slice(TDACData.Length / 2, TDACData.Length / 2);
			Span<float> copy = new float[TDACData.Length];
			TDACData.CopyTo(copy);
			Span<float> after1 = stackalloc float[span1.Length / 2];
			Span<float> after2 = stackalloc float[span1.Length / 2];
			Span<float> after3 = stackalloc float[span1.Length / 2];
			TimeDomainAliasingCancellation.PrepareInput(span1, after1);
			TimeDomainAliasingCancellation.PrepareInput(span2, after2);
			TimeDomainAliasingCancellation.PrepareInput(span3, after3);
			TDACData.Fill(0);
			Span<float> backBuffer = stackalloc float[span1.Length];
			TimeDomainAliasingCancellation.PostProcess(after1, backBuffer);
			for (int i = 0; i < backBuffer.Length; i++)
			{
				span1[i] += 0.5f * backBuffer[i];
			}
			backBuffer.Fill(0);
			TimeDomainAliasingCancellation.PostProcess(after2, backBuffer);
			for (int i = 0; i < backBuffer.Length; i++)
			{
				span2[i] += 0.5f * backBuffer[i];
			}
			backBuffer.Fill(0);
			TimeDomainAliasingCancellation.PostProcess(after3, backBuffer);
			for (int i = 0; i < backBuffer.Length; i++)
			{
				span3[i] += 0.5f * backBuffer[i];
			}
			var copy2 = copy.Slice(after1.Length, span1.Length);
			for (int i = 0; i < span2.Length; i++)
			{
				Assert.AreEqual(copy2[i], span2[i], -1.0 / short.MinValue);
			}
		}
	}
}
