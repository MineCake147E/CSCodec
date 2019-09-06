using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;
using CSCodecBenchmarks;

namespace CSCodecBenchmarkRunner
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<FftBenchmarks>();
        }
    }
}
