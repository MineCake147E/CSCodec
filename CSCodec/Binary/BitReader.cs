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
	public partial class BitReader : IDisposable
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
		/// Gets a value indicating whether this instance is EOF.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is EOF; otherwise, <c>false</c>.
		/// </value>
		public bool IsEOF { get; private set; } = false;

		/// <summary>
		/// The buffer
		/// </summary>
		private byte internalCache;

		private int currentIndex;

		/// <summary>
		/// Gets the number of bits remaining. Equivalent to 8 - <see cref="currentIndex"/>.
		/// </summary>
		/// <value>
		/// The number of bits remaining.
		/// </value>
		private int BitsRemaining
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => 8 - currentIndex;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => currentIndex = 8 - value;
		}

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
			bufferedStream = new BufferedStream(BaseStream, bufferSize);
			reader = new BinaryReader(bufferedStream);
			BitsRemaining = 8;
			AdvanceBuffer();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void AdvanceBuffer()
		{
			currentIndex = 0;
			FillBuffer();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void FillBuffer()
		{
			try
			{
				internalCache = reader.ReadByte();
			}
			catch (EndOfStreamException)
			{
				IsEOF = true;
			}
		}

		/// <summary>
		/// Reads the next bit.
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool ReadBit()
		{
			bool value = (internalCache & HSBMask) == HSBMask;
			internalCache <<= 1;
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
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public byte ReadByte()
		{
			byte value = internalCache; // Obtaining ab###### (#: invalid)
			FillBuffer();   //Read new 0bABCDEFGH
			if (currentIndex == 0) return value;    //Means internalCache(abcdefgh) was all valid
			value |= (byte)(internalCache >> BitsRemaining);    //now we need 0b00ABCDEF
			internalCache <<= currentIndex; //Assigning 0bGH######
			return value;   //returning abCDEFGH(0bxxxxxxxx) is all valid
		}

		/// <summary>
		/// Reads the next signed byte value.
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public sbyte ReadSignedByte() => unchecked((sbyte)ReadByte());

		/// <summary>
		/// Reads the next UInt16 value.
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ushort ReadUInt16()
		{
			if (currentIndex == 0)
			{
				ushort value = (ushort)(internalCache << 8);    //Obtaining abcdefgh ########
				FillBuffer();   //Read new ABCDEFGH
				value |= internalCache; //Now value became abcdefgh ABCDEFGH
				FillBuffer();   //Fill buffer for next read
				if (ByteOrder != ByteOrder.BigEndian) return BinaryPrimitives.ReverseEndianness(value); //Read as Big-Endianed so needs reversal
				return value;
			}
			else
			{
				ushort value = (ushort)(internalCache << 8);    //Obtaining ab###### ########
				FillBuffer();   //Read new ABCDEFGH
				value |= (ushort)(internalCache << BitsRemaining);  //Now value became abABCDEF GH######
				FillBuffer();   //Read new STUVWXYZ
				value |= (ushort)(internalCache >> currentIndex);   //Now value became abABCDEF GHSTUVWX
				internalCache <<= currentIndex; //Assigning 0bYZ######
				if (ByteOrder != ByteOrder.BigEndian) return BinaryPrimitives.ReverseEndianness(value); //Read as Big-Endianed so needs reversal
				return value;
			}
		}

		/// <summary>
		/// Reads the next Int16 value.
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public short ReadInt16() => unchecked((short)ReadUInt16());

		/// <summary>
		/// Reads the next UInt32 value.
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint ReadUInt32()
		{
			unchecked
			{
				var end = ByteOrder;
				ByteOrder = ByteOrder.LittleEndian;
				uint value = ReadUInt16();
				value |= (uint)(ReadUInt16() << 16);
				ByteOrder = end;
				if (end != ByteOrder.LittleEndian) return BinaryPrimitives.ReverseEndianness(value); //Read as Little-Endianed so needs reversal
				return value;
			}
		}

		/// <summary>
		/// Reads the next Int32 value.
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int ReadInt32() => unchecked((int)ReadUInt32());

		/// <summary>
		/// Reads the next UInt64 value.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
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
		/// Reads the next Int64 value.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public long ReadInt64() => unchecked((long)ReadUInt64());

		/// <summary>
		/// Reads the bytes from this stream.
		/// </summary>
		/// <param name="outBuffer">The buffer.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int ReadBytes(Memory<byte> outBuffer)
		{
			for (int i = 0; i < outBuffer.Length; i++)
			{
				outBuffer.Span[i] = ReadByte();
			}
			return outBuffer.Length;
		}

		/// <summary>
		/// Reads the next specified N-bit enum from current stream(Higher-bits first).
		/// </summary>
		/// <typeparam name="T">The <see cref="Enum"/> type in specified bit width.</typeparam>
		/// <param name="width">The number of bits to read.
		/// 0 means that <see cref="ReadEnumHighToLow{T}(int)"/> reads the <see cref="EnumBitWidthAttribute.BitWidth"/> for reading.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T ReadEnumHighToLow<T>(int width = 0) where T : unmanaged, Enum, IConvertible
		{
			if (width == 0)
			{
				var U = typeof(T);
				if (!(Attribute.GetCustomAttribute(U, typeof(EnumBitWidthAttribute)) is EnumBitWidthAttribute attr)) throw new ArgumentException("The specified enum has no EnumBitWidthAttribute definition!");
				ulong value = ReadBitsHighToLowShorterThan64(attr.BitWidth);
				return (T)Enum.ToObject(U, value);  //Boxing and Unboxing so that may be slow: BETTER SOLUTION NEEDED
			}
			else
			{
				ulong value = ReadBitsHighToLowShorterThan64(width);
				return (T)Enum.ToObject(typeof(T), value);  //Boxing and Unboxing so that may be slow: BETTER SOLUTION NEEDED
			}
		}

		/// <summary>
		/// Reads the next specified N-bit unsigned enum from current stream(Lower-bits first).
		/// </summary>
		/// <typeparam name="T">The <see cref="Enum"/> type in specified bit width.</typeparam>
		/// <param name="width">The number of bits to read.
		/// 0 means that <see cref="ReadEnumHighToLow{T}(int)"/> reads the <see cref="EnumBitWidthAttribute.BitWidth"/> for reading.</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T ReadEnumLowToHigh<T>(int width = 0) where T : unmanaged, Enum, IConvertible
		{
			if (width == 0)
			{
				var U = typeof(T);
				if (!(Attribute.GetCustomAttribute(U, typeof(EnumBitWidthAttribute)) is EnumBitWidthAttribute attr)) throw new ArgumentException("The specified enum has no EnumBitWidthAttribute definition!");
				ulong value = ReadBitsLowToHighShorterThan64(attr.BitWidth);
				return (T)Enum.ToObject(U, value);  //Boxing and Unboxing so that may be slow: BETTER SOLUTION NEEDED
			}
			else
			{
				ulong value = ReadBitsLowToHighShorterThan64(width);
				return (T)Enum.ToObject(typeof(T), value);  //Boxing and Unboxing so that may be slow: BETTER SOLUTION NEEDED
			}
		}

		/// <summary>
		/// Reads the next specified N-bit unsigned stream from current stream(Higher-bits first).
		/// </summary>
		/// <param name="size">The number of bits to read.</param>
		/// <returns>Little-Endianed(Probably) BigInteger value that contains <paramref name="size"/> bits unsigned integer.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public BigInteger ReadBitsHighToLow(int size)
		{
			BigInteger inbuf = 0;
			for (int i = 0; i < size; i++)
			{
				inbuf <<= 1;
				inbuf |= ReadBit() ? 1 : 0;
			}
			return inbuf;
		}

		/// <summary>
		/// Reads the next specified N-bit unsigned stream from current stream(Lower-bits first).
		/// </summary>
		/// <param name="size">The number of bits to read.</param>
		/// <returns>Little-Endianed(Probably) BigInteger value that contains <paramref name="size"/> bits unsigned integer.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public BigInteger ReadBitsLowToHigh(int size)
		{
			BigInteger inbuf = 0;
			BigInteger mask = BigInteger.One << (size - 1);
			do
			{
				inbuf |= ReadBit() ? mask : 0;
				mask >>= 1;
			} while (mask > 0);
			return inbuf;
		}

		/// <summary>
		/// Reads the next specified N-bit unsigned stream from current stream(Higher-bits first).
		/// </summary>
		/// <param name="size">The number of bits to read.</param>
		/// <returns>UInt64 value that contains <paramref name="size"/> bits unsigned integer.</returns>
		/// <exception cref="ArgumentOutOfRangeException">size - Size must be smaller than 64 and larger than 0!</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ulong ReadBitsHighToLowShorterThan64(int size)
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
		/// Reads the next specified N-bit unsigned stream from current stream(Lower-bits first).
		/// </summary>
		/// <param name="size">The number of bits to read.</param>
		/// <returns>UInt64 value that contains <paramref name="size"/> bits unsigned integer.</returns>
		/// <exception cref="ArgumentOutOfRangeException">size - Size must be smaller than 64 and larger than 0!</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ulong ReadBitsLowToHighShorterThan64(int size)
		{
			if (size > 64 || size <= 0) throw new ArgumentOutOfRangeException(nameof(size), "Size must be smaller than 64 and larger than 0!");
			ulong inbuf = 0ul;
			ulong mask = 1ul << (size - 1);
			do
			{
				inbuf |= ReadBit() ? mask : 0ul;
				mask >>= 1;
			} while (mask > 0);
			return inbuf;
		}

		/// <summary>
		/// Reads the remaining bits in intermediate buffer.
		/// Can be used for skipping aligning zeros.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="length"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ReadRemainingBitsInIntermediateBuffer(out byte value, out int length)
		{
			value = (byte)(internalCache >> currentIndex);
			length = 8 - currentIndex;
			AdvanceBuffer();
		}

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
					reader.Dispose();
					bufferedStream.Dispose();
					BaseStream.Dispose();
				}
				disposedValue = true;
			}
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			//It seems necessary even without a finalizer.
			GC.SuppressFinalize(this);
		}

		#endregion IDisposable Support
	}
}
