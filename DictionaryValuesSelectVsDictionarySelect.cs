using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace ComparableLinqMethodsBenchmark
{
    public class DictionaryValuesSelectVsDictionarySelect
    {
        private Dictionary<Guid, SomeModel> _source;
        private readonly Consumer _consumer = new Consumer();

        [GlobalSetup]
        public void Setup()
        {
            _source = SomeModel
                .Generate(200000)
                .ToDictionary(m => m.Id);
        }

        [Benchmark]
        public void ValuesSelect() =>
            _source.Values
                .Select(ModifyImmutably)
                .Consume(_consumer);

        [Benchmark]
        public void Select() =>
            _source
                .Select(kv => ModifyImmutably(kv.Value))
                .Consume(_consumer);

        private static SomeModel ModifyImmutably(SomeModel model) =>
            new SomeModel
            {
                Id = model.Id,
                Name = model.Name + ": modified",
                Value = model.Value * 2,
                When = model.When.AddDays(1)
            };
    }
}