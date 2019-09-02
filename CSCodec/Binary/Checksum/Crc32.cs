using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec.Binary.Checksum
{
    /// <summary>
    /// An Implementation of 32bit Cyclic Redundancy Check
    /// </summary>
    /// <seealso cref="Checksum.CrcBase{T}" />
    public sealed class Crc32 : CrcBase<uint>
	{
		private uint[] table;

		/// <summary>
		/// Gets the current output.
		/// </summary>
		/// <value>
		/// The current output.
		/// </value>
		public override uint CurrentOutput => InternalState ^ XorOutput;

		/// <summary>
		/// Initializes a new instance of the <see cref="Crc32"/> class.
		/// </summary>
		/// <param name="polynomial">The polynomial.</param>
		/// <param name="initialState">Initial internal state.</param>
		/// <param name="xorOutput">The xor output.</param>
		/// <param name="reverseInput">if set to <c>true</c> this instance have to reverse input.</param>
		/// <param name="reverseOutput">if set to <c>true</c> this instance have to reverse output.</param>
		public Crc32(uint polynomial, uint initialState, uint xorOutput, bool reverseInput, bool reverseOutput) : base(polynomial, initialState, xorOutput, reverseInput, reverseOutput)
		{
		}

		/// <summary>
		/// Initializes the Crc Table.
		/// </summary>
		protected override void InitializeTable()
		{
			table = new uint[256];
			for (ushort i = 0; i < table.Length; i++)
			{
				uint value = i;
				if (ReverseInput) value = value.ReverseBits();
				else value <<= 24;
				for (int j = 0; j < 8; j++)
				{
					uint mask = ((value & 0x80000000) == 0x80000000) ? Polynomial : 0u;
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
				var q = InternalState >> 24;
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
		protected override uint Reverse(uint value) => value.ReverseBits();
	}
}
