using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
namespace CSCodec{
	public static partial class ArrayCalculationExtensions
	{
	
		/// <summary>
		/// Scales <paramref name="buffer"/> with <paramref name="scale"/>.
		/// </summary>
		/// <param name="buffer">The array to scale.</param>
		/// <param name="offset">The offset to scale.</param>
		/// <param name="count">The count of element.</param>
		/// <param name="scale">The scale.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void ScaleArray(this System.Int32[] buffer, int offset, int count, System.Int32 scale)
		{
			int procCountFinal = count % Vector<System.Int32>.Count;
			int procCountSIMD = count - procCountFinal;
			int i;
			for (i = offset; i < offset + procCountSIMD; i += Vector<System.Int32>.Count)
			{
				(new Vector<System.Int32>(buffer, i) * scale).CopyTo(buffer, i);
			}
			for (i = offset + procCountSIMD; i < offset + count; i++)
			{
				buffer[i] *= scale;
			}
		}
	
		/// <summary>
		/// Scales <paramref name="buffer"/> with <paramref name="scale"/>.
		/// </summary>
		/// <param name="buffer">The array to scale.</param>
		/// <param name="offset">The offset to scale.</param>
		/// <param name="count">The count of element.</param>
		/// <param name="scale">The scale.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void ScaleArray(this System.Int16[] buffer, int offset, int count, System.Int16 scale)
		{
			int procCountFinal = count % Vector<System.Int16>.Count;
			int procCountSIMD = count - procCountFinal;
			int i;
			for (i = offset; i < offset + procCountSIMD; i += Vector<System.Int16>.Count)
			{
				(new Vector<System.Int16>(buffer, i) * scale).CopyTo(buffer, i);
			}
			for (i = offset + procCountSIMD; i < offset + count; i++)
			{
				buffer[i] *= scale;
			}
		}
	
		/// <summary>
		/// Scales <paramref name="buffer"/> with <paramref name="scale"/>.
		/// </summary>
		/// <param name="buffer">The array to scale.</param>
		/// <param name="offset">The offset to scale.</param>
		/// <param name="count">The count of element.</param>
		/// <param name="scale">The scale.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void ScaleArray(this System.Single[] buffer, int offset, int count, System.Single scale)
		{
			int procCountFinal = count % Vector<System.Single>.Count;
			int procCountSIMD = count - procCountFinal;
			int i;
			for (i = offset; i < offset + procCountSIMD; i += Vector<System.Single>.Count)
			{
				(new Vector<System.Single>(buffer, i) * scale).CopyTo(buffer, i);
			}
			for (i = offset + procCountSIMD; i < offset + count; i++)
			{
				buffer[i] *= scale;
			}
		}
	
		/// <summary>
		/// Scales <paramref name="buffer"/> with <paramref name="scale"/>.
		/// </summary>
		/// <param name="buffer">The array to scale.</param>
		/// <param name="offset">The offset to scale.</param>
		/// <param name="count">The count of element.</param>
		/// <param name="scale">The scale.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void ScaleArray(this System.Double[] buffer, int offset, int count, System.Double scale)
		{
			int procCountFinal = count % Vector<System.Double>.Count;
			int procCountSIMD = count - procCountFinal;
			int i;
			for (i = offset; i < offset + procCountSIMD; i += Vector<System.Double>.Count)
			{
				(new Vector<System.Double>(buffer, i) * scale).CopyTo(buffer, i);
			}
			for (i = offset + procCountSIMD; i < offset + count; i++)
			{
				buffer[i] *= scale;
			}
		}
				/// <summary>
		/// Adds values from <paramref name="bufferB"/> to <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="offsetA"></param>
		/// <param name="bufferB"></param>
		/// <param name="offsetB"></param>
		/// <param name="count"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void AddArray(System.Int32[] bufferA, int offsetA, System.Int32[] bufferB, int offsetB, int count)
		{
			if (offsetA + count > bufferA.Length || offsetB + count > bufferB.Length) throw new ArgumentException("Insufficient buffer.", nameof(count));
			int procCountFinal = count % Vector<System.Int32>.Count;
			int procCountSIMD = count - procCountFinal;
			int i = offsetA;
			int j = offsetB;
			do
			{
				(new Vector<System.Int32>(bufferA, i) + new Vector<System.Int32>(bufferB, j)).CopyTo(bufferA, i);
				i += Vector<System.Int32>.Count;
				j += Vector<System.Int32>.Count;
			} while (i < offsetA + procCountSIMD && j < offsetB + procCountSIMD);
			for (int k = 0; k < procCountFinal; k++)
			{
				bufferA[i + k] += bufferB[j + k];
			}
		}
		/// <summary>
		/// Subtracts values from <paramref name="bufferB"/> from <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="offsetA"></param>
		/// <param name="bufferB"></param>
		/// <param name="offsetB"></param>
		/// <param name="count"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SubtractArray(System.Int32[] bufferA, int offsetA, System.Int32[] bufferB, int offsetB, int count)
		{
			if (offsetA + count > bufferA.Length || offsetB + count > bufferB.Length) throw new ArgumentException("Insufficient buffer.", nameof(count));
			int procCountFinal = count % Vector<System.Int32>.Count;
			int procCountSIMD = count - procCountFinal;
			int i = offsetA;
			int j = offsetB;
			do
			{
				(new Vector<System.Int32>(bufferA, i) - new Vector<System.Int32>(bufferB, j)).CopyTo(bufferA, i);
				i += Vector<System.Int32>.Count;
				j += Vector<System.Int32>.Count;
			} while (i < offsetA + procCountSIMD && j < offsetB + procCountSIMD);
			for (int k = 0; k < procCountFinal; k++)
			{
				bufferA[i + k] -= bufferB[j + k];
			}
		}
			/// <summary>
		/// Adds values from <paramref name="bufferB"/> to <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="offsetA"></param>
		/// <param name="bufferB"></param>
		/// <param name="offsetB"></param>
		/// <param name="count"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void AddArray(System.UInt32[] bufferA, int offsetA, System.UInt32[] bufferB, int offsetB, int count)
		{
			if (offsetA + count > bufferA.Length || offsetB + count > bufferB.Length) throw new ArgumentException("Insufficient buffer.", nameof(count));
			int procCountFinal = count % Vector<System.UInt32>.Count;
			int procCountSIMD = count - procCountFinal;
			int i = offsetA;
			int j = offsetB;
			do
			{
				(new Vector<System.UInt32>(bufferA, i) + new Vector<System.UInt32>(bufferB, j)).CopyTo(bufferA, i);
				i += Vector<System.UInt32>.Count;
				j += Vector<System.UInt32>.Count;
			} while (i < offsetA + procCountSIMD && j < offsetB + procCountSIMD);
			for (int k = 0; k < procCountFinal; k++)
			{
				bufferA[i + k] += bufferB[j + k];
			}
		}
		/// <summary>
		/// Subtracts values from <paramref name="bufferB"/> from <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="offsetA"></param>
		/// <param name="bufferB"></param>
		/// <param name="offsetB"></param>
		/// <param name="count"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SubtractArray(System.UInt32[] bufferA, int offsetA, System.UInt32[] bufferB, int offsetB, int count)
		{
			if (offsetA + count > bufferA.Length || offsetB + count > bufferB.Length) throw new ArgumentException("Insufficient buffer.", nameof(count));
			int procCountFinal = count % Vector<System.UInt32>.Count;
			int procCountSIMD = count - procCountFinal;
			int i = offsetA;
			int j = offsetB;
			do
			{
				(new Vector<System.UInt32>(bufferA, i) - new Vector<System.UInt32>(bufferB, j)).CopyTo(bufferA, i);
				i += Vector<System.UInt32>.Count;
				j += Vector<System.UInt32>.Count;
			} while (i < offsetA + procCountSIMD && j < offsetB + procCountSIMD);
			for (int k = 0; k < procCountFinal; k++)
			{
				bufferA[i + k] -= bufferB[j + k];
			}
		}
			/// <summary>
		/// Adds values from <paramref name="bufferB"/> to <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="offsetA"></param>
		/// <param name="bufferB"></param>
		/// <param name="offsetB"></param>
		/// <param name="count"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void AddArray(System.Int64[] bufferA, int offsetA, System.Int64[] bufferB, int offsetB, int count)
		{
			if (offsetA + count > bufferA.Length || offsetB + count > bufferB.Length) throw new ArgumentException("Insufficient buffer.", nameof(count));
			int procCountFinal = count % Vector<System.Int64>.Count;
			int procCountSIMD = count - procCountFinal;
			int i = offsetA;
			int j = offsetB;
			do
			{
				(new Vector<System.Int64>(bufferA, i) + new Vector<System.Int64>(bufferB, j)).CopyTo(bufferA, i);
				i += Vector<System.Int64>.Count;
				j += Vector<System.Int64>.Count;
			} while (i < offsetA + procCountSIMD && j < offsetB + procCountSIMD);
			for (int k = 0; k < procCountFinal; k++)
			{
				bufferA[i + k] += bufferB[j + k];
			}
		}
		/// <summary>
		/// Subtracts values from <paramref name="bufferB"/> from <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="offsetA"></param>
		/// <param name="bufferB"></param>
		/// <param name="offsetB"></param>
		/// <param name="count"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SubtractArray(System.Int64[] bufferA, int offsetA, System.Int64[] bufferB, int offsetB, int count)
		{
			if (offsetA + count > bufferA.Length || offsetB + count > bufferB.Length) throw new ArgumentException("Insufficient buffer.", nameof(count));
			int procCountFinal = count % Vector<System.Int64>.Count;
			int procCountSIMD = count - procCountFinal;
			int i = offsetA;
			int j = offsetB;
			do
			{
				(new Vector<System.Int64>(bufferA, i) - new Vector<System.Int64>(bufferB, j)).CopyTo(bufferA, i);
				i += Vector<System.Int64>.Count;
				j += Vector<System.Int64>.Count;
			} while (i < offsetA + procCountSIMD && j < offsetB + procCountSIMD);
			for (int k = 0; k < procCountFinal; k++)
			{
				bufferA[i + k] -= bufferB[j + k];
			}
		}
			/// <summary>
		/// Adds values from <paramref name="bufferB"/> to <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="offsetA"></param>
		/// <param name="bufferB"></param>
		/// <param name="offsetB"></param>
		/// <param name="count"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void AddArray(System.UInt64[] bufferA, int offsetA, System.UInt64[] bufferB, int offsetB, int count)
		{
			if (offsetA + count > bufferA.Length || offsetB + count > bufferB.Length) throw new ArgumentException("Insufficient buffer.", nameof(count));
			int procCountFinal = count % Vector<System.UInt64>.Count;
			int procCountSIMD = count - procCountFinal;
			int i = offsetA;
			int j = offsetB;
			do
			{
				(new Vector<System.UInt64>(bufferA, i) + new Vector<System.UInt64>(bufferB, j)).CopyTo(bufferA, i);
				i += Vector<System.UInt64>.Count;
				j += Vector<System.UInt64>.Count;
			} while (i < offsetA + procCountSIMD && j < offsetB + procCountSIMD);
			for (int k = 0; k < procCountFinal; k++)
			{
				bufferA[i + k] += bufferB[j + k];
			}
		}
		/// <summary>
		/// Subtracts values from <paramref name="bufferB"/> from <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="offsetA"></param>
		/// <param name="bufferB"></param>
		/// <param name="offsetB"></param>
		/// <param name="count"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SubtractArray(System.UInt64[] bufferA, int offsetA, System.UInt64[] bufferB, int offsetB, int count)
		{
			if (offsetA + count > bufferA.Length || offsetB + count > bufferB.Length) throw new ArgumentException("Insufficient buffer.", nameof(count));
			int procCountFinal = count % Vector<System.UInt64>.Count;
			int procCountSIMD = count - procCountFinal;
			int i = offsetA;
			int j = offsetB;
			do
			{
				(new Vector<System.UInt64>(bufferA, i) - new Vector<System.UInt64>(bufferB, j)).CopyTo(bufferA, i);
				i += Vector<System.UInt64>.Count;
				j += Vector<System.UInt64>.Count;
			} while (i < offsetA + procCountSIMD && j < offsetB + procCountSIMD);
			for (int k = 0; k < procCountFinal; k++)
			{
				bufferA[i + k] -= bufferB[j + k];
			}
		}
			/// <summary>
		/// Adds values from <paramref name="bufferB"/> to <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="offsetA"></param>
		/// <param name="bufferB"></param>
		/// <param name="offsetB"></param>
		/// <param name="count"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void AddArray(System.Int16[] bufferA, int offsetA, System.Int16[] bufferB, int offsetB, int count)
		{
			if (offsetA + count > bufferA.Length || offsetB + count > bufferB.Length) throw new ArgumentException("Insufficient buffer.", nameof(count));
			int procCountFinal = count % Vector<System.Int16>.Count;
			int procCountSIMD = count - procCountFinal;
			int i = offsetA;
			int j = offsetB;
			do
			{
				(new Vector<System.Int16>(bufferA, i) + new Vector<System.Int16>(bufferB, j)).CopyTo(bufferA, i);
				i += Vector<System.Int16>.Count;
				j += Vector<System.Int16>.Count;
			} while (i < offsetA + procCountSIMD && j < offsetB + procCountSIMD);
			for (int k = 0; k < procCountFinal; k++)
			{
				bufferA[i + k] += bufferB[j + k];
			}
		}
		/// <summary>
		/// Subtracts values from <paramref name="bufferB"/> from <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="offsetA"></param>
		/// <param name="bufferB"></param>
		/// <param name="offsetB"></param>
		/// <param name="count"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SubtractArray(System.Int16[] bufferA, int offsetA, System.Int16[] bufferB, int offsetB, int count)
		{
			if (offsetA + count > bufferA.Length || offsetB + count > bufferB.Length) throw new ArgumentException("Insufficient buffer.", nameof(count));
			int procCountFinal = count % Vector<System.Int16>.Count;
			int procCountSIMD = count - procCountFinal;
			int i = offsetA;
			int j = offsetB;
			do
			{
				(new Vector<System.Int16>(bufferA, i) - new Vector<System.Int16>(bufferB, j)).CopyTo(bufferA, i);
				i += Vector<System.Int16>.Count;
				j += Vector<System.Int16>.Count;
			} while (i < offsetA + procCountSIMD && j < offsetB + procCountSIMD);
			for (int k = 0; k < procCountFinal; k++)
			{
				bufferA[i + k] -= bufferB[j + k];
			}
		}
			/// <summary>
		/// Adds values from <paramref name="bufferB"/> to <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="offsetA"></param>
		/// <param name="bufferB"></param>
		/// <param name="offsetB"></param>
		/// <param name="count"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void AddArray(System.UInt16[] bufferA, int offsetA, System.UInt16[] bufferB, int offsetB, int count)
		{
			if (offsetA + count > bufferA.Length || offsetB + count > bufferB.Length) throw new ArgumentException("Insufficient buffer.", nameof(count));
			int procCountFinal = count % Vector<System.UInt16>.Count;
			int procCountSIMD = count - procCountFinal;
			int i = offsetA;
			int j = offsetB;
			do
			{
				(new Vector<System.UInt16>(bufferA, i) + new Vector<System.UInt16>(bufferB, j)).CopyTo(bufferA, i);
				i += Vector<System.UInt16>.Count;
				j += Vector<System.UInt16>.Count;
			} while (i < offsetA + procCountSIMD && j < offsetB + procCountSIMD);
			for (int k = 0; k < procCountFinal; k++)
			{
				bufferA[i + k] += bufferB[j + k];
			}
		}
		/// <summary>
		/// Subtracts values from <paramref name="bufferB"/> from <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="offsetA"></param>
		/// <param name="bufferB"></param>
		/// <param name="offsetB"></param>
		/// <param name="count"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SubtractArray(System.UInt16[] bufferA, int offsetA, System.UInt16[] bufferB, int offsetB, int count)
		{
			if (offsetA + count > bufferA.Length || offsetB + count > bufferB.Length) throw new ArgumentException("Insufficient buffer.", nameof(count));
			int procCountFinal = count % Vector<System.UInt16>.Count;
			int procCountSIMD = count - procCountFinal;
			int i = offsetA;
			int j = offsetB;
			do
			{
				(new Vector<System.UInt16>(bufferA, i) - new Vector<System.UInt16>(bufferB, j)).CopyTo(bufferA, i);
				i += Vector<System.UInt16>.Count;
				j += Vector<System.UInt16>.Count;
			} while (i < offsetA + procCountSIMD && j < offsetB + procCountSIMD);
			for (int k = 0; k < procCountFinal; k++)
			{
				bufferA[i + k] -= bufferB[j + k];
			}
		}
			/// <summary>
		/// Adds values from <paramref name="bufferB"/> to <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="offsetA"></param>
		/// <param name="bufferB"></param>
		/// <param name="offsetB"></param>
		/// <param name="count"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void AddArray(System.SByte[] bufferA, int offsetA, System.SByte[] bufferB, int offsetB, int count)
		{
			if (offsetA + count > bufferA.Length || offsetB + count > bufferB.Length) throw new ArgumentException("Insufficient buffer.", nameof(count));
			int procCountFinal = count % Vector<System.SByte>.Count;
			int procCountSIMD = count - procCountFinal;
			int i = offsetA;
			int j = offsetB;
			do
			{
				(new Vector<System.SByte>(bufferA, i) + new Vector<System.SByte>(bufferB, j)).CopyTo(bufferA, i);
				i += Vector<System.SByte>.Count;
				j += Vector<System.SByte>.Count;
			} while (i < offsetA + procCountSIMD && j < offsetB + procCountSIMD);
			for (int k = 0; k < procCountFinal; k++)
			{
				bufferA[i + k] += bufferB[j + k];
			}
		}
		/// <summary>
		/// Subtracts values from <paramref name="bufferB"/> from <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="offsetA"></param>
		/// <param name="bufferB"></param>
		/// <param name="offsetB"></param>
		/// <param name="count"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SubtractArray(System.SByte[] bufferA, int offsetA, System.SByte[] bufferB, int offsetB, int count)
		{
			if (offsetA + count > bufferA.Length || offsetB + count > bufferB.Length) throw new ArgumentException("Insufficient buffer.", nameof(count));
			int procCountFinal = count % Vector<System.SByte>.Count;
			int procCountSIMD = count - procCountFinal;
			int i = offsetA;
			int j = offsetB;
			do
			{
				(new Vector<System.SByte>(bufferA, i) - new Vector<System.SByte>(bufferB, j)).CopyTo(bufferA, i);
				i += Vector<System.SByte>.Count;
				j += Vector<System.SByte>.Count;
			} while (i < offsetA + procCountSIMD && j < offsetB + procCountSIMD);
			for (int k = 0; k < procCountFinal; k++)
			{
				bufferA[i + k] -= bufferB[j + k];
			}
		}
			/// <summary>
		/// Adds values from <paramref name="bufferB"/> to <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="offsetA"></param>
		/// <param name="bufferB"></param>
		/// <param name="offsetB"></param>
		/// <param name="count"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void AddArray(System.Byte[] bufferA, int offsetA, System.Byte[] bufferB, int offsetB, int count)
		{
			if (offsetA + count > bufferA.Length || offsetB + count > bufferB.Length) throw new ArgumentException("Insufficient buffer.", nameof(count));
			int procCountFinal = count % Vector<System.Byte>.Count;
			int procCountSIMD = count - procCountFinal;
			int i = offsetA;
			int j = offsetB;
			do
			{
				(new Vector<System.Byte>(bufferA, i) + new Vector<System.Byte>(bufferB, j)).CopyTo(bufferA, i);
				i += Vector<System.Byte>.Count;
				j += Vector<System.Byte>.Count;
			} while (i < offsetA + procCountSIMD && j < offsetB + procCountSIMD);
			for (int k = 0; k < procCountFinal; k++)
			{
				bufferA[i + k] += bufferB[j + k];
			}
		}
		/// <summary>
		/// Subtracts values from <paramref name="bufferB"/> from <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="offsetA"></param>
		/// <param name="bufferB"></param>
		/// <param name="offsetB"></param>
		/// <param name="count"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SubtractArray(System.Byte[] bufferA, int offsetA, System.Byte[] bufferB, int offsetB, int count)
		{
			if (offsetA + count > bufferA.Length || offsetB + count > bufferB.Length) throw new ArgumentException("Insufficient buffer.", nameof(count));
			int procCountFinal = count % Vector<System.Byte>.Count;
			int procCountSIMD = count - procCountFinal;
			int i = offsetA;
			int j = offsetB;
			do
			{
				(new Vector<System.Byte>(bufferA, i) - new Vector<System.Byte>(bufferB, j)).CopyTo(bufferA, i);
				i += Vector<System.Byte>.Count;
				j += Vector<System.Byte>.Count;
			} while (i < offsetA + procCountSIMD && j < offsetB + procCountSIMD);
			for (int k = 0; k < procCountFinal; k++)
			{
				bufferA[i + k] -= bufferB[j + k];
			}
		}
			/// <summary>
		/// Adds values from <paramref name="bufferB"/> to <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="offsetA"></param>
		/// <param name="bufferB"></param>
		/// <param name="offsetB"></param>
		/// <param name="count"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void AddArray(System.Single[] bufferA, int offsetA, System.Single[] bufferB, int offsetB, int count)
		{
			if (offsetA + count > bufferA.Length || offsetB + count > bufferB.Length) throw new ArgumentException("Insufficient buffer.", nameof(count));
			int procCountFinal = count % Vector<System.Single>.Count;
			int procCountSIMD = count - procCountFinal;
			int i = offsetA;
			int j = offsetB;
			do
			{
				(new Vector<System.Single>(bufferA, i) + new Vector<System.Single>(bufferB, j)).CopyTo(bufferA, i);
				i += Vector<System.Single>.Count;
				j += Vector<System.Single>.Count;
			} while (i < offsetA + procCountSIMD && j < offsetB + procCountSIMD);
			for (int k = 0; k < procCountFinal; k++)
			{
				bufferA[i + k] += bufferB[j + k];
			}
		}
		/// <summary>
		/// Subtracts values from <paramref name="bufferB"/> from <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="offsetA"></param>
		/// <param name="bufferB"></param>
		/// <param name="offsetB"></param>
		/// <param name="count"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SubtractArray(System.Single[] bufferA, int offsetA, System.Single[] bufferB, int offsetB, int count)
		{
			if (offsetA + count > bufferA.Length || offsetB + count > bufferB.Length) throw new ArgumentException("Insufficient buffer.", nameof(count));
			int procCountFinal = count % Vector<System.Single>.Count;
			int procCountSIMD = count - procCountFinal;
			int i = offsetA;
			int j = offsetB;
			do
			{
				(new Vector<System.Single>(bufferA, i) - new Vector<System.Single>(bufferB, j)).CopyTo(bufferA, i);
				i += Vector<System.Single>.Count;
				j += Vector<System.Single>.Count;
			} while (i < offsetA + procCountSIMD && j < offsetB + procCountSIMD);
			for (int k = 0; k < procCountFinal; k++)
			{
				bufferA[i + k] -= bufferB[j + k];
			}
		}
			/// <summary>
		/// Adds values from <paramref name="bufferB"/> to <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="offsetA"></param>
		/// <param name="bufferB"></param>
		/// <param name="offsetB"></param>
		/// <param name="count"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void AddArray(System.Double[] bufferA, int offsetA, System.Double[] bufferB, int offsetB, int count)
		{
			if (offsetA + count > bufferA.Length || offsetB + count > bufferB.Length) throw new ArgumentException("Insufficient buffer.", nameof(count));
			int procCountFinal = count % Vector<System.Double>.Count;
			int procCountSIMD = count - procCountFinal;
			int i = offsetA;
			int j = offsetB;
			do
			{
				(new Vector<System.Double>(bufferA, i) + new Vector<System.Double>(bufferB, j)).CopyTo(bufferA, i);
				i += Vector<System.Double>.Count;
				j += Vector<System.Double>.Count;
			} while (i < offsetA + procCountSIMD && j < offsetB + procCountSIMD);
			for (int k = 0; k < procCountFinal; k++)
			{
				bufferA[i + k] += bufferB[j + k];
			}
		}
		/// <summary>
		/// Subtracts values from <paramref name="bufferB"/> from <paramref name="bufferA"/>.
		/// <paramref name="bufferA"/> will be overwritten.
		/// </summary>
		/// <param name="bufferA"></param>
		/// <param name="offsetA"></param>
		/// <param name="bufferB"></param>
		/// <param name="offsetB"></param>
		/// <param name="count"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void SubtractArray(System.Double[] bufferA, int offsetA, System.Double[] bufferB, int offsetB, int count)
		{
			if (offsetA + count > bufferA.Length || offsetB + count > bufferB.Length) throw new ArgumentException("Insufficient buffer.", nameof(count));
			int procCountFinal = count % Vector<System.Double>.Count;
			int procCountSIMD = count - procCountFinal;
			int i = offsetA;
			int j = offsetB;
			do
			{
				(new Vector<System.Double>(bufferA, i) - new Vector<System.Double>(bufferB, j)).CopyTo(bufferA, i);
				i += Vector<System.Double>.Count;
				j += Vector<System.Double>.Count;
			} while (i < offsetA + procCountSIMD && j < offsetB + procCountSIMD);
			for (int k = 0; k < procCountFinal; k++)
			{
				bufferA[i + k] -= bufferB[j + k];
			}
		}
		
		/// <summary>
		/// Negates the specified <paramref name="buffer"/>.
		/// </summary>
		/// <param name="buffer">The array to Negate.</param>
		/// <param name="offset">The offset to Negate.</param>
		/// <param name="count">The count of element.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void NegateArray(this System.Int32[] buffer, int offset, int count)
		{
			int procCountFinal = count % Vector<System.Int32>.Count;
			int procCountSIMD = count - procCountFinal;
			int i;
			for (i = offset; i < offset + procCountSIMD; i += Vector<System.Int32>.Count)
			{
				(-(new Vector<System.Int32>(buffer, i))).CopyTo(buffer, i);
			}
			for (i = offset + procCountSIMD; i < offset + count; i++)
			{
				buffer[i] = -buffer[i];
			}
		}
	
		/// <summary>
		/// Negates the specified <paramref name="buffer"/>.
		/// </summary>
		/// <param name="buffer">The array to Negate.</param>
		/// <param name="offset">The offset to Negate.</param>
		/// <param name="count">The count of element.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void NegateArray(this System.Int64[] buffer, int offset, int count)
		{
			int procCountFinal = count % Vector<System.Int64>.Count;
			int procCountSIMD = count - procCountFinal;
			int i;
			for (i = offset; i < offset + procCountSIMD; i += Vector<System.Int64>.Count)
			{
				(-(new Vector<System.Int64>(buffer, i))).CopyTo(buffer, i);
			}
			for (i = offset + procCountSIMD; i < offset + count; i++)
			{
				buffer[i] = -buffer[i];
			}
		}
	
		/// <summary>
		/// Negates the specified <paramref name="buffer"/>.
		/// </summary>
		/// <param name="buffer">The array to Negate.</param>
		/// <param name="offset">The offset to Negate.</param>
		/// <param name="count">The count of element.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void NegateArray(this System.Single[] buffer, int offset, int count)
		{
			int procCountFinal = count % Vector<System.Single>.Count;
			int procCountSIMD = count - procCountFinal;
			int i;
			for (i = offset; i < offset + procCountSIMD; i += Vector<System.Single>.Count)
			{
				(-(new Vector<System.Single>(buffer, i))).CopyTo(buffer, i);
			}
			for (i = offset + procCountSIMD; i < offset + count; i++)
			{
				buffer[i] = -buffer[i];
			}
		}
	
		/// <summary>
		/// Negates the specified <paramref name="buffer"/>.
		/// </summary>
		/// <param name="buffer">The array to Negate.</param>
		/// <param name="offset">The offset to Negate.</param>
		/// <param name="count">The count of element.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void NegateArray(this System.Double[] buffer, int offset, int count)
		{
			int procCountFinal = count % Vector<System.Double>.Count;
			int procCountSIMD = count - procCountFinal;
			int i;
			for (i = offset; i < offset + procCountSIMD; i += Vector<System.Double>.Count)
			{
				(-(new Vector<System.Double>(buffer, i))).CopyTo(buffer, i);
			}
			for (i = offset + procCountSIMD; i < offset + count; i++)
			{
				buffer[i] = -buffer[i];
			}
		}
		
		/// <summary>
		/// Negates the specified <paramref name="buffer"/>.
		/// </summary>
		/// <param name="buffer">The array to Negate.</param>
		/// <param name="offset">The offset to Negate.</param>
		/// <param name="count">The count of element.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void NegateArray(this System.Int16[] buffer, int offset, int count)
		{
			int procCountFinal = count % Vector<System.Int16>.Count;
			int procCountSIMD = count - procCountFinal;
			int i;
			for (i = offset; i < offset + procCountSIMD; i += Vector<System.Int16>.Count)
			{
				(-(new Vector<System.Int16>(buffer, i))).CopyTo(buffer, i);
			}
			for (i = offset + procCountSIMD; i < offset + count; i++)
			{
				buffer[i] = (System.Int16)(-buffer[i]);
			}
		}
	
		/// <summary>
		/// Negates the specified <paramref name="buffer"/>.
		/// </summary>
		/// <param name="buffer">The array to Negate.</param>
		/// <param name="offset">The offset to Negate.</param>
		/// <param name="count">The count of element.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[DebuggerNonUserCode()]
		public static void NegateArray(this System.SByte[] buffer, int offset, int count)
		{
			int procCountFinal = count % Vector<System.SByte>.Count;
			int procCountSIMD = count - procCountFinal;
			int i;
			for (i = offset; i < offset + procCountSIMD; i += Vector<System.SByte>.Count)
			{
				(-(new Vector<System.SByte>(buffer, i))).CopyTo(buffer, i);
			}
			for (i = offset + procCountSIMD; i < offset + count; i++)
			{
				buffer[i] = (System.SByte)(-buffer[i]);
			}
		}
		}
}
