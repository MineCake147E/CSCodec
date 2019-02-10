# CSCodec - .NET Standard Codec Development Library #
CSCodec is an open-source codec development library that has many features supporting development of your (original) encoder and/or decoder.

### Currently Supported Features: ###
- **LICENSED UNDER __Apache-2.0__** (won't entire project).
  - This means that you can use this library **without any copy-left**.
- `Span<T>` Based APIs
  - Many APIs depends on `Span<T>`.
- 24Bit Signed Integer Structure `Int24`
- An original **Apache-2.0 licensed Fast Fourier Transform (FFT) implementation** for `System.Numerics.Complex` arrays.
  - Looks like there's only implementations licensed under *LGPL* or *Ms-PL*, but we implemented for Apache-2.0 users.
- A Discrete Cosine Transform (DCT) Type-IV for `double` and `float` based on FFT
- Wavelet Transform(Haar and CDF 5/3 Wavelets for now) for some types(`double`, `float`, `int`, `Int24`, `short`, `sbyte`)
- A Time-Domain Aliasing Cancellation (like in MDCT) support for some types(`double`, `float`, `int`, `Int24`, `short`, `sbyte`)
- Hardware-Accelerated Add, Subtract, and Scalar Multiply for Arrays using `System.Numerics.Vector<T>`

### Features Under Development: ###

### Features In Future: ###
- BitReader reads bitwise encoded binary data
- CDF 9/7 wavelets
- ID3 Tags Support

### Project Goals: ###
- .NET Standard/Core/Frameworks, Mono, and **Xamarin** Support
- Inter-operation with [CSCore](https://github.com/filoe/cscore), [NAudio](https://github.com/naudio/NAudio), [MonoGame](https://github.com/MonoGame/MonoGame), etc...(*these parts will be licensed under Ms-PL*)
- Reference Implementations of cross-platform Apache-2.0 licensed FLAC Decoder and **Encoder** for both Samples and Uses.
