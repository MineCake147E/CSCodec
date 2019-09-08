using System;
using System.Numerics;
using BenchmarkDotNet.Attributes;
using CSCodec.Filters.Transformation;

namespace CSCodecBenchmarks
{
    [CoreJob]
    public class FftBenchmarks
    {
        private Memory<ComplexF> testRegionF;
        private Memory<Complex> testRegion;

        [Params(2048)]
        public int Size { get; set; }

        [GlobalSetup]
        public void Init()
        {
            var r = new Random();
            testRegionF = new ComplexF[Size];
            testRegion = new Complex[Size];
            Span<Complex> trs = testRegion.Span;
            Span<ComplexF> trfs = testRegionF.Span;
            for (int i = 0; i < trs.Length; i++)
            {
                var value = (r.NextDouble() * 2) - 1;
                trs[i] = value;
                trfs[i] = (float)value;
            }
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            var r = new Random();
            if (testRegionF.IsEmpty) testRegionF = new ComplexF[Size];
            if (testRegion.IsEmpty) testRegion = new Complex[Size];
            Span<Complex> trs = testRegion.Span;
            Span<ComplexF> trfs = testRegionF.Span;
            for (int i = 0; i < trs.Length; i++)
            {
                var value = (r.NextDouble() * 2) - 1;
                trs[i] = value;
                trfs[i] = (float)value;
            }
        }

        [Benchmark]
        public void FftOldF() => FftOld.FFT(testRegionF.Span);

        [Benchmark]
        public void FftNewF() => FastFourierTransformation.FFT(testRegionF.Span);

        [Benchmark]
        public void FftOldD() => FftOld.FFT(testRegion.Span);

        [Benchmark]
        public void FftNewD() => FastFourierTransformation.FFT(testRegion.Span);
    }
}
