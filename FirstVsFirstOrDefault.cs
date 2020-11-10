using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace ComparableLinqMethodsBenchmark
{
    public class FirstVsFirstOrDefault
    {
        private Guid _id;

        [GlobalSetup]
        public void Setup()
        {
            _id = Guid.NewGuid();
        }

        [Benchmark]
        public SomeModel First() =>
            SomeModel.Generate(20, id: _id)
                .First(m => m.Id == _id);

        [Benchmark]
        public SomeModel FirstOrDefault() =>
            SomeModel.Generate(20, id: _id)
                .FirstOrDefault(m => m.Id == _id);
    }
}