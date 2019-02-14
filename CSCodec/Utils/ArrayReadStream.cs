using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSCodec.Utils
{
	/// <summary>
	/// A Stream that reads data from sprcified <see cref="byte"/> array.
	/// </summary>
	/// <seealso cref="Stream" />
	public sealed class ArrayReadStream : Stream
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ArrayReadStream"/> class.
		/// </summary>
		/// <param name="buffer">The buffer.</param>
		public ArrayReadStream(Memory<byte> buffer)
		{
			Buffer = buffer;
		}

		/// <summary>
		/// Gets or sets the memory to read.
		/// </summary>
		/// <value>
		/// The memory.
		/// </value>
		private Memory<byte> Buffer { get; set; }

		private int MemoryPosition { get; set; }

		/// <summary>
		/// Gets a value indicating whether the current stream supports reading.
		/// </summary>
		public override bool CanRead => true;

		/// <summary>
		/// Gets a value indicating whether the current stream supports seeking.
		/// </summary>
		public override bool CanSeek => true;

		/// <summary>
		/// Gets a value indicating whether the current stream supports writing.
		/// </summary>
		public override bool CanWrite => false;

		/// <summary>
		/// Gets the length in bytes of the stream.
		/// </summary>
		public override long Length => Buffer.Length;

		/// <summary>
		/// Gets or sets the position within the current stream.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">value - Value must be between 0 and Memory's Length!</exception>
		public override long Position
		{
			get => MemoryPosition; set
			{
				if (value < 0 || value > Buffer.Length) throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and Memory's Length!");
				MemoryPosition = (int)value;
			}
		}

		/// <summary>
		/// Clears all buffers for this stream and causes any buffered data to be written to the underlying device.
		/// </summary>
		/// <exception cref="NotSupportedException"></exception>
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
		/// </summary>
		/// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and (offset + count - 1) replaced by the bytes read from the current source.</param>
		/// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream.</param>
		/// <param name="count">The maximum number of bytes to be read from the current stream.</param>
		/// <returns>
		/// The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.
		/// </returns>
		public override int Read(byte[] buffer, int offset, int count)
		{
			count = (int)Math.Min(Length - Position, count);
			Buffer.Span.Slice(MemoryPosition, count).CopyTo(new Span<byte>(buffer, offset, count));
			Position += count;
			return count;
		}

		/// <summary>
		/// Sets the position within the current stream.
		/// </summary>
		/// <param name="offset">A byte offset relative to the origin parameter.</param>
		/// <param name="origin">A value of type <see cref="SeekOrigin"></see> indicating the reference point used to obtain the new position.</param>
		/// <returns>
		/// The new position within the current stream.
		/// </returns>
		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
				case SeekOrigin.Begin:
					Position = offset;
					break;

				case SeekOrigin.Current:
					Position += offset;
					break;

				case SeekOrigin.End:
					Position = Length + offset;
					break;

				default:
					break;
			}
			return Position;
		}

		/// <summary>
		/// Not Supported. Sets the length of the current stream.
		/// </summary>
		/// <param name="value">The desired length of the current stream in bytes.</param>
		/// <exception cref="NotSupportedException"></exception>
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Not Supported. Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
		/// </summary>
		/// <param name="buffer">An array of bytes. This method copies count bytes from buffer to the current stream.</param>
		/// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The number of bytes to be written to the current stream.</param>
		/// <exception cref="NotSupportedException"></exception>
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="T:System.IO.Stream"></see> and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			Buffer = default;
		}
	}
}
