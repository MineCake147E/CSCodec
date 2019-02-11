using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec.Binary
{
	/// <summary>
	/// Represents ByteOrder
	/// </summary>
	public enum ByteOrder
	{
		/// <summary>
		/// Little Endian(0x01234567 => 0x67 0x45 0x23 0x01)
		/// </summary>
		LittleEndian,

		/// <summary>
		/// Big Endian(0x01234567 => 0x01 0x23 0x45 0x67)
		/// </summary>
		BigEndian
	}
}
