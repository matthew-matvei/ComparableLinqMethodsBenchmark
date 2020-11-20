using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace ComparableLinqMethodsBenchmark
{
    public class FirstVsFirstOrDefault
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
            _enumerableSource = SomeModel
                .Generate(200)
                .Append(SomeModel.FromId(_id))
                .ToArray() as IEnumerable<SomeModel>;
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
        public SomeModel First() =>
            _enumerableSource.First(m => m.Id == _id);

        [Benchmark]
        public SomeModel FirstOrDefault() =>
            _enumerableSource.FirstOrDefault(m => m.Id == _id);

        [Benchmark]
        public SomeModel Array_First() =>
            _arraySource.First(m => m.Id == _id);

        [Benchmark]
        public SomeModel Array_FirstOrDefault() =>
            _arraySource.FirstOrDefault(m => m.Id == _id);

        [Benchmark]
        public SomeModel List_First() =>
            _listSource.First(m => m.Id == _id);

        [Benchmark]
        public SomeModel List_FirstOrDefault() =>
            _listSource.FirstOrDefault(m => m.Id == _id);

        [Benchmark]
        public SomeModel Collection_First() =>
            _collectionSource.First(m => m.Id == _id);

        [Benchmark]
        public SomeModel Collection_FirstOrDefault() =>
            _collectionSource.FirstOrDefault(m => m.Id == _id);

        [Benchmark]
        public SomeModel HashSet_First() =>
            _hashSetSource.First(m => m.Id == _id);

        [Benchmark]
        public SomeModel HashSet_FirstOrDefault() =>
            _hashSetSource.FirstOrDefault(m => m.Id == _id);

        [Benchmark]
        public SomeModel Dictionary_First() =>
            _dictionarySource.First(m => m.Value.Id == _id).Value;

        [Benchmark]
        public SomeModel Dictionary_FirstOrDefault() =>
            _dictionarySource.FirstOrDefault(m => m.Value.Id == _id).Value;


    }
}