using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec.Binary.Checksum
{
	/// <summary>
	/// An Implementation of 8bit Cyclic Redundancy Check
	/// </summary>
	/// <seealso cref="Checksum.CrcBase{T}" />
	public sealed class Crc8 : CrcBase<byte>
	{
		/// <summary>
		/// The polynomial of CRC-8-CCITT x^8 + x^2 + x^1 + 1
		/// </summary>
		public const byte PolynomialCRC8CCITT = 0x07;

		private byte[] table;

		/// <summary>
		/// Gets the current output.
		/// </summary>
		/// <value>
		/// The current output.
		/// </value>
		public override byte CurrentOutput => unchecked((byte)(InitialState ^ XorOutput));

		/// <summary>
		/// Initializes a new instance of the <see cref="Crc8"/> class.
		/// </summary>
		/// <param name="polynomial">The polynomial.</param>
		/// <param name="initialState">Initial internal state.</param>
		/// <param name="xorOutput">The xor output.</param>
		/// <param name="reverseInput">if set to <c>true</c> this instance have to reverse input.</param>
		/// <param name="reverseOutput">if set to <c>true</c> this instance have to reverse output.</param>
		public Crc8(byte polynomial, byte initialState, byte xorOutput, bool reverseInput, bool reverseOutput) : base(polynomial, initialState, xorOutput, reverseInput, reverseOutput)
		{
		}

		/// <summary>
		/// Initializes the table using specified polynomial.
		/// </summary>
		protected override void InitializeTable()
		{
			table = new byte[256];
			for (byte i = 0; i < table.Length; i++)
			{
				byte value = i;
				if (ReverseInput) value = value.ReverseBits();
				for (int j = 0; j < 8; j++)
				{
					byte mask = ((value & 0x80) == 0x80) ? Polynomial : (byte)0;
					value <<= 1;
					value ^= mask;
				}
				if (ReverseInput) value = value.ReverseBits();
				table[i] = value;
			}
		}

		/// <summary>
		/// Appends the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		protected override void CalculateNext(byte value)
		{
			if (ReverseOutput)
			{
				var q = InternalState;
				InternalState >>= 8;
				InternalState ^= table[(q ^ value) & 0xff];
			}
			else
			{
				var q = InternalState;
				InternalState <<= 8;
				InternalState ^= table[(q ^ value) & 0xff];
			}
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			table = null;
		}

		/// <summary>
		/// Reverses the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		protected override byte Reverse(byte value) => value.ReverseBits();
	}
}
