using BenchmarkDotNet.Attributes;
using System;
using System.Linq;

namespace ComparableLinqMethodsBenchmark
{
    public class SelectContainsVsAnyBenchmark
    {
        private Guid _id;
        private SomeModel[] _source;

        [GlobalSetup]
        public void Setup()
        {
            _id = Guid.NewGuid();
            _source = new[]
            {
                new SomeModel { Id = Guid.NewGuid() },
                new SomeModel { Id = _id },
                new SomeModel { Id = Guid.NewGuid() }
            };
        }

        [Benchmark]
        public bool BenchmarkSelectContains() => _source.Select(m => m.Id).Contains(_id);

        [Benchmark]
        public bool BenchmarkAny() => _source.Any(m => m.Id == _id);

        private class SomeModel
        {
            public Guid Id { get; set; }
        }
    }
}
