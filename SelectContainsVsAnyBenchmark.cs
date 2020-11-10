using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComparableLinqMethodsBenchmark
{
    public class SelectContainsVsAnyBenchmark
    {
        private Guid _id;
        private IEnumerable<SomeModel> _source;

        [GlobalSetup]
        public void Setup()
        {
            _id = Guid.NewGuid();
            _source = SomeModel.Generate(20);
        }

        [Benchmark]
        public bool BenchmarkSelectContains() =>
            _source.Select(m => m.Id).Contains(_id);

        [Benchmark]
        public bool BenchmarkAny() =>
            _source.Any(m => m.Id == _id);
    }
}
