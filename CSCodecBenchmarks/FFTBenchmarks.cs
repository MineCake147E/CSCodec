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

        private const int size = 2048;

        [GlobalSetup]
        public void Init()
        {
            var r = new Random();
            testRegionF = new ComplexF[size];
            testRegion = new Complex[size];
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
            if (testRegionF.IsEmpty) testRegionF = new ComplexF[size];
            if (testRegion.IsEmpty) testRegion = new Complex[size];
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
