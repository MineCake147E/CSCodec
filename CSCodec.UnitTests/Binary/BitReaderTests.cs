using System;
using System.Buffers.Binary;
using System.Numerics;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using CSCodec.Utils;
using CSCodec.Binary;

namespace CSCodec.UnitTests.Binary
{
	[TestFixture]
	public class BitReaderTests
	{
		[TestCase(byte.MinValue)]
		[TestCase(byte.MaxValue)]
		public void ReadByteTest(byte value)
		{
			byte[] data = new byte[16];
			data[0] = value;
			ArrayReadStream stream = new ArrayReadStream(data);
			BitReader reader = new BitReader(stream);
			Assert.AreEqual(value, reader.ReadByte());
		}

		[TestCase(sbyte.MinValue)]
		[TestCase(sbyte.MaxValue)]
		public void ReadSByteTest(sbyte value)
		{
			byte[] data = new byte[16];
			data[0] = unchecked((byte)value);
			ArrayReadStream stream = new ArrayReadStream(data);
			BitReader reader = new BitReader(stream);
			Assert.AreEqual(value, reader.ReadSignedByte());
		}

		[TestCase(short.MaxValue, ByteOrder.BigEndian)]
		[TestCase(short.MinValue, ByteOrder.BigEndian)]
		[TestCase(short.MaxValue, ByteOrder.LittleEndian)]
		[TestCase(short.MinValue, ByteOrder.LittleEndian)]
		public void ReadShortTest(short value, ByteOrder byteOrder)
		{
			Memory<byte> data = new byte[16];
			BinaryPrimitives.WriteInt16LittleEndian(data.Span, byteOrder == ByteOrder.LittleEndian ? value : BinaryPrimitives.ReverseEndianness(value));
			ArrayReadStream stream = new ArrayReadStream(data);
			BitReader reader = new BitReader(stream)
			{
				ByteOrder = byteOrder
			};
			Assert.AreEqual(value, reader.ReadInt16());
		}

		[TestCase(ushort.MaxValue, ByteOrder.BigEndian)]
		[TestCase(ushort.MinValue, ByteOrder.BigEndian)]
		[TestCase(ushort.MaxValue, ByteOrder.LittleEndian)]
		[TestCase(ushort.MinValue, ByteOrder.LittleEndian)]
		public void ReadUShortTest(ushort value, ByteOrder byteOrder)
		{
			Memory<byte> data = new byte[16];
			BinaryPrimitives.WriteUInt16LittleEndian(data.Span, byteOrder == ByteOrder.LittleEndian ? value : BinaryPrimitives.ReverseEndianness(value));
			ArrayReadStream stream = new ArrayReadStream(data);
			BitReader reader = new BitReader(stream)
			{
				ByteOrder = byteOrder
			};
			Assert.AreEqual(value, reader.ReadUInt16());
		}

		[TestCase(int.MaxValue, ByteOrder.BigEndian)]
		[TestCase(int.MinValue, ByteOrder.BigEndian)]
		[TestCase(int.MaxValue, ByteOrder.LittleEndian)]
		[TestCase(int.MinValue, ByteOrder.LittleEndian)]
		public void ReadIntTest(int value, ByteOrder byteOrder)
		{
			Memory<byte> data = new byte[16];
			BinaryPrimitives.WriteInt32LittleEndian(data.Span, byteOrder == ByteOrder.LittleEndian ? value : BinaryPrimitives.ReverseEndianness(value));
			ArrayReadStream stream = new ArrayReadStream(data);
			BitReader reader = new BitReader(stream)
			{
				ByteOrder = byteOrder
			};
			Assert.AreEqual(value, reader.ReadInt32());
		}

		[TestCase(uint.MaxValue, ByteOrder.BigEndian)]
		[TestCase(uint.MinValue, ByteOrder.BigEndian)]
		[TestCase(uint.MaxValue, ByteOrder.LittleEndian)]
		[TestCase(uint.MinValue, ByteOrder.LittleEndian)]
		public void ReadUIntTest(uint value, ByteOrder byteOrder)
		{
			Memory<byte> data = new byte[16];
			BinaryPrimitives.WriteUInt32LittleEndian(data.Span, byteOrder == ByteOrder.LittleEndian ? value : BinaryPrimitives.ReverseEndianness(value));
			ArrayReadStream stream = new ArrayReadStream(data);
			BitReader reader = new BitReader(stream)
			{
				ByteOrder = byteOrder
			};
			Assert.AreEqual(value, reader.ReadUInt32());
		}

		[TestCase(long.MaxValue, ByteOrder.BigEndian)]
		[TestCase(long.MinValue, ByteOrder.BigEndian)]
		[TestCase(long.MaxValue, ByteOrder.LittleEndian)]
		[TestCase(long.MinValue, ByteOrder.LittleEndian)]
		public void ReadLongTest(long value, ByteOrder byteOrder)
		{
			Memory<byte> data = new byte[16];
			BinaryPrimitives.WriteInt64LittleEndian(data.Span, byteOrder == ByteOrder.LittleEndian ? value : BinaryPrimitives.ReverseEndianness(value));
			ArrayReadStream stream = new ArrayReadStream(data);
			BitReader reader = new BitReader(stream)
			{
				ByteOrder = byteOrder
			};
			Assert.AreEqual(value, reader.ReadInt64());
		}

		[TestCase(ulong.MaxValue, ByteOrder.BigEndian)]
		[TestCase(ulong.MinValue, ByteOrder.BigEndian)]
		[TestCase(ulong.MaxValue, ByteOrder.LittleEndian)]
		[TestCase(ulong.MinValue, ByteOrder.LittleEndian)]
		public void ReadULongTest(ulong value, ByteOrder byteOrder)
		{
			Memory<byte> data = new byte[16];
			BinaryPrimitives.WriteUInt64LittleEndian(data.Span, byteOrder == ByteOrder.LittleEndian ? value : BinaryPrimitives.ReverseEndianness(value));
			ArrayReadStream stream = new ArrayReadStream(data);
			BitReader reader = new BitReader(stream)
			{
				ByteOrder = byteOrder
			};
			Assert.AreEqual(value, reader.ReadUInt64());
		}

		private static IEnumerable<TestCaseData> ReadBitsHLTestSource
		{
			get
			{
				var rng = new Random(810931810);
				for (int i = 0; i < 10; i++)
				{
					var value = new BigInteger();
					for (int j = 0; j < 63; j++)
					{
						value <<= 1;
						value |= rng.NextDouble() > 0.5 ? 1 : 0;
					}
					yield return new TestCaseData(value, 64);
				}
			}
		}

		[Test]
		[TestCaseSource(nameof(ReadBitsHLTestSource))]
		public void ReadBitsHLTest(BigInteger value, int length)
		{
			using (MemoryStream memory = new MemoryStream())
			using (BitWriter writer = new BitWriter(memory))
			{
				writer.WriteBitsHighToLow(value, length);
				writer.Flush();
				memory.Seek(0, SeekOrigin.Begin);
				using (var reader = new BitReader(memory))
				{
					Assert.AreEqual(value, reader.ReadBitsHighToLow(length));
				}
			}
		}

		[Test]
		[TestCaseSource(nameof(ReadBitsHLTestSource))]
		public void ReadBitsLHTest(BigInteger value, int length)
		{
			using (MemoryStream memory = new MemoryStream())
			using (BitWriter writer = new BitWriter(memory))
			{
				writer.WriteBitsLowToHigh(value, length);
				writer.Flush();
				memory.Seek(0, SeekOrigin.Begin);
				using (var reader = new BitReader(memory))
				{
					Assert.AreEqual(value, reader.ReadBitsLowToHigh(length));
				}
			}
		}

		[TestCase]
		public void ReadEnumHLTest()
		{
			using (MemoryStream memory = new MemoryStream())
			using (BitWriter writer = new BitWriter(memory))
			{
				writer.WriteEnumHighToLow(TestEnum.Value0);
				writer.WriteEnumHighToLow(TestEnum.Value1);
				writer.WriteEnumHighToLow(TestEnum.Value2);
				writer.WriteEnumHighToLow(TestEnum.Value3);
				writer.Flush();
				memory.Seek(0, SeekOrigin.Begin);
				using (var reader = new BitReader(memory))
				{
					Assert.AreEqual(TestEnum.Value0, reader.ReadEnumHighToLow<TestEnum>());
					Assert.AreEqual(TestEnum.Value1, reader.ReadEnumHighToLow<TestEnum>());
					Assert.AreEqual(TestEnum.Value2, reader.ReadEnumHighToLow<TestEnum>());
					Assert.AreEqual(TestEnum.Value3, reader.ReadEnumHighToLow<TestEnum>());
				}
			}
		}

		[TestCase]
		public void ReadEnumLHTest()
		{
			using (MemoryStream memory = new MemoryStream())
			using (BitWriter writer = new BitWriter(memory))
			{
				writer.WriteEnumLowToHigh(TestEnum.Value0);
				writer.WriteEnumLowToHigh(TestEnum.Value1);
				writer.WriteEnumLowToHigh(TestEnum.Value2);
				writer.WriteEnumLowToHigh(TestEnum.Value3);
				writer.Flush();
				memory.Seek(0, SeekOrigin.Begin);
				using (var reader = new BitReader(memory))
				{
					Assert.AreEqual(TestEnum.Value0, reader.ReadEnumLowToHigh<TestEnum>());
					Assert.AreEqual(TestEnum.Value1, reader.ReadEnumLowToHigh<TestEnum>());
					Assert.AreEqual(TestEnum.Value2, reader.ReadEnumLowToHigh<TestEnum>());
					Assert.AreEqual(TestEnum.Value3, reader.ReadEnumLowToHigh<TestEnum>());
				}
			}
		}
	}
}
