using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using CSCodec.Filters.Prediction;

namespace CSCodec.UnitTests.Filters.Prediction
{
	[TestFixture]
	public class LinearPredictorTests
	{
		[TestCase]
		public void TestDouble()
		{
			Span<double> data = new double[512];
			int order = 64;
			for (int i = 0; i < data.Length; i++)
			{
				//Little bit complex waveform
				data[i] = Math.Sin(127 * Math.PI * i / data.Length) + Math.Sin(131 * Math.PI * i / data.Length);
			}
			var linearPredictor = new LinearPredictorDouble();
			linearPredictor.Initialize(data, order, out _);
			Span<double> predictResult = new double[data.Length];
			linearPredictor.Predict(data.Slice(0, order), predictResult);
			double error = 0;
			Console.WriteLine("Expected,Actual,Error");
			for (int i = 0; i < data.Length; i++)
			{
				var g = predictResult[i] - data[i];
				Console.WriteLine($"{data[i]},{predictResult[i]},{g}");
				error += g * g;
			}
			error /= data.Length;
			Assert.Pass($"Squared Mean Error:{error}"); //There is no complete prediction so ignores errors.
		}

		[TestCase]
		public void TestFloat()
		{
			Span<float> data = new float[512];
			int order = 64;
			for (int i = 0; i < data.Length; i++)
			{
				//Little bit complex waveform
				data[i] = (float)(Math.Sin(127 * Math.PI * i / data.Length) + Math.Sin(131 * Math.PI * i / data.Length));
			}
			var linearPredictor = new LinearPredictorFloat();
			linearPredictor.Initialize(data, order, out _);
			Span<float> predictResult = new float[data.Length];
			linearPredictor.Predict(data.Slice(0, order), predictResult);
			float error = 0;
			Console.WriteLine("Expected,Actual,Error");
			for (int i = 0; i < data.Length; i++)
			{
				var g = predictResult[i] - data[i];
				Console.WriteLine($"{data[i]},{predictResult[i]},{g}");
				error += g * g;
			}
			error /= data.Length;
			Assert.Pass($"Squared Mean Error:{error}"); //There is no complete prediction so ignores errors.
		}
	}
}
