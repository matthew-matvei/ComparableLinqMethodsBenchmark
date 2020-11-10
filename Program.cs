using BenchmarkDotNet.Running;

namespace ComparableLinqMethodsBenchmark
{
    public static class Program
    {
        public static void Main()
        {
            /*BenchmarkRunner.Run<SelectContainsVsAnyBenchmark>();
            BenchmarkRunner.Run<WhereThenFirstOrDefaultVsFirstOrDefault>();
            BenchmarkRunner.Run<SelectWhereVsWhereSelect>();*/
            BenchmarkRunner.Run<FirstVsFirstOrDefault>();
        }
    }
}
