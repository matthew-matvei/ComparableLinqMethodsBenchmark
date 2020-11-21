using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace ComparableLinqMethodsBenchmark
{
    public class WhereWhereVsWhere
    {
        private Guid _id;
        private SomeModel[] _arraySource;
        private List<SomeModel> _listSource;
        private Collection<SomeModel> _collectionSource;
        private HashSet<SomeModel> _hashSetSource;
        private Dictionary<Guid, SomeModel> _dictionarySource;
        private readonly Consumer _consumer = new Consumer();

        [GlobalSetup]
        public void Setup()
        {
            _id = Guid.NewGuid();
            _arraySource = SomeModel
                .Generate(200)
                .Append(SomeModel.FromId(_id))
                .ToArray();
            _listSource = SomeModel
                .Generate(200)
                .Append(SomeModel.FromId(_id))
                .ToList();
            _collectionSource = new Collection<SomeModel>(
                SomeModel
                    .Generate(200)
                    .Append(SomeModel.FromId(_id))
                    .ToList());
            _hashSetSource = SomeModel
                .Generate(200)
                .Append(SomeModel.FromId(_id))
                .ToHashSet(new SomeModel.IdEqualityComparer());
            _dictionarySource = SomeModel
                .Generate(200)
                .Append(SomeModel.FromId(_id))
                .ToDictionary(m => m.Id);
        }

        [Benchmark]
        public void Array_WhereWhere() =>
            _arraySource
                .Where(ValueHalfEmpty)
                .Where(m => m.Id == _id)
                .Consume(_consumer);

        [Benchmark]
        public void Array_Where() =>
            _arraySource
                .Where(m => ValueHalfEmpty(m) && m.Id == _id)
                .Consume(_consumer);

        [Benchmark]
        public void List_WhereWhere() =>
            _listSource
                .Where(ValueHalfEmpty)
                .Where(m => m.Id == _id)
                .Consume(_consumer);

        [Benchmark]
        public void List_Where() =>
            _listSource
                .Where(m => ValueHalfEmpty(m) && m.Id == _id)
                .Consume(_consumer);

        [Benchmark]
        public void Collection_WhereWhere() =>
            _collectionSource
                .Where(ValueHalfEmpty)
                .Where(m => m.Id == _id)
                .Consume(_consumer);

        [Benchmark]
        public void Collection_Where() =>
            _collectionSource
                .Where(m => ValueHalfEmpty(m) && m.Id == _id)
                .Consume(_consumer);

        [Benchmark]
        public void HashSet_WhereWhere() =>
            _hashSetSource
                .Where(ValueHalfEmpty)
                .Where(m => m.Id == _id)
                .Consume(_consumer);

        [Benchmark]
        public void HashSet_Where() =>
            _hashSetSource
                .Where(m => ValueHalfEmpty(m) && m.Id == _id)
                .Consume(_consumer);

        [Benchmark]
        public void Dictionary_WhereWhere() =>
            _dictionarySource
                .Select(m => m.Value)
                .Where(ValueHalfEmpty)
                .Where(m => m.Id == _id)
                .Consume(_consumer);

        [Benchmark]
        public void Dictionary_Where() =>
            _dictionarySource
                .Select(m => m.Value)
                .Where(m => ValueHalfEmpty(m) && m.Id == _id)
                .Consume(_consumer);

        private static bool ValueHalfEmpty(SomeModel model) =>
            model.Value <= 50;
    }
}