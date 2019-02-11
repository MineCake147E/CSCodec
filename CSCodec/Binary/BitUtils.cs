using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec.Binary
{
	/// <summary>
	///
	/// </summary>
	/// TODO Edit XML Comment Template for BitUtils
	public static class BitUtils
	{
		/// <summary>
		/// Reads the specified buffer.
		/// </summary>
		/// <param name="buffer">The buffer.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException">buffer should be longer than 4 bytes. - buffer</exception>
		public static uint Read(Span<byte> buffer)
		{
			if (buffer.Length < sizeof(uint)) throw new ArgumentException("buffer should be longer than 4 bytes.", nameof(buffer));
			return ((uint)buffer[3] << 24) | ((uint)buffer[2] << 16) | ((uint)buffer[1] << 8) | buffer[0];
		}
	}
}
