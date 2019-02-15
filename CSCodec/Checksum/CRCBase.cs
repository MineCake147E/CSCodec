using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec.Checksum
{
	/// <summary>
	/// A Base Class for Implementations of Cyclic Redundancy Check
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class CrcBase<T> where T : unmanaged, IConvertible, IComparable<T>, IEquatable<T>
	{
		/// <summary>
		/// Appends the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public abstract void Append(byte value);

		/// <summary>
		/// Aquires this instance's current CRC Sum.
		/// </summary>
		/// <returns></returns>
		public abstract T Aquire();
	}
}
