using BenchmarkDotNet.Running;

namespace Elementary.Memoization.Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<MeasureInsertPerformance>();
        }
    }
}