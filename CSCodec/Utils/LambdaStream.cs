using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSCodec.Utils
{
	/// <summary>
	/// Lambda-Expression Specified Stream
	/// </summary>
	/// <seealso cref="Stream" />
	public sealed partial class LambdaStream : Stream
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LambdaStream"/> class.
		/// </summary>
		/// <param name="readFunction">The read function.</param>
		/// <param name="seekFunction">The seek function.</param>
		/// <param name="flushFunction">The flush function.</param>
		/// <param name="writeFunction">The write function.</param>
		/// <param name="lengthGetFunction">The length get function.</param>
		/// <param name="setLengthFunction">The set length function.</param>
		/// <param name="positionGetFunction">The position get function.</param>
		/// <param name="positionSetFunction">The position set function.</param>
		public LambdaStream(Func<byte[], int, int, int> readFunction = null, Func<long, SeekOrigin, long> seekFunction = null, Action flushFunction = null,
			Action<byte[], int, int> writeFunction = null, Func<long> lengthGetFunction = null, Action<long> setLengthFunction = null,
			Func<long> positionGetFunction = null, Action<long> positionSetFunction = null)
		{
			ReadFunction = readFunction;
			SeekFunction = seekFunction;
			FlushFunction = flushFunction;
			WriteFunction = writeFunction;
			LengthGetFunction = lengthGetFunction;
			SetLengthFunction = setLengthFunction;
			PositionGetFunction = positionGetFunction;
			PositionSetFunction = positionSetFunction;
		}

		/// <summary>
		/// Gets the <see cref="Read(byte[], int, int)"/> function.
		/// </summary>
		/// <value>
		/// The read function.
		/// </value>
		public Func<byte[], int, int, int> ReadFunction { get; private set; }

		/// <summary>
		/// Gets the <see cref="Seek(long, SeekOrigin)"/> function.
		/// </summary>
		/// <value>
		/// The seek function.
		/// </value>
		public Func<long, SeekOrigin, long> SeekFunction { get; private set; }

		/// <summary>
		/// Gets the <see cref="Flush"/> function.
		/// </summary>
		/// <value>
		/// The flush function.
		/// </value>
		public Action FlushFunction { get; private set; }

		/// <summary>
		/// Gets the <see cref="Write(byte[], int, int)"/> function.
		/// </summary>
		/// <value>
		/// The write function.
		/// </value>
		public Action<byte[], int, int> WriteFunction { get; private set; }

		/// <summary>
		/// Gets the <see cref="Length"/> get function.
		/// </summary>
		/// <value>
		/// The length get function.
		/// </value>
		public Func<long> LengthGetFunction { get; private set; }

		/// <summary>
		/// Gets the <see cref="SetLength(long)"/> function.
		/// </summary>
		/// <value>
		/// The set length function.
		/// </value>
		public Action<long> SetLengthFunction { get; private set; }

		/// <summary>
		/// Gets the <see cref="Position"/> get function.
		/// </summary>
		/// <value>
		/// The position get function.
		/// </value>
		public Func<long> PositionGetFunction { get; private set; }

		/// <summary>
		/// Gets the <see cref="Position"/> set function.
		/// </summary>
		/// <value>
		/// The position set function.
		/// </value>
		public Action<long> PositionSetFunction { get; private set; }

		/// <summary>
		/// Gets a value indicating whether the current stream supports reading.
		/// </summary>
		public override bool CanRead => ReadFunction != null;

		/// <summary>
		/// Gets a value indicating whether the current stream supports seeking.
		/// </summary>
		public override bool CanSeek => SeekFunction != null;

		/// <summary>
		/// Gets a value indicating whether the current stream supports writing.
		/// </summary>
		public override bool CanWrite => WriteFunction != null;

		/// <summary>
		/// Gets the length in bytes of the stream.
		/// </summary>
		public override long Length => LengthGetFunction?.Invoke() ?? 0;

		/// <summary>
		/// Gets or sets the position within the current stream.
		/// </summary>
		/// <exception cref="NotSupportedException"></exception>
		public override long Position { get => PositionGetFunction?.Invoke() ?? throw new NotSupportedException(); set => PositionSetFunction?.Invoke(value); }

		/// <summary>
		/// Clears all buffers for this stream and causes any buffered data to be written to the underlying device.
		/// </summary>
		public override void Flush() => FlushFunction?.Invoke();

		/// <summary>
		/// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
		/// </summary>
		/// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and (offset + count - 1) replaced by the bytes read from the current source.</param>
		/// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream.</param>
		/// <param name="count">The maximum number of bytes to be read from the current stream.</param>
		/// <returns>
		/// The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.
		/// </returns>
		/// <exception cref="NotSupportedException"></exception>
		public override int Read(byte[] buffer, int offset, int count) => ReadFunction?.Invoke(buffer, offset, count) ?? throw new NotSupportedException();

		/// <summary>
		/// Sets the position within the current stream.
		/// </summary>
		/// <param name="offset">A byte offset relative to the origin parameter.</param>
		/// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin"></see> indicating the reference point used to obtain the new position.</param>
		/// <returns>
		/// The new position within the current stream.
		/// </returns>
		/// <exception cref="NotSupportedException"></exception>
		public override long Seek(long offset, SeekOrigin origin) => SeekFunction?.Invoke(offset, origin) ?? throw new NotSupportedException();

		/// <summary>
		/// Sets the length of the current stream.
		/// </summary>
		/// <param name="value">The desired length of the current stream in bytes.</param>
		public override void SetLength(long value) => SetLengthFunction?.Invoke(value);

		/// <summary>
		/// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
		/// </summary>
		/// <param name="buffer">An array of bytes. This method copies count bytes from buffer to the current stream.</param>
		/// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The number of bytes to be written to the current stream.</param>
		public override void Write(byte[] buffer, int offset, int count) => WriteFunction?.Invoke(buffer, offset, count);
	}
}
