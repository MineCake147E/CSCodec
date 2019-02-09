using CSCodec.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace CSCodec
{
	/// <summary>
	/// The Base class for Codecs.
	/// </summary>
	public abstract class DecoderBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DecoderBase"/> class.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <exception cref="ArgumentNullException">format</exception>
		protected DecoderBase(WaveFormat format)
		{
			Format = format ?? throw new ArgumentNullException(nameof(format));
		}

		/// <summary>
		/// Gets the format.
		/// </summary>
		/// <value>
		/// The format.
		/// </value>
		public WaveFormat Format { get; }

		/// <summary>
		/// Reads the stream to the specified buffer.
		/// </summary>
		/// <param name="buffer">The buffer.</param>
		public abstract void Read(Span<byte> buffer);
	}
}
