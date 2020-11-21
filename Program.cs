using BenchmarkDotNet.Running;

namespace ComparableLinqMethodsBenchmark
{
    public static class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run<FirstVsFirstOrDefault>();
            BenchmarkRunner.Run<SelectContainsVsAnyBenchmark>();
            BenchmarkRunner.Run<SelectWhereVsWhereSelect>();
            BenchmarkRunner.Run<WhereDistinctVsDistinctWhere>();
            BenchmarkRunner.Run<WhereThenFirstOrDefaultVsFirstOrDefault>();
            BenchmarkRunner.Run<WhereWhereVsWhere>();
        }
    }
}
