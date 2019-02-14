using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace CSCodec.Binary
{
	/// <summary>
	/// Writes data in bit.
	/// </summary>
	public partial class BitWriter : IDisposable
	{
		/// <summary>
		/// The value which indicates whether this instance is disposed or not.
		/// </summary>
		private bool disposedValue = false;

		/// <summary>
		/// The base stream
		/// </summary>
		private readonly Stream BaseStream;

		/// <summary>
		/// Gets or sets the byte order for reading integers larger than 8bit.
		/// </summary>
		/// <value>
		/// The byte order.
		/// </value>
		public ByteOrder ByteOrder { get; set; }

		/// <summary>
		/// Gets or sets the buffer.
		/// </summary>
		/// <value>
		/// The buffer.
		/// </value>
		private Memory<byte> InternalBuffer { get; set; }

#pragma warning disable S3459 // Unassigned members should be removed

		/// <summary>
		/// Gets the positioned buffer.
		/// </summary>
		/// <value>
		/// The positioned buffer.
		/// </value>
		private Memory<byte> PositionedBuffer
#pragma warning restore S3459 // Unassigned members should be removed
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => InternalBuffer.Slice(Position);
		}

		/// <summary>
		/// The primitive buffer
		/// </summary>
		private byte[] PrimitiveBuffer { get; set; }

		/// <summary>
		/// The flushing conversion buffer.
		/// </summary>
		private Memory<byte> ConversionBuffer { get; set; }

		/// <summary>
		/// The position of Buffer in Bytes.
		/// </summary>
		private int Position = 0;

		private uint Cache = 0;

		/// <summary>
		/// The location of cache in bits.
		/// </summary>
		private int location = 32;

		/// <summary>
		/// Initializes a new instance of the <see cref="BitWriter"/> class.
		/// </summary>
		/// <param name="BaseStream">The base stream.</param>
		/// <param name="bufferSize">Size of the buffer divided by <c>sizeof(int)</c>.</param>
		/// <exception cref="ArgumentNullException">BaseStream</exception>
		public BitWriter(Stream BaseStream, int bufferSize = 256)
		{
			this.BaseStream = BaseStream ?? throw new ArgumentNullException(nameof(BaseStream));
			InternalBuffer = PrimitiveBuffer = new byte[bufferSize * sizeof(int)];  //Buffer points to PrimitiveBuffer.
			ConversionBuffer = new byte[sizeof(int)];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void FlushCache()
		{
			location = 32;
			BinaryPrimitives.WriteUInt32BigEndian(ConversionBuffer.Span, Cache);  //Force Big-Endianed
			if (InternalBuffer.Length - Position < sizeof(int))
			{
				int lenFlush = InternalBuffer.Length - Position;
				ConversionBuffer.Span.Slice(0, lenFlush).CopyTo(PositionedBuffer.Span);
				FlushInternalBuffer();
				ConversionBuffer.Span.Slice(lenFlush).CopyTo(InternalBuffer.Span);
				Position += sizeof(int) - lenFlush;
			}
			else
			{
				ConversionBuffer.Span.CopyTo(PositionedBuffer.Span);
				Position += sizeof(int);
				if (Position >= InternalBuffer.Length)
				{
					FlushInternalBuffer();
				}
			}
			Cache = 0;
			ConversionBuffer.Span.Fill(0);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void FlushInternalBuffer()
		{
			BaseStream.Write(PrimitiveBuffer, 0, PrimitiveBuffer.Length);
			InternalBuffer.Span.Fill(0);
			Position = 0;
		}

		/// <summary>
		///  Clears all buffers for this stream and causes any buffered data to be written to the underlying device.
		/// </summary>
		public void Flush()
		{
			AlignInBytes();
			if (location == 0) FlushCache();
			else
			{
				var bytesToWrite = (32 - location) / 8;
				if (bytesToWrite > 0)
				{
					location = 32;
					BinaryPrimitives.WriteUInt32BigEndian(ConversionBuffer.Span, Cache);  //Force Big-Endianed
					if (InternalBuffer.Length - Position < bytesToWrite)
					{
						int lenFlush = InternalBuffer.Length - Position;
						ConversionBuffer.Span.Slice(0, lenFlush).CopyTo(PositionedBuffer.Span);
						FlushInternalBuffer();
						ConversionBuffer.Span.Slice(lenFlush, bytesToWrite - lenFlush).CopyTo(InternalBuffer.Span);
						Position += bytesToWrite - lenFlush;
					}
					else
					{
						ConversionBuffer.Span.CopyTo(PositionedBuffer.Span);
						Position += bytesToWrite;
						if (Position >= InternalBuffer.Length)
						{
							FlushInternalBuffer();
						}
					}
				}
				ConversionBuffer.Span.Fill(0);
			}
			BaseStream.Write(PrimitiveBuffer, 0, Position);
			InternalBuffer.Span.Fill(0);
			Position = 0;
		}

		/// <summary>
		/// Writes the specified single bit.
		/// </summary>
		/// <param name="value">The bit value to write. 1 when <c>true</c>, 0 when <c>false</c>.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteBit(bool value)
		{
			location--;
			if (value)
			{
				Cache |= 1u << location;
			}
			if (location == 0) FlushCache();
		}

		/// <summary>
		/// Writes the specified <see cref="byte"/> value.
		/// </summary>
		/// <param name="value">The value to write.</param>
		public void WriteByte(byte value)
		{
			if (location > 8)
			{
				location -= 8;
				Cache |= (uint)value << location;
			}
			else
			{
				var shift = 8 - location;
				Cache |= (uint)value >> shift;
				FlushCache();
				if (shift == 0) return;
				location -= shift;
				Cache |= (uint)value << location;
			}
		}

		/// <summary>
		/// Writes the specified <see cref="sbyte"/> value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void WriteSignedByte(sbyte value) => WriteByte(unchecked((byte)value));

		/// <summary>
		/// Writes the specified <see cref="ushort"/> value.
		/// </summary>
		/// <param name="value">The value to write.</param>
		public void WriteUInt16(ushort value)
		{
			if (ByteOrder != ByteOrder.BigEndian) value = BinaryPrimitives.ReverseEndianness(value);
			if (location > 16)
			{
				location -= 16;
				Cache |= (uint)value << location;
			}
			else
			{
				var shift = 16 - location;
				Cache |= (uint)value >> shift;
				FlushCache();
				if (shift == 0) return;
				location -= shift;
				Cache |= (uint)value << location;
			}
		}

		/// <summary>
		/// Writes the specified <see cref="short"/> value.
		/// </summary>
		/// <param name="value">The value to write.</param>
		public void WriteInt16(short value) => WriteUInt16(unchecked((ushort)value));

		/// <summary>
		/// Writes the specified <see cref="uint"/> value.
		/// </summary>
		/// <param name="value">The value to write.</param>
		public void WriteUInt32(uint value)
		{
			value = ReverseEndianessIfNessesary(value);
			unchecked
			{
				var shift = 32 - location;
				Cache |= value >> shift;
				FlushCache();
				if (shift == 0) return;
				location -= shift;
				Cache |= value << location;
			}
		}

		/// <summary>
		/// Writes the specified <see cref="int"/> value.
		/// </summary>
		/// <param name="value">The value to write.</param>
		public void WriteInt32(int value) => WriteUInt32(unchecked((uint)value));

		/// <summary>
		/// Writes the specified <see cref="ulong"/> value.
		/// </summary>
		/// <param name="value">The value to write.</param>
		public void WriteUInt64(ulong value)
		{
			value = ReverseEndianessIfNessesary(value);
			unchecked
			{
				WriteUInt32((uint)(value >> 32));
				WriteUInt32((uint)value);
			}
		}

		/// <summary>
		/// Writes the specified <see cref="long"/> value.
		/// </summary>
		/// <param name="value">The value to write.</param>
		public void WriteInt64(long value) => WriteUInt64(unchecked((ulong)value));

		/// <summary>
		/// Writes the specified unsigned <see cref="BigInteger"/> value(Higher-bits first).
		/// </summary>
		/// <param name="value">The value to write.</param>
		/// <param name="width">The number of bits to write.</param>
		public void WriteBitsHighToLow(BigInteger value, int width)
		{
			BigInteger mask = (BigInteger)1 << (width - 1);
			do
			{
				WriteBit((value & mask) == mask);
				mask >>= 1;
			} while (mask > 0);
		}

		/// <summary>
		/// Writes the specified unsigned <see cref="BigInteger"/> value(Lower-bits first).
		/// </summary>
		/// <param name="value">The value to write.</param>
		/// <param name="width">The number of bits to write.</param>
		public void WriteBitsLowToHigh(BigInteger value, int width)
		{
			BigInteger mask = BigInteger.One << (width - 1);
			do
			{
				WriteBit((value & mask) == mask);
				mask >>= 1;
			} while (mask > 0);
		}

		/// <summary>
		/// Writes the specified unsigned <see cref="BigInteger"/> value(Higher-bits first).
		/// </summary>
		/// <param name="value">The value to write.</param>
		/// <param name="width">The number of bits to write.
		/// 0 means that <see cref="WriteEnumHighToLow{T}(T, int)"/> reads the <see cref="EnumBitWidthAttribute.BitWidth"/> for writing.</param>
		/// <typeparam name="T">The Enum definition to write.</typeparam>
		public void WriteEnumHighToLow<T>(T value, int width = 0) where T : unmanaged, Enum, IConvertible
		{
			ulong valueToWrite = Convert.ToUInt64(Convert.ChangeType(value, value.GetTypeCode()));
			if (width == 0)
			{
				var U = typeof(T);
				if (!(Attribute.GetCustomAttribute(U, typeof(EnumBitWidthAttribute)) is EnumBitWidthAttribute attr)) throw new ArgumentException("The specified enum has no EnumBitWidthAttribute definition!");
				width = attr.BitWidth;
			}
			WriteBitsHighToLow(valueToWrite, width);
		}

		/// <summary>
		/// Writes the specified unsigned <see cref="BigInteger"/> value(Lower-bits first).
		/// </summary>
		/// <param name="value">The value to write.</param>
		/// <param name="width">The number of bits to write.
		/// 0 means that <see cref="WriteEnumLowToHigh{T}(T, int)"/> reads the <see cref="EnumBitWidthAttribute.BitWidth"/> for writing.</param>
		/// <typeparam name="T">The Enum definition to write.</typeparam>
		public void WriteEnumLowToHigh<T>(T value, int width = 0) where T : unmanaged, Enum, IConvertible
		{
			ulong valueToWrite = Convert.ToUInt64(Convert.ChangeType(value, value.GetTypeCode()));
			WriteBitsLowToHigh(valueToWrite, width);
		}

		/// <summary>
		/// Aligns the in bytes.
		/// </summary>
		/// <param name="defaultValue">The bit value to write for padding. 1 when <c>true</c>, 0 when <c>false</c>.</param>
		/// <returns>The number of bits written.</returns>
		public int AlignInBytes(bool defaultValue = false)
		{
			int val = location % 8;
			if (defaultValue)
			{
				while (val > 0)
				{
					WriteBit(true);
					val--;
				}
			}
			else
			{
				location -= val;
			}
			if (location == 0) FlushCache();
			return val;
		}

		#region EndianessHandlers

		/// <summary>
		/// Reverses the endianess if nessesary.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ulong ReverseEndianessIfNessesary(ulong value)
		{
			if (ByteOrder != ByteOrder.BigEndian) return BinaryPrimitives.ReverseEndianness(value);
			return value;
		}

		/// <summary>
		/// Reverses the endianess if nessesary.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private uint ReverseEndianessIfNessesary(uint value)
		{
			if (ByteOrder != ByteOrder.BigEndian) return BinaryPrimitives.ReverseEndianness(value);
			return value;
		}

		/// <summary>
		/// Reverses the endianess if nessesary.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ushort ReverseEndianessIfNessesary(ushort value)
		{
			if (ByteOrder != ByteOrder.BigEndian) return BinaryPrimitives.ReverseEndianness(value);
			return value;
		}

		/// <summary>
		/// Reverses the endianess if nessesary.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private IEnumerable<byte> ReverseEndianessIfNessesary(BigInteger value)
		{
			if (ByteOrder != ByteOrder.BigEndian) return value.ToByteArray().Reverse();
			return value.ToByteArray();
		}

		#endregion EndianessHandlers

		#region IDisposable Support

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					BaseStream.Dispose();
				}
				ConversionBuffer = default;
				InternalBuffer = default;
				PrimitiveBuffer = null;
				disposedValue = true;
			}
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion IDisposable Support
	}
}
