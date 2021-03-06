﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CSCodec.Filters.Transformation
{
	/// <summary>
	/// Fast Fourier Transformation Modes
	/// </summary>
	public enum FftMode : short
	{
		/// <summary>
		/// The forward transform.
		/// </summary>
		Forward = 1,

		/// <summary>
		/// The backward transform.
		/// </summary>
		Backward = 2
	}
}
