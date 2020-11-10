using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BenchmarkDotNet.Attributes;

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

        [GlobalSetup]
        public void Setup()
        {
            _enumerableSource = SomeModel.Generate(20)
                as IEnumerable<SomeModel>;
            _arraySource = SomeModel.Generate(20)
                .ToArray();
            _listSource = SomeModel.Generate(20)
                .ToList();
            _collectionSource = new Collection<SomeModel>(
                SomeModel.Generate(20).ToList());
            _hashSetSource = SomeModel.Generate(20)
                .ToHashSet(new SomeModel.IdEqualityComparer());
            _dictionarySource = SomeModel.Generate(20)
                .ToDictionary(m => m.Id);
        }

        [Benchmark]
        public IEnumerable<SomeModel> SelectWhere() =>
            _enumerableSource
                .Select(ModifyImmutably)
                .Where(ValueHalfFull);

        [Benchmark]
        public IEnumerable<SomeModel> WhereSelect() =>
            _enumerableSource
                .Where(ValueHalfFull)
                .Select(ModifyImmutably);

        [Benchmark]
        public IEnumerable<SomeModel> Array_SelectWhere() =>
            _arraySource
                .Select(ModifyImmutably)
                .Where(ValueHalfFull);

        [Benchmark]
        public IEnumerable<SomeModel> Array_WhereSelect() =>
            _arraySource
                .Where(ValueHalfFull)
                .Select(ModifyImmutably);

        [Benchmark]
        public IEnumerable<SomeModel> List_SelectWhere() =>
            _listSource
                .Select(ModifyImmutably)
                .Where(ValueHalfFull);

        [Benchmark]
        public IEnumerable<SomeModel> List_WhereSelect() =>
            _listSource
                .Where(ValueHalfFull)
                .Select(ModifyImmutably);

        [Benchmark]
        public IEnumerable<SomeModel> Collection_SelectWhere() =>
            _collectionSource
                .Select(ModifyImmutably)
                .Where(ValueHalfFull);

        [Benchmark]
        public IEnumerable<SomeModel> Collection_WhereSelect() =>
            _collectionSource
                .Where(ValueHalfFull)
                .Select(ModifyImmutably);

        [Benchmark]
        public IEnumerable<SomeModel> HashSet_SelectWhere() =>
            _hashSetSource
                .Select(ModifyImmutably)
                .Where(ValueHalfFull);

        [Benchmark]
        public IEnumerable<SomeModel> HashSet_WhereSelect() =>
            _hashSetSource
                .Where(ValueHalfFull)
                .Select(ModifyImmutably);

        [Benchmark]
        public IEnumerable<SomeModel> Dictionary_SelectWhere() =>
            _dictionarySource
                .Select(kv => ModifyImmutably(kv.Value))
                .Where(ValueHalfFull);

        [Benchmark]
        public IEnumerable<SomeModel> Dictionary_WhereSelect() =>
            _dictionarySource
                .Where(kv => ValueHalfFull(kv.Value))
                .Select(kv => ModifyImmutably(kv.Value));

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