using BenchmarkDotNet.Extensions;
using System;
using System.Collections.Generic;
using Bogus;
using System.Diagnostics.CodeAnalysis;

namespace ComparableLinqMethodsBenchmark
{
    public class SomeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public DateTimeOffset When { get; set; }

        public static IEnumerable<SomeModel> Generate(uint count) =>
            new Faker<SomeModel>()
                .StrictMode(true)
                .RuleFor(m => m.Id, _ => Guid.NewGuid())
                .RuleFor(m => m.Name, f => f.Name.FirstName())
                .RuleFor(m => m.Value, f => f.Random.Int(0, 100))
                .RuleFor(m => m.When, f => f.Date.SoonOffset(days: 3))
                .GenerateLazy((int)count);

        public static SomeModel FromId(Guid id) => new SomeModel
        {
            Id = id,
            Name = "Model created from id",
            Value = 101,
            When = DateTimeOffset.UtcNow
        };

        public class IdEqualityComparer : IEqualityComparer<SomeModel>
        {
            public bool Equals([AllowNull] SomeModel x, [AllowNull] SomeModel y) =>
                x.Id == y.Id;

            public int GetHashCode([DisallowNull] SomeModel obj) =>
                HashCode.Combine(obj.Id);
        }

        public class WhenDateEqualityComparer : IEqualityComparer<SomeModel>
        {
            public bool Equals([AllowNull] SomeModel x, [AllowNull] SomeModel y) =>
                x.When.Date == y.When.Date;

            public int GetHashCode([DisallowNull] SomeModel obj) =>
                HashCode.Combine(obj.When.Date);
        }
    }
}
