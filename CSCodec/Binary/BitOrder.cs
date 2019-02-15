using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec.Binary
{
	/// <summary>
	/// Represents Bit Order
	/// </summary>
	public enum BitOrder : uint
	{
		/// <summary>
		/// Higher-bit first(MSB is head)
		/// </summary>
		HighFirst,

		/// <summary>
		/// Lower-bit first(LSB is head)
		/// </summary>
		LowFirst
	}
}
