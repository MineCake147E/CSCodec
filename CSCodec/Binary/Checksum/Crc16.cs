using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec.Binary.Checksum
{
	/// <summary>
	/// An Implementation of 16bit Cyclic Redundancy Check
	/// </summary>
	/// <seealso cref="Checksum.CrcBase{T}" />
	public sealed class Crc16 : CrcBase<ushort>
	{
		/// <summary>
		/// The polynomial CRC-16 IBM x^16 + x^15 + x^2 + 1
		/// </summary>
		public const ushort PolynomialCRC16IBM = 0x8005;

		private ushort[] table;

		/// <summary>
		/// Gets the current output.
		/// </summary>
		/// <value>
		/// The current output.
		/// </value>
		public override ushort CurrentOutput => unchecked((ushort)(InternalState ^ XorOutput));

		/// <summary>
		/// Initializes a new instance of the <see cref="Crc16"/> class.
		/// </summary>
		/// <param name="polynomial">The polynomial.</param>
		/// <param name="initialState">Initial internal state.</param>
		/// <param name="xorOutput">The xor output.</param>
		/// <param name="reverseInput">if set to <c>true</c> this instance have to reverse input.</param>
		/// <param name="reverseOutput">if set to <c>true</c> this instance have to reverse output.</param>
		public Crc16(ushort polynomial, ushort initialState, ushort xorOutput, bool reverseInput, bool reverseOutput) : base(polynomial, initialState, xorOutput, reverseInput, reverseOutput)
		{
		}

		/// <summary>
		/// Initializes the Crc Table.
		/// </summary>
		protected override void InitializeTable()
		{
			table = new ushort[256];
			for (ushort i = 0; i < table.Length; i++)
			{
				ushort value = i;
				if (ReverseInput) value = value.ReverseBits();
				else value <<= 8;
				for (int j = 0; j < 8; j++)
				{
					ushort mask = ((value & 0x8000) == 0x8000) ? Polynomial : (ushort)0;
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
				var q = InternalState >> 8;
				InternalState <<= 8;
				InternalState ^= table[(q ^ value) & 0xff];
			}
		}

		#region IDisposable Support

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			table = null;
		}

		#endregion IDisposable Support

		/// <summary>
		/// Reverses the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		protected override ushort Reverse(ushort value) => value.ReverseBits();
	}
}
