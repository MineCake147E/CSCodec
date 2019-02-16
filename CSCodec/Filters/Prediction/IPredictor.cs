using System;

namespace CSCodec.Filters.Prediction
{
	/// <summary>
	/// Basic Definitions for Predictors.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IPredictor<T> where T : struct, IEquatable<T>, IComparable<T>
	{
		/// <summary>
		/// Initializes the Predictor using specified data.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="Order">The order of predictor.</param>
		/// <param name="errors">The errors.</param>
		void Initialize(in ReadOnlySpan<T> data, int Order, out Span<double> errors);

		/// <summary>
		/// Predicts outputs from the specified first data.
		/// </summary>
		/// <param name="first">The first data.</param>
		/// <param name="output">The prediction output without overlapping <paramref name="first"/>. <paramref name="first"/> will be copied first.</param>
		void Predict(in ReadOnlySpan<T> first, Span<T> output);
	}
}
