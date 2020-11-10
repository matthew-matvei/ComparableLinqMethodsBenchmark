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
        public bool SelectContains() =>
            SomeModel.Generate(20, id: _id)
                .Select(m => m.Id)
                .Contains(_id);

        [Benchmark]
        public bool Any() =>
            SomeModel.Generate(20, id: _id)
                .Any(m => m.Id == _id);
    }
}
