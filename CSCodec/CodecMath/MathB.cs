using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Buffers.Binary;
using System.Text;
using System.Runtime.InteropServices;

namespace System
{
    /// <summary>
    /// Supports some bit arithmetics.
    /// </summary>
    public static class MathB
    {
        #region CountBits

        /// <summary>
        /// same as floor(log2(i))
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CountBits(ulong i)
        {
            // Reference: https://graphics.stanford.edu/~seander/bithacks.html#IntegerLog
            int r = 0;
            if ((i & 0xFFFF_FFFF_0000_0000uL) != 0)
            {
                i >>= 32;
                r |= 32;
            }
            if ((i & 0xFFFF0000u) != 0)
            {
                i >>= 16;
                r |= 16;
            }
            if ((i & 0xFF00) != 0)
            {
                i >>= 8;
                r |= 8;
            }
            if ((i & 0xF0) != 0)
            {
                i >>= 4;
                r |= 4;
            }
            if ((i & 0xC) != 0)
            {
                i >>= 2;
                r |= 2;
            }
            if ((i & 0x2) != 0)
                r |= 1;
            return r;
        }

        /// <summary>
        /// same as floor(log2(i))
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CountBits(uint i)
        {
            // Reference: https://graphics.stanford.edu/~seander/bithacks.html#IntegerLog
            int r = 0;
            if ((i & 0xFFFF0000u) != 0)
            {
                i >>= 16;
                r |= 16;
            }
            if ((i & 0xFF00) != 0)
            {
                i >>= 8;
                r |= 8;
            }
            if ((i & 0xF0) != 0)
            {
                i >>= 4;
                r |= 4;
            }
            if ((i & 0xC) != 0)
            {
                i >>= 2;
                r |= 2;
            }
            if ((i & 0x2) != 0)
                r |= 1;
            return r;
        }

        /// <summary>
        /// same as floor(log2(i))
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CountBits(ushort i)
        {
            // Reference: https://graphics.stanford.edu/~seander/bithacks.html#IntegerLog
            int r = 0;
            if ((i & 0xFF00) != 0)
            {
                i >>= 8;
                r |= 8;
            }
            if ((i & 0xF0) != 0)
            {
                i >>= 4;
                r |= 4;
            }
            if ((i & 0xC) != 0)
            {
                i >>= 2;
                r |= 2;
            }
            if ((i & 0x2) != 0)
                r |= 1;
            return r;
        }

        /// <summary>
        /// same as floor(log2(i))
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CountBits(byte i)
        {
            // Reference: https://graphics.stanford.edu/~seander/bithacks.html#IntegerLog
            int r = 0;
            if ((i & 0xF0) != 0)
            {
                i >>= 4;
                r |= 4;
            }
            if ((i & 0xC) != 0)
            {
                i >>= 2;
                r |= 2;
            }
            if ((i & 0x2) != 0)
                r |= 1;
            return r;
        }

        #endregion CountBits

        #region ReverseBits

        /// <summary>
        /// Reverses the bits of the specified value in specified width.
        /// </summary>
        /// <param name="i">The value.</param>
        /// <param name="width">The width in bits.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ReverseBits(this uint i, int width)
        {
            int shift = 32 - width;
            return ReverseBits(i << shift);
        }

        /// <summary>
        /// Reverses the bits of the specified value in 64bit.
        /// </summary>
        /// <param name="i">The value to reverse bit order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ReverseBits(ulong i)
        {
            i = ((i & 0x5555_5555_5555_5555) << 1) | ((i >> 1) & 0x5555_5555_5555_5555);
            i = ((i & 0x3333_3333_3333_3333) << 2) | ((i >> 2) & 0x3333_3333_3333_3333);
            i = ((i & 0x0f0f_0f0f_0f0f_0f0f) << 4) | ((i >> 4) & 0x0f0f_0f0f_0f0f_0f0f);
            return BinaryPrimitives.ReverseEndianness(i);
        }

        /// <summary>
        /// Reverses the bits of the specified value in 32bit.
        /// </summary>
        /// <param name="i">The value to reverse bit order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ReverseBits(uint i)
        {
            i = ((i & 0x5555_5555) << 1) | ((i >> 1) & 0x5555_5555);
            i = ((i & 0x3333_3333) << 2) | ((i >> 2) & 0x3333_3333);
            i = ((i & 0x0f0f_0f0f) << 4) | ((i >> 4) & 0x0f0f_0f0f);
            return BinaryPrimitives.ReverseEndianness(i);
        }

        /// <summary>
        /// Reverses the bits of the specified value in 16bit.
        /// </summary>
        /// <param name="i">The value to reverse bit order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ReverseBits(ushort i)
        {
            uint j = i;
            j = ((j >> 1) & 0x5555) | ((j & 0x5555) << 1);
            j = ((j >> 2) & 0x3333) | ((j & 0x3333) << 2);
            j = ((j >> 4) & 0x0f0f) | ((j & 0x0f0f) << 4);
            return BinaryPrimitives.ReverseEndianness(unchecked((ushort)j));
        }

        /// <summary>
        /// Reverses the bits of the specified value in 8bit.
        /// </summary>
        /// <param name="i">The value to reverse bit order.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte ReverseBits(byte i)
        {
            uint x = i;
            x = ((x >> 1) & 0x55u) | ((x & 0x55u) << 1);
            x = ((x >> 2) & 0x33u) | ((x & 0x33u) << 2);
            return (byte)(((x >> 4) & 0x0fu) | ((x & 0x0fu) << 4));
        }

        #endregion ReverseBits

        #region CountZeros

        private static ReadOnlySpan<byte> TrailingZeroCountDeBruijn => new byte[32]
        {
            00, 01, 28, 02, 29, 14, 24, 03,
            30, 22, 20, 15, 25, 17, 04, 08,
            31, 27, 13, 23, 21, 19, 16, 07,
            26, 12, 18, 06, 11, 05, 10, 09
        };

        /// <summary>
        /// Counts the consecutive zero bits on the right.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CountConsecutiveZeros(uint value)
        {
            if (value == 0)
            {
                return 32;
            }

            // uint.MaxValue >> 27 is always in range [0 - 31] so we use Unsafe.AddByteOffset to avoid bounds check
            return Unsafe.AddByteOffset(
                // Using deBruijn sequence, k=2, n=5 (2^5=32) : 0b_0000_0111_0111_1100_1011_0101_0011_0001u
                ref MemoryMarshal.GetReference(TrailingZeroCountDeBruijn),
                // uint|long -> IntPtr cast on 32-bit platforms does expensive overflow checks not needed here
                (IntPtr)(int)(((value & (uint)-(int)value) * 0x077CB531u) >> 27)); // Multi-cast mitigates redundant conv.u8
        }

        #endregion CountZeros

        /// <summary>
        /// Determines whether the specified <paramref name="i"/> is power of two.
        /// </summary>
        /// <param name="i">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is power of two; otherwise, <c>false</c>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOfTwo(this uint i) => i != 0 && (i & (i - 1)) == 0;

        /// <summary>
        /// Determines whether the specified <paramref name="i"/> is power of two.
        /// </summary>
        /// <param name="i">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is power of two; otherwise, <c>false</c>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOfTwo(this int i) => i != 0 && (i & (i - 1)) == 0;

        #region SignedToFromUnsigned

        /// <summary>
        /// Converts to unsigned integer bitwisely(-1L => 0xFFFFFFFFFFFFFFFFUL without exception).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ToUnsigned(long value) => unchecked((ulong)value);

        /// <summary>
        /// Converts to unsigned integer bitwisely(-1 => 0xFFFFFFFFu without exception).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ToUnsigned(int value) => unchecked((uint)value);

        /// <summary>
        /// Converts to unsigned integer bitwisely(-1 => 65535 without exception).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ToUnsigned(short value) => unchecked((ushort)value);

        /// <summary>
        /// Converts to unsigned integer bitwisely(-1 => 255 without exception).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte ToUnsigned(sbyte value) => unchecked((byte)value);

        /// <summary>
        /// Converts to signed integer bitwisely(0xFFFFFFFFFFFFFFFFUL => -1L without exception).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ToSigned(ulong value) => unchecked((long)value);

        /// <summary>
        /// Converts to signed integer bitwisely(0xFFFFFFFFu => -1 without exception).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToSigned(uint value) => unchecked((int)value);

        /// <summary>
        /// Converts to signed integer bitwisely(65535 => -1 without exception).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ToSigned(ushort value) => unchecked((short)value);

        /// <summary>
        /// Converts to signed integer bitwisely(255 => -1 without exception).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte ToSigned(byte value) => unchecked((sbyte)value);

        #endregion SignedToFromUnsigned
    }
}
