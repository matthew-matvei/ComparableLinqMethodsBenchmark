using BenchmarkDotNet.Running;

namespace ComparableLinqMethodsBenchmark
{
    public static class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run<SelectContainsVsAnyBenchmark>();
        }
    }
}
