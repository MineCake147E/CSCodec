using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec.Filters.Prediction
{
	/// <summary>
	/// Base Definition of Linear Prediction.
	/// </summary>
	/// <typeparam name="T">Type to predict.</typeparam>
	public abstract class LinearPredictorBase<T> : IDisposable, IPredictor<T> where T : struct, IEquatable<T>, IComparable<T>
	{
		/// <summary>
		/// The value which indicates whether this instance is disposed or not.
		/// </summary>
		protected bool disposed = false;

		/// <summary>
		/// Gets the coefficients.
		/// </summary>
		/// <value>
		/// The coefficients.
		/// </value>
		public T[] Coefficients { get; protected set; }

		/// <summary>
		/// Gets the order of this instance.
		/// </summary>
		/// <value>
		/// The order.
		/// </value>
		public int Order => Coefficients.Length;

		/// <summary>
		/// Initializes the Linear Predictor using specified data.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="Order">The order of predictor.</param>
		/// <param name="errors">The errors.</param>
		public abstract void Initialize(in ReadOnlySpan<T> data, int Order, out Span<double> errors);

		/// <summary>
		/// Resets this instance using specified coefficents.
		/// </summary>
		/// <param name="coefficents">The coefficents to be set.</param>
		public abstract void Reset(in ReadOnlySpan<T> coefficents);

		/// <summary>
		/// Resets this instance using specified global coefficient.
		/// </summary>
		/// <param name="fill">The coefficents to be set.</param>
		public abstract void Reset(in T fill);

		/// <summary>
		/// Predicts outputs from the specified first data.
		/// </summary>
		/// <param name="first">The first data.</param>
		/// <param name="output">The prediction output without overlapping <paramref name="first"/>. <paramref name="first"/> will be copied first.</param>
		public abstract void Predict(in ReadOnlySpan<T> first, Span<T> output);

		#region IDisposable Support

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected abstract void Dispose(bool disposing);

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion IDisposable Support
	}
}
