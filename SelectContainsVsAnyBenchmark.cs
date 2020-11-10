using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComparableLinqMethodsBenchmark
{
    public class SelectContainsVsAnyBenchmark
    {
        private Guid _id;

        [GlobalSetup]
        public void Setup()
        {
            _id = Guid.NewGuid();
        }

        [Benchmark]
        public bool BenchmarkSelectContains() =>
            SomeModel.Generate(20, id: _id)
                .Select(m => m.Id)
                .Contains(_id);

        [Benchmark]
        public bool BenchmarkAny() =>
            SomeModel.Generate(20, id: _id)
                .Any(m => m.Id == _id);
    }
}
