using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec.Utils
{
	public sealed partial class LambdaStream
	{
		#region Operators

		/// <summary>
		/// Performs an implicit conversion from <see cref="Func{T1, T2, T3, TResult}"/> to <see cref="LambdaStream"/>.
		/// </summary>
		/// <param name="ReadFunction">The read function.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator LambdaStream(Func<byte[], int, int, int> ReadFunction)
		{
			return new LambdaStream(readFunction: ReadFunction);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="Action{T1, T2, T3}"/> to <see cref="LambdaStream"/>.
		/// </summary>
		/// <param name="WriteFunction">The write function.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static implicit operator LambdaStream(Action<byte[], int, int> WriteFunction)
		{
			return new LambdaStream(writeFunction: WriteFunction);
		}

		#endregion Operators
	}
}
