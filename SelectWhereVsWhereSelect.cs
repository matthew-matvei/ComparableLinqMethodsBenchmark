using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace ComparableLinqMethodsBenchmark
{
    public class SelectWhereVsWhereSelect
    {
        private IEnumerable<SomeModel> _enumerableSource;
        private SomeModel[] _arraySource;
        private List<SomeModel> _listSource;
        private Collection<SomeModel> _collectionSource;
        private HashSet<SomeModel> _hashSetSource;
        private Dictionary<Guid, SomeModel> _dictionarySource;
        private readonly Consumer _consumer = new Consumer();

        [GlobalSetup]
        public void Setup()
        {
            _enumerableSource = SomeModel.Generate(200)
                as IEnumerable<SomeModel>;
            _arraySource = SomeModel.Generate(200)
                .ToArray();
            _listSource = SomeModel.Generate(200)
                .ToList();
            _collectionSource = new Collection<SomeModel>(
                SomeModel.Generate(200).ToList());
            _hashSetSource = SomeModel.Generate(200)
                .ToHashSet(new SomeModel.IdEqualityComparer());
            _dictionarySource = SomeModel.Generate(200)
                .ToDictionary(m => m.Id);
        }

        [Benchmark]
        public void SelectWhere() =>
            _enumerableSource
                .Select(ModifyImmutably)
                .Where(ValueHalfFull)
                .Consume(_consumer);

        [Benchmark]
        public void WhereSelect() =>
            _enumerableSource
                .Where(ValueHalfFull)
                .Select(ModifyImmutably)
                .Consume(_consumer);

        [Benchmark]
        public void Array_SelectWhere() =>
            _arraySource
                .Select(ModifyImmutably)
                .Where(ValueHalfFull)
                .Consume(_consumer);

        [Benchmark]
        public void Array_WhereSelect() =>
            _arraySource
                .Where(ValueHalfFull)
                .Select(ModifyImmutably)
                .Consume(_consumer);

        [Benchmark]
        public void List_SelectWhere() =>
            _listSource
                .Select(ModifyImmutably)
                .Where(ValueHalfFull)
                .Consume(_consumer);

        [Benchmark]
        public void List_WhereSelect() =>
            _listSource
                .Where(ValueHalfFull)
                .Select(ModifyImmutably)
                .Consume(_consumer);

        [Benchmark]
        public void Collection_SelectWhere() =>
            _collectionSource
                .Select(ModifyImmutably)
                .Where(ValueHalfFull)
                .Consume(_consumer);

        [Benchmark]
        public void Collection_WhereSelect() =>
            _collectionSource
                .Where(ValueHalfFull)
                .Select(ModifyImmutably)
                .Consume(_consumer);

        [Benchmark]
        public void HashSet_SelectWhere() =>
            _hashSetSource
                .Select(ModifyImmutably)
                .Where(ValueHalfFull)
                .Consume(_consumer);

        [Benchmark]
        public void HashSet_WhereSelect() =>
            _hashSetSource
                .Where(ValueHalfFull)
                .Select(ModifyImmutably)
                .Consume(_consumer);

        [Benchmark]
        public void Dictionary_SelectWhere() =>
            _dictionarySource
                .Select(kv => ModifyImmutably(kv.Value))
                .Where(ValueHalfFull)
                .Consume(_consumer);

        [Benchmark]
        public void Dictionary_WhereSelect() =>
            _dictionarySource
                .Where(kv => ValueHalfFull(kv.Value))
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

        private static bool ValueHalfFull(SomeModel model) =>
            model.Value <= 50;
    }
}