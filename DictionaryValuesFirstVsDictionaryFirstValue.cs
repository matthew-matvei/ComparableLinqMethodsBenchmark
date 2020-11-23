using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace ComparableLinqMethodsBenchmark
{
    public class DictionaryValuesFirstVsDictionaryFirstValue
    {
        private Guid _id;
        private Dictionary<Guid, SomeModel> _source;

        [GlobalSetup]
        public void Setup()
        {
            _id = Guid.NewGuid();
            _source = SomeModel
                .Generate(200)
                .Append(SomeModel.FromId(_id))
                .ToDictionary(m => m.Id);
        }

        [Benchmark]
        public SomeModel ValuesFirst() =>
            _source.Values
                .First(m => m.Id == _id);

        [Benchmark]
        public SomeModel FirstValue() =>
            _source
                .First(kv => kv.Value.Id == _id)
                .Value;
    }
}