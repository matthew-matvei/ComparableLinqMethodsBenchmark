using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace ComparableLinqMethodsBenchmark
{
    public class WhereDistinctVsDistinctWhere_DifferingLevelsOfDistinctness
    {
        private SomeModel[] _tenDistinctSource;
        private SomeModel[] _fiftyDistinctSource;
        private SomeModel[] _oneHundredDistinctSource;
        private SomeModel[] _oneHundredAndFiftyDistinctSource;
        private readonly Consumer _consumer = new Consumer();

        [GlobalSetup]
        public void Setup()
        {
            _tenDistinctSource = GenerateDistinctDates(10, 200)
                .ToArray();
            _fiftyDistinctSource = GenerateDistinctDates(50, 200)
                .ToArray();
            _oneHundredDistinctSource = GenerateDistinctDates(100, 200)
                .ToArray();
            _oneHundredAndFiftyDistinctSource = GenerateDistinctDates(
                150,
                200)
                .ToArray();
        }

        [Benchmark]
        public void TenDistinct_WhereDistinct() =>
            _tenDistinctSource
                .Where(ValueIsHalfFull(10))
                .Distinct(new SomeModel.WhenDateEqualityComparer())
                .Consume(_consumer);

        [Benchmark]
        public void TenDistinct_DistinctWhere() =>
            _tenDistinctSource
                .Distinct(new SomeModel.WhenDateEqualityComparer())
                .Where(ValueIsHalfFull(10))
                .Consume(_consumer);

        [Benchmark]
        public void FiftyDistinct_WhereDistinct() =>
            _fiftyDistinctSource
                .Where(ValueIsHalfFull(50))
                .Distinct(new SomeModel.WhenDateEqualityComparer())
                .Consume(_consumer);

        [Benchmark]
        public void FiftyDistinct_DistinctWhere() =>
            _fiftyDistinctSource
                .Distinct(new SomeModel.WhenDateEqualityComparer())
                .Where(ValueIsHalfFull(50))
                .Consume(_consumer);

        [Benchmark]
        public void OneHundredDistinct_WhereDistinct() =>
            _oneHundredDistinctSource
                .Where(ValueIsHalfFull(100))
                .Distinct(new SomeModel.WhenDateEqualityComparer())
                .Consume(_consumer);

        [Benchmark]
        public void OneHundredDistinct_DistinctWhere() =>
            _oneHundredDistinctSource
                .Distinct(new SomeModel.WhenDateEqualityComparer())
                .Where(ValueIsHalfFull(100))
                .Consume(_consumer);

        [Benchmark]
        public void OneHundredAndFiftyDistinct_WhereDistinct() =>
            _oneHundredAndFiftyDistinctSource
                .Where(ValueIsHalfFull(150))
                .Distinct(new SomeModel.WhenDateEqualityComparer())
                .Consume(_consumer);

        [Benchmark]
        public void OneHundredAndFiftyDistinct_DistinctWhere() =>
            _oneHundredAndFiftyDistinctSource
                .Distinct(new SomeModel.WhenDateEqualityComparer())
                .Where(ValueIsHalfFull(150))
                .Consume(_consumer);

        private static IEnumerable<SomeModel> GenerateDistinctDates(
            uint distinctModelCount,
            uint modelCount) =>
                Enumerable
                    .Range(1, (int)modelCount)
                    .Select(i => new SomeModel
                    {
                        Id = Guid.NewGuid(),
                        Name = $"Model {i}",
                        Value = i % (int)distinctModelCount,
                        When = DateTimeOffset.UtcNow.AddDays(i % distinctModelCount)
                    });

        private static Func<SomeModel, bool> ValueIsHalfFull(uint distinctAmount)
            => (SomeModel model) => model.Value <= distinctAmount / 2;
    }
}