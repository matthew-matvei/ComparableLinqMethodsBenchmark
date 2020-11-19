using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace ComparableLinqMethodsBenchmark
{
    public class WhereWhereVsWhere
    {
        private Guid _id;
        private IEnumerable<SomeModel> _enumerableSource;
        private SomeModel[] _arraySource;
        private List<SomeModel> _listSource;
        private Collection<SomeModel> _collectionSource;
        private HashSet<SomeModel> _hashSetSource;
        private Dictionary<Guid, SomeModel> _dictionarySource;

        [GlobalSetup]
        public void Setup()
        {
            _id = Guid.NewGuid();
            _enumerableSource = SomeModel.Generate(20, id: _id)
                .ToArray() as IEnumerable<SomeModel>;
            _arraySource = SomeModel.Generate(20, id: _id)
                .ToArray();
            _listSource = SomeModel.Generate(20, id: _id)
                .ToList();
            _collectionSource = new Collection<SomeModel>(
                SomeModel.Generate(20, id: _id).ToList());
            _hashSetSource = SomeModel.Generate(20, id: _id)
                .ToHashSet(new SomeModel.IdEqualityComparer());
            _dictionarySource = SomeModel.Generate(20, id: _id)
                .ToDictionary(m => m.Id);
        }

        [Benchmark]
        public IEnumerable<SomeModel> WhereWhere() =>
            _enumerableSource
                .Where(ValueHalfEmpty)
                .Where(m => m.Id == _id);

        [Benchmark]
        public IEnumerable<SomeModel> Where() =>
            _enumerableSource
                .Where(m => ValueHalfEmpty(m) && m.Id == _id);

        [Benchmark]
        public IEnumerable<SomeModel> Array_WhereWhere() =>
            _arraySource
                .Where(ValueHalfEmpty)
                .Where(m => m.Id == _id);

        [Benchmark]
        public IEnumerable<SomeModel> Array_Where() =>
            _arraySource
                .Where(m => ValueHalfEmpty(m) && m.Id == _id);

        [Benchmark]
        public IEnumerable<SomeModel> List_WhereWhere() =>
            _listSource
                .Where(ValueHalfEmpty)
                .Where(m => m.Id == _id);

        [Benchmark]
        public IEnumerable<SomeModel> List_Where() =>
            _listSource
                .Where(m => ValueHalfEmpty(m) && m.Id == _id);

        [Benchmark]
        public IEnumerable<SomeModel> Collection_WhereWhere() =>
            _collectionSource
                .Where(ValueHalfEmpty)
                .Where(m => m.Id == _id);

        [Benchmark]
        public IEnumerable<SomeModel> Collection_Where() =>
            _collectionSource
                .Where(m => ValueHalfEmpty(m) && m.Id == _id);

        [Benchmark]
        public IEnumerable<SomeModel> HashSet_WhereWhere() =>
            _hashSetSource
                .Where(ValueHalfEmpty)
                .Where(m => m.Id == _id);

        [Benchmark]
        public IEnumerable<SomeModel> HashSet_Where() =>
            _hashSetSource
                .Where(m => ValueHalfEmpty(m) && m.Id == _id);

        [Benchmark]
        public IEnumerable<SomeModel> Dictionary_WhereWhere() =>
            _dictionarySource
                .Select(m => m.Value)
                .Where(ValueHalfEmpty)
                .Where(m => m.Id == _id);

        [Benchmark]
        public IEnumerable<SomeModel> Dictionary_Where() =>
            _dictionarySource
                .Select(m => m.Value)
                .Where(m => ValueHalfEmpty(m) && m.Id == _id);

        private static bool ValueHalfEmpty(SomeModel model) =>
            model.Value <= 50;
    }
}