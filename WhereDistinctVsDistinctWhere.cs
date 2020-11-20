using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace ComparableLinqMethodsBenchmark
{
    public class WhereDistinctVsDistinctWhere
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
        public void WhereDistinct() =>
            _enumerableSource
                .Where(ValueHalfFull)
                .Distinct(new SomeModel.WhenDateEqualityComparer())
                .Consume(_consumer);

        [Benchmark]
        public void DistinctWhere() =>
            _enumerableSource
                .Distinct(new SomeModel.WhenDateEqualityComparer())
                .Where(ValueHalfFull)
                .Consume(_consumer);

        [Benchmark]
        public void Array_WhereDistinct() =>
            _arraySource
                .Where(ValueHalfFull)
                .Distinct(new SomeModel.WhenDateEqualityComparer())
                .Consume(_consumer);

        [Benchmark]
        public void Array_DistinctWhere() =>
            _arraySource
                .Distinct(new SomeModel.WhenDateEqualityComparer())
                .Where(ValueHalfFull)
                .Consume(_consumer);

        [Benchmark]
        public void List_WhereDistinct() =>
            _listSource
                .Where(ValueHalfFull)
                .Distinct(new SomeModel.WhenDateEqualityComparer())
                .Consume(_consumer);

        [Benchmark]
        public void List_DistinctWhere() =>
            _listSource
                .Distinct(new SomeModel.WhenDateEqualityComparer())
                .Where(ValueHalfFull)
                .Consume(_consumer);

        [Benchmark]
        public void Collection_WhereDistinct() =>
            _collectionSource
                .Where(ValueHalfFull)
                .Distinct(new SomeModel.WhenDateEqualityComparer())
                .Consume(_consumer);

        [Benchmark]
        public void Collection_DistinctWhere() =>
            _collectionSource
                .Distinct(new SomeModel.WhenDateEqualityComparer())
                .Where(ValueHalfFull)
                .Consume(_consumer);

        [Benchmark]
        public void HashSet_WhereDistinct() =>
            _hashSetSource
                .Where(ValueHalfFull)
                .Distinct(new SomeModel.WhenDateEqualityComparer())
                .Consume(_consumer);

        [Benchmark]
        public void HashSet_DistinctWhere() =>
            _hashSetSource
                .Distinct(new SomeModel.WhenDateEqualityComparer())
                .Where(ValueHalfFull)
                .Consume(_consumer);

        [Benchmark]
        public void Dictionary_WhereDistinct() =>
            _dictionarySource
                .Select(kv => kv.Value)
                .Where(ValueHalfFull)
                .Distinct(new SomeModel.WhenDateEqualityComparer())
                .Consume(_consumer);

        [Benchmark]
        public void Dictionary_DistinctWhere() =>
            _dictionarySource
                .Select(kv => kv.Value)
                .Distinct(new SomeModel.WhenDateEqualityComparer())
                .Where(ValueHalfFull)
                .Consume(_consumer);

        private static bool ValueHalfFull(SomeModel arg) =>
            arg.Value <= 50;
    }
}