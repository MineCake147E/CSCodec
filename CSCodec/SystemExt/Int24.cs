using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	/// <summary>
	/// An simple representation of 24bit signed integer.
	/// </summary>
	/// <seealso cref="System.IEquatable{T}" />
	[StructLayout(LayoutKind.Explicit)]
	public readonly struct Int24 : IEquatable<Int24>, IComparable<Int24>
	{
		[FieldOffset(0)]
		private readonly byte tail;

		[FieldOffset(1)]
		private readonly byte middle;

		[FieldOffset(2)]
		private readonly byte head;

		/// <summary>
		/// Initializes a new instance of the <see cref="Int24"/> struct.
		/// </summary>
		/// <param name="tail">The tail.</param>
		/// <param name="middle">The middle.</param>
		/// <param name="head">The head.</param>
		public Int24(byte tail, byte middle, byte head)
		{
			this.tail = tail;
			this.middle = middle;
			this.head = head;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Int24"/> struct.
		/// </summary>
		/// <param name="shiftedValue">The 8-bit left shifted value. e.g. 0xffffff00 (the last 8 bits will be ignored.)</param>
		public Int24(int shiftedValue)
		{
			tail = (byte)(shiftedValue >> 8);
			middle = (byte)(shiftedValue >> 16);
			head = (byte)(shiftedValue >> 24);
		}

		/// <summary>
		/// Constructs new <see cref="Int24"/> instances from the specified bytes.
		/// </summary>
		/// <param name="buffer">The buffer.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="outbuf">The output buffer.</param>
		/// <param name="dstoffset">The destination offset.</param>
		/// <param name="dstcount">The destination count.</param>
		public static void FromBytes(byte[] buffer, int offset, Int24[] outbuf, int dstoffset, int dstcount)
		{
			int y = dstoffset;
			for (int i = offset; i < offset + dstcount * 3; i += 3, y++)
			{
				outbuf[y] = new Int24(buffer[i], buffer[i + 1], buffer[i + 2]);
			}
		}

		/// <summary>
		/// Converts an array of <see cref="Int24"/>s to the specified array of bytes.
		/// </summary>
		/// <param name="buffer">The buffer.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="outbuf">The output buffer.</param>
		/// <param name="dstoffset">The destination offset.</param>
		/// <param name="srccount">The source count.</param>
		public static void ToBytes(Int24[] buffer, int offset, byte[] outbuf, int dstoffset, int srccount)
		{
			int y = dstoffset;
			for (int i = offset; i < offset + srccount; i++)
			{
				var t = buffer[i];
				outbuf[y++] = t.tail;
				outbuf[y++] = t.middle;
				outbuf[y++] = t.head;
			}
		}

		/// <summary>
		/// Converts to string.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return ((int)this).ToString();
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="Int24"/> to <see cref="int"/>.
		/// </summary>
		/// <param name="v">The input value.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator int(Int24 v)
		{
			return ((v.tail << 8) | (v.middle << 16) | (v.head << 24)) >> 8;
		}

		/// <summary>
		/// Negates a specified <see cref="Int24"/> value.
		/// </summary>
		/// <param name="value">The value to negate.</param>
		/// <returns>
		/// The result of the <paramref name="value"/> parameter multiplied by negative one (-1).
		/// </returns>
		public static Int24 operator -(Int24 value)
		{
			return (Int24)(-(int)value);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="int"/> to <see cref="Int24"/>.
		/// </summary>
		/// <param name="v">The input value.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static explicit operator Int24(int v) => new Int24(v << 8);

		/// <summary>
		/// Performs an explicit conversion from <see cref="Int24"/> to <see cref="float"/>.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static explicit operator Int24(float value) => (Int24)(int)value;

		/// <summary>
		/// Performs an explicit conversion from <see cref="System.Double"/> to <see cref="Int24"/>.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static explicit operator Int24(double value) => (Int24)(int)value;

		/// <summary>
		/// Implements the operator +.
		/// </summary>
		/// <param name="a">a.</param>
		/// <param name="b">The b.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static Int24 operator +(Int24 a, Int24 b)
		{
			return new Int24((int)a + b);
		}

		/// <summary>
		/// Implements the operator -.
		/// </summary>
		/// <param name="a">a.</param>
		/// <param name="b">The b.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		/// TODO Edit XML Comment Template for -
		public static Int24 operator -(Int24 a, Int24 b)
		{
			return (Int24)((int)a - b);
		}

		/// <summary>
		/// Implements the operator *.
		/// </summary>
		/// <param name="a">a.</param>
		/// <param name="b">The b.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		/// TODO Edit XML Comment Template for *
		public static Int24 operator *(Int24 a, Int24 b)
		{
			return (Int24)((int)a * b);
		}

		/// <summary>
		/// Implements the operator /.
		/// </summary>
		/// <param name="a">a.</param>
		/// <param name="b">The b.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		/// TODO Edit XML Comment Template for /
		public static Int24 operator /(Int24 a, Int24 b)
		{
			return (Int24)((int)a + b);
		}

		/// <summary>
		/// Performs an right-shift operation and returns to specified value.
		/// </summary>
		/// <param name="a">a.</param>
		/// <param name="b">The b.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		/// TODO Edit XML Comment Template for >>
		public static Int24 operator >>(Int24 a, int b)
		{
			return (Int24)((int)a >> b);
		}

		/// <summary>
		/// Implements the operator &lt;&lt;.
		/// </summary>
		/// <param name="a">a.</param>
		/// <param name="b">The b.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		/// TODO Edit XML Comment Template for <<
		public static Int24 operator <<(Int24 a, int b)
		{
			return (Int24)((int)a << b);
		}

		/// <summary>
		/// Indicates whether the values of two specified <see cref="Int24"/> objects are equal.
		/// </summary>
		/// <param name="int1">The first <see cref="Int24"/> to compare.</param>
		/// <param name="int2">The second <see cref="Int24"/> to compare.</param>
		/// <returns>
		///   <c>true</c> if the value of int1 is the same as the value of int2; otherwise, <c>false</c>.
		/// </returns>
		/// TODO Edit XML Comment Template for ==
		public static bool operator ==(Int24 int1, Int24 int2)
		{
			return int1.Equals(int2);
		}

		/// <summary>
		/// Indicates whether the values of two specified <see cref="Int24"/> objects are not equal.
		/// </summary>
		/// <param name="int1">The first <see cref="Int24"/> to compare.</param>
		/// <param name="int2">The second <see cref="Int24"/> to compare.</param>
		/// <returns>
		///   <c>true</c> if int1 and int2 are not equal; otherwise, <c>false</c>.
		/// </returns>
		/// TODO Edit XML Comment Template for !=
		public static bool operator !=(Int24 int1, Int24 int2)
		{
			return !(int1 == int2);
		}

		/// <summary>
		/// Determines whether one specified <see cref="Int24"/> is less than another specified <see cref="Int24"/>.
		/// </summary>
		/// <param name="left">The first <see cref="Int24"/> to compare.</param>
		/// <param name="right">The second <see cref="Int24"/> to compare.</param>
		/// <returns>
		///   <c>true</c> if left is less than right; otherwise, <c>false</c>.
		/// </returns>
		public static bool operator <(Int24 left, Int24 right) => (int)left < right;

		/// <summary>
		/// Determines whether one specified <see cref="Int24"/> is greater than another specified <see cref="Int24"/> value.
		/// </summary>
		/// <param name="left">The first <see cref="Int24"/> to compare.</param>
		/// <param name="right">The second <see cref="Int24"/> to compare.</param>
		/// <returns>
		///   <c>true</c> if left is greater than right; otherwise, <c>false</c>.
		/// </returns>
		public static bool operator >(Int24 left, Int24 right) => (int)left > right;

		/// <summary>
		/// Returns a value that indicates whether a specified <see cref="Int24"/> is less than or equal to another specified <see cref="Int24"/>.
		/// </summary>
		/// <param name="left">The first <see cref="Int24"/> to compare.</param>
		/// <param name="right">The second <see cref="Int24"/> to compare.</param>
		/// <returns>
		///   <c>true</c> if left is less than or equal to right; otherwise, <c>false</c>.
		/// </returns>
		public static bool operator <=(Int24 left, Int24 right) => (int)left <= right;

		/// <summary>
		/// Determines whether one specified <see cref="Int24"/> is greater than or equal to another specified <see cref="Int24"/>.
		/// </summary>
		/// <param name="left">The first <see cref="Int24"/> to compare.</param>
		/// <param name="right">The second  <see cref="Int24"/> to compare.</param>
		/// <returns>
		///   <c>true</c> if <see cref="Int24"/> is greater than or equal to <see cref="Int24"/>; otherwise, <c>false</c>.
		/// </returns>
		public static bool operator >=(Int24 left, Int24 right) => (int)left >= right;

		/// <summary>
		/// Determines whether the specified <see cref="object" />, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		/// TODO Edit XML Comment Template for Equals
		public override bool Equals(object obj)
		{
			return obj is Int24 && Equals((Int24)obj);
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
		/// </returns>
		/// TODO Edit XML Comment Template for Equals
		public bool Equals(Int24 other)
		{
			return tail == other.tail &&
				   middle == other.middle &&
				   head == other.head;
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
		/// </returns>
		/// TODO Edit XML Comment Template for GetHashCode
		public override int GetHashCode()
		{
			var hashCode = -428595538;
			hashCode = hashCode * -1521134295 + tail.GetHashCode();
			hashCode = hashCode * -1521134295 + middle.GetHashCode();
			hashCode = hashCode * -1521134295 + head.GetHashCode();
			return hashCode;
		}

		/// <summary>
		/// Compares the value of this instance to a specified <see cref="Int24"/> value and returns an integer that indicates whether this instance is less than, equal to, or greater than the specified <see cref="Int24"/> value.
		/// </summary>
		/// <param name="other">The <see cref="Int24"/> to compare to the current instance.</param>
		/// <returns>
		/// A signed number indicating the relative values of this instance and the other parameter.
		/// </returns>
		public int CompareTo(Int24 other)
		{
			return ((int)this).CompareTo(other);
		}
	}
}
