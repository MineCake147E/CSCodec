using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace CSCodec.Binary
{
	/// <summary>
	/// Reads data in bit.
	/// </summary>
	public partial class BitReader
	{
		/// <summary>
		/// The value which indicates whether this instance is disposed or not.
		/// </summary>
		protected bool disposedValue = false;

		private const byte HSBMask = 0x80;

		/// <summary>
		/// Gets or sets the byte order for reading integers larger than 8bit.
		/// </summary>
		/// <value>
		/// The byte order.
		/// </value>
		public ByteOrder ByteOrder { get; set; }

		/// <summary>
		/// The base stream
		/// </summary>
		private readonly Stream BaseStream;

		private readonly BufferedStream bufferedStream;

		private readonly BinaryReader reader;

		/// <summary>
		/// The buffer
		/// </summary>
		private byte buffer;

		private int currentIndex;

		/// <summary>
		/// Initializes a new instance of the <see cref="BitReader"/> class.
		/// </summary>
		/// <param name="BaseStream">The base stream.</param>
		/// <param name="bufferSize">The internal buffer's size in bytes.</param>
		/// <exception cref="ArgumentNullException">BaseStream</exception>
		/// <exception cref="ArgumentOutOfRangeException">BufferWidth - BufferWidth must be equal or greater than 16!</exception>
		public BitReader(Stream BaseStream, int bufferSize = 256)
		{
			this.BaseStream = BaseStream ?? throw new ArgumentNullException(nameof(BaseStream));
			bufferedStream = new BufferedStream(BaseStream);
			reader = new BinaryReader(bufferedStream);
			currentIndex = 0;
			buffer = reader.ReadByte();
		}

		private void AdvanceBuffer()
		{
			currentIndex = 0;
			buffer = reader.ReadByte();
		}

		/// <summary>
		/// Reads the next bit.
		/// </summary>
		/// <returns></returns>
		public bool ReadBit()
		{
			bool value = (buffer & HSBMask) == HSBMask;
			buffer <<= 1;
			currentIndex++;
			if (currentIndex == 8)
			{
				AdvanceBuffer();
			}
			return value;
		}

		/// <summary>
		/// Reads the next byte value.
		/// </summary>
		/// <returns></returns>
		public byte ReadByte()
		{
			byte value = buffer;
			AdvanceBuffer();
			if (currentIndex == 0) return value;
			value |= (byte)(buffer >> currentIndex);
			return value;
		}

		/// <summary>
		/// Reads the next signed byte value.
		/// </summary>
		/// <returns></returns>
		public sbyte ReadSignedByte() => (sbyte)ReadByte();

		/// <summary>
		/// Reads the next UInt16 value.
		/// </summary>
		/// <returns></returns>
		public ushort ReadUInt16()
		{
			ushort a = ReadByte();
			ushort b = ReadByte();
			switch (ByteOrder)
			{
				case ByteOrder.LittleEndian:
					return (ushort)((b << 8) | a);

				case ByteOrder.BigEndian:
					return (ushort)((a << 8) | b);

				default:
					throw new InvalidOperationException();
			}
		}

		/// <summary>
		/// Reads the next Int16 value.
		/// </summary>
		/// <returns></returns>
		public short ReadInt16() => (short)ReadUInt16();

		/// <summary>
		/// Reads the next UInt32 value.
		/// </summary>
		/// <returns></returns>
		public uint ReadUInt32()
		{
			uint a = ReadUInt16();
			uint b = ReadUInt16();
			switch (ByteOrder)
			{
				case ByteOrder.LittleEndian:
					return (b << 16) | a;

				case ByteOrder.BigEndian:
					return (a << 16) | b;

				default:
					throw new InvalidOperationException();
			}
		}

		/// <summary>
		/// Reads the next Int32 value.
		/// </summary>
		/// <returns></returns>
		public int ReadInt32() => (int)ReadUInt32();

		/// <summary>
		/// Reads the next UInt64 value.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public ulong ReadUInt64()
		{
			ulong a = ReadUInt32();
			ulong b = ReadUInt32();
			switch (ByteOrder)
			{
				case ByteOrder.LittleEndian:
					return (b << 32) | a;

				case ByteOrder.BigEndian:
					return (a << 32) | b;

				default:
					throw new InvalidOperationException();
			}
		}

		/// <summary>
		/// Reads the bytes from this stream.
		/// </summary>
		/// <param name="outBuffer">The buffer.</param>
		/// <returns></returns>
		public int ReadBytes(Memory<byte> outBuffer)
		{
			if (currentIndex == 0)
			{
				outBuffer.Span[0] = buffer;
				unsafe
				{
					byte[] inbuf = new byte[outBuffer.Length - 1];
					int read = reader.Read(inbuf, 0, inbuf.Length);
					Memory<byte> memory = inbuf;
					memory.CopyTo(outBuffer.Slice(1));
					AdvanceBuffer();
					return read;
				}
			}
			else
			{
				byte[] inbuf = new byte[outBuffer.Length];
				inbuf[0] = (byte)(buffer >> currentIndex);
				int read = reader.Read(inbuf, 1, inbuf.Length - 1);
				int indInv = 8 - currentIndex;
				for (int i = 0; i < outBuffer.Span.Length - 1; i++)
				{
					outBuffer.Span[i] = (byte)((inbuf[i] << currentIndex) | (inbuf[i + 1] >> indInv));
				}
				buffer = reader.ReadByte();
				outBuffer.Span[outBuffer.Length - 1] = (byte)(inbuf[inbuf.Length - 1] << currentIndex | buffer >> indInv);
				buffer <<= currentIndex;
				return read;
			}
		}

		/// <summary>
		/// Reads the next specified N-bit enum from current stream.
		/// </summary>
		/// <typeparam name="T">The <see cref="Enum"/> type that has <see cref="EnumBitWidthAttribute"/> attribute.</typeparam>
		/// <returns></returns>
		public T ReadEnum<T>() where T : unmanaged, Enum, IConvertible
		{
			var U = typeof(T);
			var attr = Attribute.GetCustomAttribute(U, typeof(EnumBitWidthAttribute)) as EnumBitWidthAttribute;
			if (attr == null) throw new ArgumentException("The specified enum has no EnumBitWidthAttribute definition!");
			ulong value = 0;    //Big Endianed bit stream =>unsigned 64bit integer
			for (int i = 0; i < attr.BitWidth; i++)
			{
				value <<= 1;
				value |= ReadBit() ? 1UL : 0UL;
			}
			return (T)Enum.ToObject(U, value);  //Boxing and Unboxing so that may be slow: BETTER SOLUTION NEEDED
		}

		/// <summary>
		/// Reads the next specified N-bit stream from current stream.
		/// </summary>
		/// <param name="size">The number of bits read.</param>
		/// <returns>Little-Endianed(Probably) BigInteger value that contains <paramref name="size"/> bits unsigned integer.</returns>
		public BigInteger ReadBits(int size)
		{
			BigInteger inbuf = 0ul;
			for (int i = 0; i < size; i++)
			{
				inbuf <<= 1;
				inbuf |= ReadBit() ? 1 : 0;
			}
			return inbuf;
		}

		/// <summary>
		/// Reads the next specified N-bit stream from current stream.
		/// </summary>
		/// <param name="size">The number of bits read.</param>
		/// <returns>UInt64 value that contains <paramref name="size"/> bits unsigned integer.</returns>
		/// <exception cref="ArgumentOutOfRangeException">size - Size must be smaller than 64 and larger than 0!</exception>
		public ulong ReadBitsShorterThan64(int size)
		{
			if (size > 64 || size <= 0) throw new ArgumentOutOfRangeException(nameof(size), "Size must be smaller than 64 and larger than 0!");
			ulong inbuf = 0ul;
			for (int i = 0; i < size; i++)
			{
				inbuf <<= 1;
				inbuf |= ReadBit() ? 1ul : 0ul;
			}
			return inbuf;
		}

		/// <summary>
		/// Reads the remaining bits in intermediate buffer.
		/// </summary>
		/// <returns></returns>
		public void ReadRemainingBitsInIntermediateBuffer(out byte value, out int length)
		{
			value = (byte)(buffer >> currentIndex);
			length = 8 - currentIndex;
			AdvanceBuffer();
		}
	}
}
