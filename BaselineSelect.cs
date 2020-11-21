using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace ComparableLinqMethodsBenchmark
{
    public class BaselineSelect
    {
        private SomeModel[] _arraySource;
        private List<SomeModel> _listSource;
        private Collection<SomeModel> _collectionSource;
        private HashSet<SomeModel> _hashSetSource;
        private Dictionary<Guid, SomeModel> _dictionarySource;
        private readonly Consumer _consumer = new Consumer();

        [GlobalSetup]
        public void Setup()
        {
            _arraySource = SomeModel.Generate(200000).ToArray();
            _listSource = SomeModel.Generate(200000).ToList();
            _collectionSource = new Collection<SomeModel>(
                SomeModel.Generate(200000).ToArray());
            _hashSetSource = SomeModel
                .Generate(200000)
                .ToHashSet(new SomeModel.IdEqualityComparer());
            _dictionarySource = SomeModel.Generate(200000).ToDictionary(m => m.Id);
        }

        [Benchmark]
        public void Array_Baseline() =>
            _arraySource
                .Select(ModifyImmutably)
                .Consume(_consumer);

        [Benchmark]
        public void List_Baseline() =>
            _listSource
                .Select(ModifyImmutably)
                .Consume(_consumer);

        [Benchmark]
        public void Collection_Baseline() =>
            _collectionSource
                .Select(ModifyImmutably)
                .Consume(_consumer);

        [Benchmark]
        public void Hashset_Baseline() =>
            _hashSetSource
                .Select(ModifyImmutably)
                .Consume(_consumer);

        [Benchmark]
        public void Dictionary_Baseline() =>
            _dictionarySource
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