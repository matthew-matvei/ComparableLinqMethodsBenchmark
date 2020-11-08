using BenchmarkDotNet.Attributes;
using System;
using System.Linq;

namespace ComparableLinqMethodsBenchmark
{
    public class WhereThenFirstOrDefaultVsFirstOrDefault
    {
        private Guid _id;
        private SomeModel[] _source;

        [GlobalSetup]
        public void Setup()
        {
            _id = Guid.NewGuid();
            _source = SomeModel.Generate(20).ToArray();
        }

        [Benchmark]
        public SomeModel BenchmarkWhereThenFirstOrDefault() =>
            _source.Where(m => m.Id == _id).FirstOrDefault();

        [Benchmark]
        public SomeModel BenchmarkFirstOrDefault() =>
            _source.FirstOrDefault(m => m.Id == _id);
    }
}
