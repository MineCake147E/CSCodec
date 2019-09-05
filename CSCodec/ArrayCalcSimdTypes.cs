using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CSCodec{
	public static partial class MathV
	{
			/// <summary>
		/// Adds values from <paramref name="bufferB"/> to <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA">The destination <see cref="Span{T}"/></param>
		/// <param name="bufferB"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void AddArray(Span<int> bufferA, ReadOnlySpan<int> bufferB)
		{
			if (bufferB.Length > bufferA.Length) throw new ArgumentException("Insufficient buffer.", nameof(bufferA));
            var remainder = bufferB.Length % Vector<int>.Count;
            var newLength = bufferB.Length - remainder;
            if (newLength != 0)
            {
                var src = MemoryMarshal.Cast<int, Vector<int>>(bufferB);
                var dst = MemoryMarshal.Cast<int, Vector<int>>(bufferA).Slice(0, src.Length);
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] += src[i];
                }
            }
            if (remainder != 0)
            {
                var srcRem = bufferB.Slice(newLength);
                var dstRem = bufferA.Slice(newLength).Slice(0, srcRem.Length);
                for (int i = 0; i < srcRem.Length; i++)
                {
                    dstRem[i] += srcRem[i];
                }
            }
		}
		/// <summary>
		/// Subtracts <paramref name="bufferB"/> from <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="bufferB"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SubtractArray(Span<int> bufferA, ReadOnlySpan<int> bufferB)
		{
			if (bufferB.Length > bufferA.Length) throw new ArgumentException("Insufficient buffer.", nameof(bufferA));
            var remainder = bufferB.Length % Vector<int>.Count;
            var newLength = bufferB.Length - remainder;
            if (newLength != 0)
            {
                var src = MemoryMarshal.Cast<int, Vector<int>>(bufferB);
                var dst = MemoryMarshal.Cast<int, Vector<int>>(bufferA).Slice(0, src.Length);
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] -= src[i];
                }
            }
            if (remainder != 0)
            {
                var srcRem = bufferB.Slice(newLength);
                var dstRem = bufferA.Slice(newLength).Slice(0, srcRem.Length);
                for (int i = 0; i < srcRem.Length; i++)
                {
                    dstRem[i] -= srcRem[i];
                }
            }
		}
			/// <summary>
		/// Adds values from <paramref name="bufferB"/> to <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA">The destination <see cref="Span{T}"/></param>
		/// <param name="bufferB"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void AddArray(Span<uint> bufferA, ReadOnlySpan<uint> bufferB)
		{
			if (bufferB.Length > bufferA.Length) throw new ArgumentException("Insufficient buffer.", nameof(bufferA));
            var remainder = bufferB.Length % Vector<uint>.Count;
            var newLength = bufferB.Length - remainder;
            if (newLength != 0)
            {
                var src = MemoryMarshal.Cast<uint, Vector<uint>>(bufferB);
                var dst = MemoryMarshal.Cast<uint, Vector<uint>>(bufferA).Slice(0, src.Length);
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] += src[i];
                }
            }
            if (remainder != 0)
            {
                var srcRem = bufferB.Slice(newLength);
                var dstRem = bufferA.Slice(newLength).Slice(0, srcRem.Length);
                for (int i = 0; i < srcRem.Length; i++)
                {
                    dstRem[i] += srcRem[i];
                }
            }
		}
		/// <summary>
		/// Subtracts <paramref name="bufferB"/> from <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="bufferB"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SubtractArray(Span<uint> bufferA, ReadOnlySpan<uint> bufferB)
		{
			if (bufferB.Length > bufferA.Length) throw new ArgumentException("Insufficient buffer.", nameof(bufferA));
            var remainder = bufferB.Length % Vector<uint>.Count;
            var newLength = bufferB.Length - remainder;
            if (newLength != 0)
            {
                var src = MemoryMarshal.Cast<uint, Vector<uint>>(bufferB);
                var dst = MemoryMarshal.Cast<uint, Vector<uint>>(bufferA).Slice(0, src.Length);
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] -= src[i];
                }
            }
            if (remainder != 0)
            {
                var srcRem = bufferB.Slice(newLength);
                var dstRem = bufferA.Slice(newLength).Slice(0, srcRem.Length);
                for (int i = 0; i < srcRem.Length; i++)
                {
                    dstRem[i] -= srcRem[i];
                }
            }
		}
			/// <summary>
		/// Adds values from <paramref name="bufferB"/> to <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA">The destination <see cref="Span{T}"/></param>
		/// <param name="bufferB"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void AddArray(Span<long> bufferA, ReadOnlySpan<long> bufferB)
		{
			if (bufferB.Length > bufferA.Length) throw new ArgumentException("Insufficient buffer.", nameof(bufferA));
            var remainder = bufferB.Length % Vector<long>.Count;
            var newLength = bufferB.Length - remainder;
            if (newLength != 0)
            {
                var src = MemoryMarshal.Cast<long, Vector<long>>(bufferB);
                var dst = MemoryMarshal.Cast<long, Vector<long>>(bufferA).Slice(0, src.Length);
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] += src[i];
                }
            }
            if (remainder != 0)
            {
                var srcRem = bufferB.Slice(newLength);
                var dstRem = bufferA.Slice(newLength).Slice(0, srcRem.Length);
                for (int i = 0; i < srcRem.Length; i++)
                {
                    dstRem[i] += srcRem[i];
                }
            }
		}
		/// <summary>
		/// Subtracts <paramref name="bufferB"/> from <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="bufferB"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SubtractArray(Span<long> bufferA, ReadOnlySpan<long> bufferB)
		{
			if (bufferB.Length > bufferA.Length) throw new ArgumentException("Insufficient buffer.", nameof(bufferA));
            var remainder = bufferB.Length % Vector<long>.Count;
            var newLength = bufferB.Length - remainder;
            if (newLength != 0)
            {
                var src = MemoryMarshal.Cast<long, Vector<long>>(bufferB);
                var dst = MemoryMarshal.Cast<long, Vector<long>>(bufferA).Slice(0, src.Length);
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] -= src[i];
                }
            }
            if (remainder != 0)
            {
                var srcRem = bufferB.Slice(newLength);
                var dstRem = bufferA.Slice(newLength).Slice(0, srcRem.Length);
                for (int i = 0; i < srcRem.Length; i++)
                {
                    dstRem[i] -= srcRem[i];
                }
            }
		}
			/// <summary>
		/// Adds values from <paramref name="bufferB"/> to <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA">The destination <see cref="Span{T}"/></param>
		/// <param name="bufferB"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void AddArray(Span<ulong> bufferA, ReadOnlySpan<ulong> bufferB)
		{
			if (bufferB.Length > bufferA.Length) throw new ArgumentException("Insufficient buffer.", nameof(bufferA));
            var remainder = bufferB.Length % Vector<ulong>.Count;
            var newLength = bufferB.Length - remainder;
            if (newLength != 0)
            {
                var src = MemoryMarshal.Cast<ulong, Vector<ulong>>(bufferB);
                var dst = MemoryMarshal.Cast<ulong, Vector<ulong>>(bufferA).Slice(0, src.Length);
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] += src[i];
                }
            }
            if (remainder != 0)
            {
                var srcRem = bufferB.Slice(newLength);
                var dstRem = bufferA.Slice(newLength).Slice(0, srcRem.Length);
                for (int i = 0; i < srcRem.Length; i++)
                {
                    dstRem[i] += srcRem[i];
                }
            }
		}
		/// <summary>
		/// Subtracts <paramref name="bufferB"/> from <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="bufferB"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SubtractArray(Span<ulong> bufferA, ReadOnlySpan<ulong> bufferB)
		{
			if (bufferB.Length > bufferA.Length) throw new ArgumentException("Insufficient buffer.", nameof(bufferA));
            var remainder = bufferB.Length % Vector<ulong>.Count;
            var newLength = bufferB.Length - remainder;
            if (newLength != 0)
            {
                var src = MemoryMarshal.Cast<ulong, Vector<ulong>>(bufferB);
                var dst = MemoryMarshal.Cast<ulong, Vector<ulong>>(bufferA).Slice(0, src.Length);
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] -= src[i];
                }
            }
            if (remainder != 0)
            {
                var srcRem = bufferB.Slice(newLength);
                var dstRem = bufferA.Slice(newLength).Slice(0, srcRem.Length);
                for (int i = 0; i < srcRem.Length; i++)
                {
                    dstRem[i] -= srcRem[i];
                }
            }
		}
			/// <summary>
		/// Adds values from <paramref name="bufferB"/> to <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA">The destination <see cref="Span{T}"/></param>
		/// <param name="bufferB"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void AddArray(Span<short> bufferA, ReadOnlySpan<short> bufferB)
		{
			if (bufferB.Length > bufferA.Length) throw new ArgumentException("Insufficient buffer.", nameof(bufferA));
            var remainder = bufferB.Length % Vector<short>.Count;
            var newLength = bufferB.Length - remainder;
            if (newLength != 0)
            {
                var src = MemoryMarshal.Cast<short, Vector<short>>(bufferB);
                var dst = MemoryMarshal.Cast<short, Vector<short>>(bufferA).Slice(0, src.Length);
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] += src[i];
                }
            }
            if (remainder != 0)
            {
                var srcRem = bufferB.Slice(newLength);
                var dstRem = bufferA.Slice(newLength).Slice(0, srcRem.Length);
                for (int i = 0; i < srcRem.Length; i++)
                {
                    dstRem[i] += srcRem[i];
                }
            }
		}
		/// <summary>
		/// Subtracts <paramref name="bufferB"/> from <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="bufferB"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SubtractArray(Span<short> bufferA, ReadOnlySpan<short> bufferB)
		{
			if (bufferB.Length > bufferA.Length) throw new ArgumentException("Insufficient buffer.", nameof(bufferA));
            var remainder = bufferB.Length % Vector<short>.Count;
            var newLength = bufferB.Length - remainder;
            if (newLength != 0)
            {
                var src = MemoryMarshal.Cast<short, Vector<short>>(bufferB);
                var dst = MemoryMarshal.Cast<short, Vector<short>>(bufferA).Slice(0, src.Length);
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] -= src[i];
                }
            }
            if (remainder != 0)
            {
                var srcRem = bufferB.Slice(newLength);
                var dstRem = bufferA.Slice(newLength).Slice(0, srcRem.Length);
                for (int i = 0; i < srcRem.Length; i++)
                {
                    dstRem[i] -= srcRem[i];
                }
            }
		}
			/// <summary>
		/// Adds values from <paramref name="bufferB"/> to <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA">The destination <see cref="Span{T}"/></param>
		/// <param name="bufferB"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void AddArray(Span<ushort> bufferA, ReadOnlySpan<ushort> bufferB)
		{
			if (bufferB.Length > bufferA.Length) throw new ArgumentException("Insufficient buffer.", nameof(bufferA));
            var remainder = bufferB.Length % Vector<ushort>.Count;
            var newLength = bufferB.Length - remainder;
            if (newLength != 0)
            {
                var src = MemoryMarshal.Cast<ushort, Vector<ushort>>(bufferB);
                var dst = MemoryMarshal.Cast<ushort, Vector<ushort>>(bufferA).Slice(0, src.Length);
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] += src[i];
                }
            }
            if (remainder != 0)
            {
                var srcRem = bufferB.Slice(newLength);
                var dstRem = bufferA.Slice(newLength).Slice(0, srcRem.Length);
                for (int i = 0; i < srcRem.Length; i++)
                {
                    dstRem[i] += srcRem[i];
                }
            }
		}
		/// <summary>
		/// Subtracts <paramref name="bufferB"/> from <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="bufferB"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SubtractArray(Span<ushort> bufferA, ReadOnlySpan<ushort> bufferB)
		{
			if (bufferB.Length > bufferA.Length) throw new ArgumentException("Insufficient buffer.", nameof(bufferA));
            var remainder = bufferB.Length % Vector<ushort>.Count;
            var newLength = bufferB.Length - remainder;
            if (newLength != 0)
            {
                var src = MemoryMarshal.Cast<ushort, Vector<ushort>>(bufferB);
                var dst = MemoryMarshal.Cast<ushort, Vector<ushort>>(bufferA).Slice(0, src.Length);
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] -= src[i];
                }
            }
            if (remainder != 0)
            {
                var srcRem = bufferB.Slice(newLength);
                var dstRem = bufferA.Slice(newLength).Slice(0, srcRem.Length);
                for (int i = 0; i < srcRem.Length; i++)
                {
                    dstRem[i] -= srcRem[i];
                }
            }
		}
			/// <summary>
		/// Adds values from <paramref name="bufferB"/> to <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA">The destination <see cref="Span{T}"/></param>
		/// <param name="bufferB"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void AddArray(Span<sbyte> bufferA, ReadOnlySpan<sbyte> bufferB)
		{
			if (bufferB.Length > bufferA.Length) throw new ArgumentException("Insufficient buffer.", nameof(bufferA));
            var remainder = bufferB.Length % Vector<sbyte>.Count;
            var newLength = bufferB.Length - remainder;
            if (newLength != 0)
            {
                var src = MemoryMarshal.Cast<sbyte, Vector<sbyte>>(bufferB);
                var dst = MemoryMarshal.Cast<sbyte, Vector<sbyte>>(bufferA).Slice(0, src.Length);
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] += src[i];
                }
            }
            if (remainder != 0)
            {
                var srcRem = bufferB.Slice(newLength);
                var dstRem = bufferA.Slice(newLength).Slice(0, srcRem.Length);
                for (int i = 0; i < srcRem.Length; i++)
                {
                    dstRem[i] += srcRem[i];
                }
            }
		}
		/// <summary>
		/// Subtracts <paramref name="bufferB"/> from <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="bufferB"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SubtractArray(Span<sbyte> bufferA, ReadOnlySpan<sbyte> bufferB)
		{
			if (bufferB.Length > bufferA.Length) throw new ArgumentException("Insufficient buffer.", nameof(bufferA));
            var remainder = bufferB.Length % Vector<sbyte>.Count;
            var newLength = bufferB.Length - remainder;
            if (newLength != 0)
            {
                var src = MemoryMarshal.Cast<sbyte, Vector<sbyte>>(bufferB);
                var dst = MemoryMarshal.Cast<sbyte, Vector<sbyte>>(bufferA).Slice(0, src.Length);
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] -= src[i];
                }
            }
            if (remainder != 0)
            {
                var srcRem = bufferB.Slice(newLength);
                var dstRem = bufferA.Slice(newLength).Slice(0, srcRem.Length);
                for (int i = 0; i < srcRem.Length; i++)
                {
                    dstRem[i] -= srcRem[i];
                }
            }
		}
			/// <summary>
		/// Adds values from <paramref name="bufferB"/> to <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA">The destination <see cref="Span{T}"/></param>
		/// <param name="bufferB"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void AddArray(Span<byte> bufferA, ReadOnlySpan<byte> bufferB)
		{
			if (bufferB.Length > bufferA.Length) throw new ArgumentException("Insufficient buffer.", nameof(bufferA));
            var remainder = bufferB.Length % Vector<byte>.Count;
            var newLength = bufferB.Length - remainder;
            if (newLength != 0)
            {
                var src = MemoryMarshal.Cast<byte, Vector<byte>>(bufferB);
                var dst = MemoryMarshal.Cast<byte, Vector<byte>>(bufferA).Slice(0, src.Length);
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] += src[i];
                }
            }
            if (remainder != 0)
            {
                var srcRem = bufferB.Slice(newLength);
                var dstRem = bufferA.Slice(newLength).Slice(0, srcRem.Length);
                for (int i = 0; i < srcRem.Length; i++)
                {
                    dstRem[i] += srcRem[i];
                }
            }
		}
		/// <summary>
		/// Subtracts <paramref name="bufferB"/> from <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="bufferB"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SubtractArray(Span<byte> bufferA, ReadOnlySpan<byte> bufferB)
		{
			if (bufferB.Length > bufferA.Length) throw new ArgumentException("Insufficient buffer.", nameof(bufferA));
            var remainder = bufferB.Length % Vector<byte>.Count;
            var newLength = bufferB.Length - remainder;
            if (newLength != 0)
            {
                var src = MemoryMarshal.Cast<byte, Vector<byte>>(bufferB);
                var dst = MemoryMarshal.Cast<byte, Vector<byte>>(bufferA).Slice(0, src.Length);
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] -= src[i];
                }
            }
            if (remainder != 0)
            {
                var srcRem = bufferB.Slice(newLength);
                var dstRem = bufferA.Slice(newLength).Slice(0, srcRem.Length);
                for (int i = 0; i < srcRem.Length; i++)
                {
                    dstRem[i] -= srcRem[i];
                }
            }
		}
			/// <summary>
		/// Adds values from <paramref name="bufferB"/> to <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA">The destination <see cref="Span{T}"/></param>
		/// <param name="bufferB"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void AddArray(Span<float> bufferA, ReadOnlySpan<float> bufferB)
		{
			if (bufferB.Length > bufferA.Length) throw new ArgumentException("Insufficient buffer.", nameof(bufferA));
            var remainder = bufferB.Length % Vector<float>.Count;
            var newLength = bufferB.Length - remainder;
            if (newLength != 0)
            {
                var src = MemoryMarshal.Cast<float, Vector<float>>(bufferB);
                var dst = MemoryMarshal.Cast<float, Vector<float>>(bufferA).Slice(0, src.Length);
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] += src[i];
                }
            }
            if (remainder != 0)
            {
                var srcRem = bufferB.Slice(newLength);
                var dstRem = bufferA.Slice(newLength).Slice(0, srcRem.Length);
                for (int i = 0; i < srcRem.Length; i++)
                {
                    dstRem[i] += srcRem[i];
                }
            }
		}
		/// <summary>
		/// Subtracts <paramref name="bufferB"/> from <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="bufferB"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SubtractArray(Span<float> bufferA, ReadOnlySpan<float> bufferB)
		{
			if (bufferB.Length > bufferA.Length) throw new ArgumentException("Insufficient buffer.", nameof(bufferA));
            var remainder = bufferB.Length % Vector<float>.Count;
            var newLength = bufferB.Length - remainder;
            if (newLength != 0)
            {
                var src = MemoryMarshal.Cast<float, Vector<float>>(bufferB);
                var dst = MemoryMarshal.Cast<float, Vector<float>>(bufferA).Slice(0, src.Length);
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] -= src[i];
                }
            }
            if (remainder != 0)
            {
                var srcRem = bufferB.Slice(newLength);
                var dstRem = bufferA.Slice(newLength).Slice(0, srcRem.Length);
                for (int i = 0; i < srcRem.Length; i++)
                {
                    dstRem[i] -= srcRem[i];
                }
            }
		}
			/// <summary>
		/// Adds values from <paramref name="bufferB"/> to <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA">The destination <see cref="Span{T}"/></param>
		/// <param name="bufferB"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void AddArray(Span<double> bufferA, ReadOnlySpan<double> bufferB)
		{
			if (bufferB.Length > bufferA.Length) throw new ArgumentException("Insufficient buffer.", nameof(bufferA));
            var remainder = bufferB.Length % Vector<double>.Count;
            var newLength = bufferB.Length - remainder;
            if (newLength != 0)
            {
                var src = MemoryMarshal.Cast<double, Vector<double>>(bufferB);
                var dst = MemoryMarshal.Cast<double, Vector<double>>(bufferA).Slice(0, src.Length);
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] += src[i];
                }
            }
            if (remainder != 0)
            {
                var srcRem = bufferB.Slice(newLength);
                var dstRem = bufferA.Slice(newLength).Slice(0, srcRem.Length);
                for (int i = 0; i < srcRem.Length; i++)
                {
                    dstRem[i] += srcRem[i];
                }
            }
		}
		/// <summary>
		/// Subtracts <paramref name="bufferB"/> from <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="bufferB"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SubtractArray(Span<double> bufferA, ReadOnlySpan<double> bufferB)
		{
			if (bufferB.Length > bufferA.Length) throw new ArgumentException("Insufficient buffer.", nameof(bufferA));
            var remainder = bufferB.Length % Vector<double>.Count;
            var newLength = bufferB.Length - remainder;
            if (newLength != 0)
            {
                var src = MemoryMarshal.Cast<double, Vector<double>>(bufferB);
                var dst = MemoryMarshal.Cast<double, Vector<double>>(bufferA).Slice(0, src.Length);
                for (int i = 0; i < src.Length; i++)
                {
                    dst[i] -= src[i];
                }
            }
            if (remainder != 0)
            {
                var srcRem = bufferB.Slice(newLength);
                var dstRem = bufferA.Slice(newLength).Slice(0, srcRem.Length);
                for (int i = 0; i < srcRem.Length; i++)
                {
                    dstRem[i] -= srcRem[i];
                }
            }
		}
		
		/// <summary>
		/// Negates the specified <paramref name="span"/>.
		/// </summary>
		/// <param name="span">The region to Negate.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void NegateArray(this Span<int> span)
		{
			if (Vector<int>.Count > span.Length)
            {
                for (int i = 0; i < span.Length; i++)
                {
                    span[i] = -span[i];
                }
            }
            else
            {
                var spanV = MemoryMarshal.Cast<int, Vector<int>>(span);
                for (int i = 0; i < spanV.Length; i++)
                {
                    spanV[i] = -spanV[i];
                }
                var spanR = span.Slice(spanV.Length * Vector<int>.Count);
                for (int i = 0; i < spanR.Length; i++)
                {
                    spanR[i] = -spanR[i];
                }
            }
		}
	
		/// <summary>
		/// Negates the specified <paramref name="span"/>.
		/// </summary>
		/// <param name="span">The region to Negate.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void NegateArray(this Span<long> span)
		{
			if (Vector<long>.Count > span.Length)
            {
                for (int i = 0; i < span.Length; i++)
                {
                    span[i] = -span[i];
                }
            }
            else
            {
                var spanV = MemoryMarshal.Cast<long, Vector<long>>(span);
                for (int i = 0; i < spanV.Length; i++)
                {
                    spanV[i] = -spanV[i];
                }
                var spanR = span.Slice(spanV.Length * Vector<long>.Count);
                for (int i = 0; i < spanR.Length; i++)
                {
                    spanR[i] = -spanR[i];
                }
            }
		}
	
		/// <summary>
		/// Negates the specified <paramref name="span"/>.
		/// </summary>
		/// <param name="span">The region to Negate.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void NegateArray(this Span<float> span)
		{
			if (Vector<float>.Count > span.Length)
            {
                for (int i = 0; i < span.Length; i++)
                {
                    span[i] = -span[i];
                }
            }
            else
            {
                var spanV = MemoryMarshal.Cast<float, Vector<float>>(span);
                for (int i = 0; i < spanV.Length; i++)
                {
                    spanV[i] = -spanV[i];
                }
                var spanR = span.Slice(spanV.Length * Vector<float>.Count);
                for (int i = 0; i < spanR.Length; i++)
                {
                    spanR[i] = -spanR[i];
                }
            }
		}
	
		/// <summary>
		/// Negates the specified <paramref name="span"/>.
		/// </summary>
		/// <param name="span">The region to Negate.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void NegateArray(this Span<double> span)
		{
			if (Vector<double>.Count > span.Length)
            {
                for (int i = 0; i < span.Length; i++)
                {
                    span[i] = -span[i];
                }
            }
            else
            {
                var spanV = MemoryMarshal.Cast<double, Vector<double>>(span);
                for (int i = 0; i < spanV.Length; i++)
                {
                    spanV[i] = -spanV[i];
                }
                var spanR = span.Slice(spanV.Length * Vector<double>.Count);
                for (int i = 0; i < spanR.Length; i++)
                {
                    spanR[i] = -spanR[i];
                }
            }
		}
		
		/// <summary>
		/// Negates the specified <paramref name="span"/>.
		/// </summary>
		/// <param name="span">The region to Negate.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void NegateArray(this Span<short> span)
		{
			if (Vector<short>.Count > span.Length)
            {
                for (int i = 0; i < span.Length; i++)
                {
                    span[i] = (short)-span[i];
                }
            }
            else
            {
                var spanV = MemoryMarshal.Cast<short, Vector<short>>(span);
                for (int i = 0; i < spanV.Length; i++)
                {
                    spanV[i] = -spanV[i];
                }
                var spanR = span.Slice(spanV.Length * Vector<short>.Count);
                for (int i = 0; i < spanR.Length; i++)
                {
                    spanR[i] = (short)-spanR[i];
                }
            }
		}
	
		/// <summary>
		/// Negates the specified <paramref name="span"/>.
		/// </summary>
		/// <param name="span">The region to Negate.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void NegateArray(this Span<sbyte> span)
		{
			if (Vector<sbyte>.Count > span.Length)
            {
                for (int i = 0; i < span.Length; i++)
                {
                    span[i] = (sbyte)-span[i];
                }
            }
            else
            {
                var spanV = MemoryMarshal.Cast<sbyte, Vector<sbyte>>(span);
                for (int i = 0; i < spanV.Length; i++)
                {
                    spanV[i] = -spanV[i];
                }
                var spanR = span.Slice(spanV.Length * Vector<sbyte>.Count);
                for (int i = 0; i < spanR.Length; i++)
                {
                    spanR[i] = (sbyte)-spanR[i];
                }
            }
		}
		}
}
