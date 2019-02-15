using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec.Binary.Checksum
{
	/// <summary>
	/// A Base Class for Implementations of Cyclic Redundancy Check
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class CrcBase<T> : IDisposable where T : unmanaged, IConvertible, IComparable<T>, IEquatable<T>
	{
		/// <summary>
		/// Gets or sets the polynomial for this CRC Instance.
		/// </summary>
		/// <value>
		/// The polynomial.
		/// </value>
		public T Polynomial { get; }

		/// <summary>
		/// Gets or sets the internal state.
		/// </summary>
		/// <value>
		/// The internal state.
		/// </value>
		protected T InternalState { get; set; }

		/// <summary>
		/// Gets the current output.
		/// </summary>
		/// <value>
		/// The current output.
		/// </value>
		public abstract T CurrentOutput { get; }

		/// <summary>
		/// Gets the initial state.
		/// </summary>
		/// <value>
		/// The initial state.
		/// </value>
		public T InitialState { get; }

		/// <summary>
		/// Gets or sets the value to xor output.
		/// </summary>
		/// <value>
		/// The xor output.
		/// </value>
		public T XorOutput { get; }

		/// <summary>
		/// Gets or sets a value indicating whether to reverse input.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance have to reverse input; otherwise, <c>false</c>.
		/// </value>
		public bool ReverseInput { get; }

		/// <summary>
		/// Gets or sets a value indicating whether to reverse output.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance have to reverse output; otherwise, <c>false</c>.
		/// </value>
		public bool ReverseOutput { get; }

		/// <summary>
		/// Appends the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		protected abstract void CalculateNext(byte value);

		/// <summary>
		/// Initializes the Crc Table.
		/// </summary>
		protected abstract void InitializeTable();

		/// <summary>
		/// Reverses the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		protected abstract T Reverse(T value);

		/// <summary>
		/// Appends the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void Append(byte value)
		{
			CalculateNext(value);
		}

		#region IDisposable Support

		private bool disposedValue = false; // 重複する呼び出しを検出するには

		/// <summary>
		/// Initializes a new instance of the <see cref="CrcBase{T}"/> class.
		/// </summary>
		/// <param name="polynomial">The polynomial.</param>
		/// <param name="initialState">Initial internal state.</param>
		/// <param name="xorOutput">The xor output.</param>
		/// <param name="reverseInput">if set to <c>true</c> this instance have to reverse input.</param>
		/// <param name="reverseOutput">if set to <c>true</c> this instance have to reverse output.</param>
		protected CrcBase(T polynomial, T initialState, T xorOutput, bool reverseInput, bool reverseOutput)
		{
			Polynomial = polynomial;
			InitialState = InternalState = initialState;
			XorOutput = xorOutput;
			ReverseInput = reverseInput;
			ReverseOutput = reverseOutput;
		}

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public void Initialize()
		{
			if (ReverseOutput) InternalState = Reverse(InternalState);
			InitializeTable();
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				disposedValue = true;
			}
		}

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
