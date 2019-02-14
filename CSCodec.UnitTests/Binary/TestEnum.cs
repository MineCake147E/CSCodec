using CSCodec.Binary;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec.UnitTests.Binary
{
#pragma warning disable S2344 // Enumeration type names should not have "Flags" or "Enum" suffixes

	[EnumBitWidth(2)]
	public enum TestEnum
#pragma warning restore S2344 // Enumeration type names should not have "Flags" or "Enum" suffixes
	{
		Value0,
		Value1,
		Value2,
		Value3
	}
}
