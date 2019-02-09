using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CSCodec.Core
{
	/// <summary>
	/// Representing a format of wave.
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	public class WaveFormat
	{
		/// <summary>
		/// The encoding for wave.
		/// </summary>
		protected AudioEncoding encoding;

		/// <summary>
		/// The number of channels in wave.
		/// </summary>
		protected short channels;

		/// <summary>
		/// The sampling frequency of wave.
		/// </summary>
		protected int sampleRate;

		/// <summary>
		/// The block align.
		/// </summary>
		protected short blockAlign;

		/// <summary>
		/// The bits per sample.
		/// </summary>
		protected short bitsPerSample;

		/// <summary>
		/// The extra size.
		/// </summary>
		protected short extraSize;

		/// <summary>
		/// Initializes a new instance of the <see cref="WaveFormat"/> class.
		/// </summary>
		/// <param name="encoding">The encoding.</param>
		/// <param name="channels">The channels.</param>
		/// <param name="sampleRate">The sample rate.</param>
		/// <param name="blockAlign">The block align.</param>
		/// <param name="bitsPerSample">The bits per sample.</param>
		/// <param name="extraSize">Size of the extra.</param>
		public WaveFormat(AudioEncoding encoding, short channels, int sampleRate, short blockAlign, short bitsPerSample, short extraSize)
		{
			this.encoding = encoding;
			this.channels = channels;
			this.sampleRate = sampleRate;
			this.blockAlign = blockAlign;
			this.bitsPerSample = bitsPerSample;
			this.extraSize = extraSize;
		}

		/// <summary>
		/// Gets the audio encoding.
		/// </summary>
		/// <value>
		/// The audio encoding.
		/// </value>
		public AudioEncoding AudioEncoding => encoding;

		/// <summary>
		/// Gets the channels.
		/// </summary>
		/// <value>
		/// The channels.
		/// </value>
		public short Channels => channels;

		/// <summary>
		/// Gets the sample rate.
		/// </summary>
		/// <value>
		/// The sample rate.
		/// </value>
		public int SampleRate => sampleRate;

		/// <summary>
		/// Gets the block alignment.
		/// </summary>
		/// <value>
		/// The block alignment.
		/// </value>
		public short BlockAlignment => blockAlign;

		/// <summary>
		/// Gets the bits per sample.
		/// </summary>
		/// <value>
		/// The bits per sample.
		/// </value>
		public short BitsPerSample => bitsPerSample;

		/// <summary>
		/// Gets the size of the extra.
		/// </summary>
		/// <value>
		/// The size of the extra.
		/// </value>
		public short ExtraSize => extraSize;
	}
}
