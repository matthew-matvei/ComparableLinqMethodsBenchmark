﻿using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ComparableLinqMethodsBenchmark
{
    public class WhereThenFirstOrDefaultVsFirstOrDefault
    {
        private Guid _id;
        private SomeModel[] _arraySource;
        private List<SomeModel> _listSource;
        private Collection<SomeModel> _collectionSource;
        private HashSet<SomeModel> _hashSetSource;
        private Dictionary<Guid, SomeModel> _dictionarySource;

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
        public SomeModel Array_WhereThenFirstOrDefault() =>
            _arraySource
                .Where(m => m.Id == _id)
                .FirstOrDefault();

        [Benchmark]
        public SomeModel Array_FirstOrDefault() =>
            _arraySource.FirstOrDefault(m => m.Id == _id);

        [Benchmark]
        public SomeModel List_WhereThenFirstOrDefault() =>
            _listSource
                .Where(m => m.Id == _id)
                .FirstOrDefault();

        [Benchmark]
        public SomeModel List_FirstOrDefault() =>
            _listSource.FirstOrDefault(m => m.Id == _id);

        [Benchmark]
        public SomeModel Collection_WhereThenFirstOrDefault() =>
            _collectionSource
                .Where(m => m.Id == _id)
                .FirstOrDefault();

        [Benchmark]
        public SomeModel Collection_FirstOrDefault() =>
            _collectionSource.FirstOrDefault(m => m.Id == _id);

        [Benchmark]
        public SomeModel HashSet_WhereThenFirstOrDefault() =>
            _hashSetSource
                .Where(m => m.Id == _id)
                .FirstOrDefault();

        [Benchmark]
        public SomeModel HashSet_FirstOrDefault() =>
            _hashSetSource.FirstOrDefault(m => m.Id == _id);

        [Benchmark]
        public SomeModel Dictionary_WhereThenFirstOrDefault() =>
            _dictionarySource
                .Where(m => m.Value.Id == _id)
                .FirstOrDefault()
                .Value;

        [Benchmark]
        public SomeModel Dictionary_FirstOrDefault() =>
            _dictionarySource
                .FirstOrDefault(m => m.Value.Id == _id)
                .Value;
    }
}
