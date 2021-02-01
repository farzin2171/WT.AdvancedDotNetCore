using System;
using BenchmarkDotNet.Running;

namespace WT.ProjeectBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<SerializationBenchMark>();
        }
    }
}
