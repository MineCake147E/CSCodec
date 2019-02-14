# CSCodec - .NET Standard Codec Development Library #
CSCodec is an open-source Codec development library that has many features supporting development of your decoder and/or encoder for both your original or well-known formats.

### Currently Supported Features: ###
- **Core Project is LICENSED UNDER __[Apache-2.0](https://github.com/MineCake147E/CSCodec/blob/master/LICENSE.md)__**
- `Span<T>` Based APIs
  - Many APIs depends on `Span<T>`.
- 24Bit Signed Integer Structure `Int24`
- An original **Fully Managed Fast Fourier Transform (FFT) implementation** for `System.Numerics.Complex` arrays.
  - Looks like there's only implementations licensed under [LGPL](https://www.gnu.org/licenses/lgpl-3.0.en.html) or [Ms-PL](https://github.com/MineCake147E/MonoGame.TexturedGeometry2D/blob/master/LICENSE.md), but it is implemented under Apache-2.0.
- A Discrete Cosine Transform (DCT) Type-IV for `double` and `float` based on FFT
- Wavelet Transform(Haar and CDF 5/3 Wavelets for now) for some types(`double`, `float`, `int`, `Int24`, `short`, `sbyte`)
- A Time-Domain Aliasing Cancellation (like in MDCT) support for some types(`double`, `float`, `int`, `Int24`, `short`, `sbyte`)
- Hardware-Accelerated Add, Subtract, and Scalar Multiply for Arrays using `System.Numerics.Vector<T>`
- BitReader reads bitwise encoded binary data
- BitWriter writes bitwise encoded binary data

### Features Under Development: ###
- Better Documentations for existing APIs

### Features In Future: ###
- CDF 9/7 wavelets
- ID3 Tags Support

### Project Goals: ###
- .NET Standard/Core/Frameworks, Mono, and **Xamarin** Support
- Inter-operation with [CSCore](https://github.com/filoe/cscore), [NAudio](https://github.com/naudio/NAudio), [MonoGame](https://github.com/MonoGame/MonoGame), etc...(*these parts will be licensed under Ms-PL*)
- Reference Implementations of cross-platform FLAC Decoder and **Encoder** for both Samples and Uses.
