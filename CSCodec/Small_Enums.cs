using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec
{
	public enum FunctionValueType : ushort
	{
		/// <summary>
		/// サンプル
		/// </summary>
		Raster = 1,
		/// <summary>
		/// ブロック
		/// </summary>
		Vector = 2,
		/// <summary>
		/// ビット
		/// </summary>
		RasterBit = ushort.MaxValue - 1,
		/// <summary>
		/// バイト列
		/// </summary>
		ByteArray = ushort.MaxValue
	}
}
