using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComparableLinqMethodsBenchmark
{
    public class WhereThenFirstOrDefaultVsFirstOrDefault
    {
        private Guid _id;

        [GlobalSetup]
        public void Setup()
        {
            _id = Guid.NewGuid();
        }

        [Benchmark]
        public SomeModel WhereThenFirstOrDefault() =>
            SomeModel.Generate(20, id: _id)
                .Where(m => m.Id == _id)
                .FirstOrDefault();

        [Benchmark]
        public SomeModel FirstOrDefault() =>
            SomeModel.Generate(20, id: _id)
                .FirstOrDefault(m => m.Id == _id);
    }
}
