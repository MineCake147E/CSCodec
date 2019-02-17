# CSCodec - .NET Standard Audio Codec Development Library #
CSCodec is an open-source Audio Codec development library that has many features supporting development of your decoder and/or encoder for both your original or well-known formats.

### Currently Supported Features: ###
- **Core Project is LICENSED UNDER __[Apache-2.0](https://github.com/MineCake147E/CSCodec/blob/master/LICENSE.md)__**
- A lot of `Span<T>` Based APIs
- 24Bit Signed Integer Structure `Int24`
- An original **Fast Fourier Transform (FFT) IMPLEMENTATION** for `System.Numerics.Complex` arrays.
- A Discrete Cosine Transform (DCT) Type-IV for `double` and `float` based on FFT
- Wavelet Transform(Haar and CDF 5/3 Wavelets for now) for some types(`double`, `float`, `int`, `Int24`, `short`, `sbyte`)
- A Time-Domain Aliasing Cancellation (like in MDCT) support for some types(`double`, `float`)
- Hardware-Accelerated Add, Subtract, and Scalar Multiply for Arrays using `System.Numerics.Vector<T>`
- Bit Encoded Binary Data I/O
  - BitReader and BitWriter supports I/O of bit encoded binary data
- Cyclic Redundancy Check(CRC) of 8bits, 16bits, 32bits Support(Tested with some polynomials)
- Linear Prediction support for `double` and `float` using FFT when possible and appropriate

### Features Under Development: ###
- TDAC test and debug for `int`, `Int24`, `short`, and `sbyte`
- Unit Tests for existing APIs
- Better Documentations for existing APIs
- Change underlying structure of `Int24` from three `byte`s to single `int`

### Features In Future: ###
- Linear Prediction support for `int`, `Int24`, `short`, and `sbyte` in order to implement FLAC Encoder and Decoder
- Binary Compression
  - Rice Coding
  - Huffman Coding
  - Arithmetic Coding without patents
    - Range Coder
- new type `SingleComplex` that supports `float` based Complex calculation
  - FFT for `SingleComplex`
- `Parallel`ization for existing apis where possible and appropriate
- `decimal` supports below if possible, needed, and appropriate
  - new type `DecimalComplex`
    - FFT
    - DCT-IV
  - Haar and CDF 5/7 wavelets
  - TDAC
  - Array Calculation but not Hardware-Accelerated maybe
  - BitReader/BitWriter
  - LinearPredictor
- Unrounded Irreversible CDF 9/7 wavelet transform for `double`, `float`
- Unrounded Irreversible CDF 9/7 wavelet transform for `decimal` if possible
- Rounded CDF 9/7 wavelet transform for `int`, `Int24`, `short`, and `sbyte` if possible
- ID3 Tags Support

### Project Goals: ###
- .NET Standard/Core/Frameworks, Mono, and **Xamarin** Support
- Inter-operation with [CSCore](https://github.com/filoe/cscore), [NAudio](https://github.com/naudio/NAudio), [MonoGame](https://github.com/MonoGame/MonoGame), etc...(*these parts will be licensed under Ms-PL*)
- Reference Implementations of cross-platform FLAC Decoder and **Encoder** for both Samples and Uses.
