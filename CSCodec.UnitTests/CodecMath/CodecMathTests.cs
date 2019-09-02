using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace CSCodec.UnitTests.CodecMath
{
    [TestFixture]
    public class CodecMathTests
    {
        [TestCase(0ul, 0ul)]
        [TestCase(ulong.MaxValue, ulong.MaxValue)]
        [TestCase(0xff00ff00ff00ff00ul, 0x00ff00ff00ff00fful)]
        [TestCase(0xf0f0f0f0f0f0f0f0ul, 0x0f0f0f0f0f0f0f0ful)]
        [TestCase(0xA5A5A5A5A5A5A5A5ul, 0xA5A5A5A5A5A5A5A5ul)]
        [TestCase(0xAAAAAAAA00000000ul, 0x0000000055555555ul)]
        public void BitReverseTestUInt64(ulong valueBefore, ulong valueAfter)
        {
            Assert.AreEqual(valueAfter, MathB.ReverseBits(valueBefore));
        }

        [TestCase(0u, 0u)]
        [TestCase(uint.MaxValue, uint.MaxValue)]
        [TestCase(0xff00ff00u, 0x00ff00ffu)]
        [TestCase(0xf0f0f0f0u, 0x0f0f0f0fu)]
        [TestCase(0xA5A5A5A5u, 0xA5A5A5A5u)]
        [TestCase(0xAAAA0000u, 0x00005555u)]
        public void BitReverseTestUInt32(uint valueBefore, uint valueAfter)
        {
            Assert.AreEqual(valueAfter, MathB.ReverseBits(valueBefore));
        }

        [TestCase((ushort)0, (ushort)0)]
        [TestCase(ushort.MaxValue, ushort.MaxValue)]
        [TestCase((ushort)0xff00u, (ushort)0x00ffu)]
        [TestCase((ushort)0xf0f0u, (ushort)0x0f0fu)]
        [TestCase((ushort)0xA5A5u, (ushort)0xA5A5u)]
        [TestCase((ushort)0xAA00u, (ushort)0x0055u)]
        public void BitReverseTestUInt16(ushort valueBefore, ushort valueAfter)
        {
            Assert.AreEqual(valueAfter, MathB.ReverseBits(valueBefore));
        }

        [TestCase((byte)0, (byte)0)]
        [TestCase(byte.MaxValue, byte.MaxValue)]
        [TestCase((byte)0xf0u, (byte)0x0fu)]
        [TestCase((byte)0xf0u, (byte)0x0fu)]
        [TestCase((byte)0xA5u, (byte)0xA5u)]
        [TestCase((byte)0xA0u, (byte)0x05u)]
        public void BitReverseTestByte(byte valueBefore, byte valueAfter)
        {
            Assert.AreEqual(valueAfter, MathB.ReverseBits(valueBefore));
        }

        public static IEnumerable<TestCaseData> PowerOfTwoCheckTestCaseSource
        {
            get
            {
                yield return new TestCaseData(1u, true);
                for (int i = 1; i < 31; i++)
                {
                    yield return new TestCaseData(1u << i, true);
                    yield return new TestCaseData((1u << i) + 1u, false);
                }
            }
        }

        [Test, TestCaseSource(nameof(PowerOfTwoCheckTestCaseSource))]
        public void PowerOfTwoCheckTestUInt32(uint value, bool isPowerOfTwo)
        {
            Assert.AreEqual(isPowerOfTwo, value.IsPowerOfTwo());
        }

        public static IEnumerable<TestCaseData> CountBitsTestCaseSource
        {
            get
            {
                yield return new TestCaseData(1u, 0);
                yield return new TestCaseData(0u, 0);
                for (int i = 1; i < 31; i++)
                {
                    yield return new TestCaseData(1u << i, i);
                    yield return new TestCaseData((1u << i) - 1u, i - 1);
                }
            }
        }

        [Test, TestCaseSource(nameof(CountBitsTestCaseSource))]
        public void CountBitsTest(uint value, int bits)
        {
            Assert.AreEqual(bits, MathB.CountBits(value));
        }
    }
}
