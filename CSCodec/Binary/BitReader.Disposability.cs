using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec.Binary
{
	public partial class BitReader : IDisposable
	{
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
			//ファイナライザがなくても必要らしい
			GC.SuppressFinalize(this);
		}

		#endregion IDisposable Support
	}
}
