using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec.Binary
{
	/// <summary>
	/// An attribute that explains how many bits the Enum spends for recording.
	/// BitReader and BitWriter assumes the specified Enum has the specified width.
	/// </summary>
	/// <seealso cref="Attribute" />
	[AttributeUsage(AttributeTargets.Enum, Inherited = false, AllowMultiple = false)]
	public sealed class EnumBitWidthAttribute : Attribute
	{
		/// <summary>
		/// Gets the Width of the Enum in bits.
		/// </summary>
		/// <value>
		/// The Width of the Enum in bits.
		/// </value>
		public int BitWidth { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="EnumBitWidthAttribute"/> class.
		/// </summary>
		/// <param name="BitWidth">Width of the Enum in bits.</param>
		public EnumBitWidthAttribute(int BitWidth)
		{
			if (BitWidth <= 0) throw new ArgumentOutOfRangeException(nameof(BitWidth), "BitWidth must be grater than 0!");
		}
	}
}
