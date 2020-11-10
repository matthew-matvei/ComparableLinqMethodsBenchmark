using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ComparableLinqMethodsBenchmark
{
    public class SelectContainsVsAnyBenchmark
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
                SomeModel.Generate(20, id: _id).ToArray());
            _hashSetSource = SomeModel.Generate(20, id: _id)
                .ToHashSet(new SomeModel.IdEqualityComparer());
            _dictionarySource = SomeModel.Generate(20, id: _id)
                .ToDictionary(m => m.Id);
        }

        [Benchmark]
        public bool SelectContains() =>
            _enumerableSource
                .Select(m => m.Id)
                .Contains(_id);

        [Benchmark]
        public bool Any() =>
            _enumerableSource.Any(m => m.Id == _id);

        [Benchmark]
        public bool Array_SelectContains() =>
            _arraySource
                .Select(m => m.Id)
                .Contains(_id);

        [Benchmark]
        public bool Array_Any() =>
            _arraySource.Any(m => m.Id == _id);

        [Benchmark]
        public bool List_SelectContains() =>
            _listSource
                .Select(m => m.Id)
                .Contains(_id);

        [Benchmark]
        public bool List_Any() =>
            _listSource.Any(m => m.Id == _id);

        [Benchmark]
        public bool Collection_SelectContains() =>
            _collectionSource
                .Select(m => m.Id)
                .Contains(_id);

        [Benchmark]
        public bool Collection_Any() =>
            _collectionSource.Any(m => m.Id == _id);

        [Benchmark]
        public bool HashSet_SelectContains() =>
            _hashSetSource
                .Select(m => m.Id)
                .Contains(_id);

        [Benchmark]
        public bool HashSet_Any() =>
            _hashSetSource.Any(m => m.Id == _id);

        [Benchmark]
        public bool Dictionary_SelectContains() =>
            _dictionarySource
                .Select(m => m.Value.Id)
                .Contains(_id);

        [Benchmark]
        public bool Dictionary_Any() =>
            _dictionarySource.Any(m => m.Value.Id == _id);
    }
}
