using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ComparableLinqMethodsBenchmark
{
    public class SomeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public DateTimeOffset When { get; set; }

        public static IEnumerable<SomeModel> Generate(uint count) =>
            Enumerable.Range(1, (int)count)
                .Select(i => new SomeModel
                {
                    Id = Guid.NewGuid(),
                    Name = $"Model {i}",
                    Value = i,
                    When = DateTimeOffset.UtcNow.AddHours(i)
                });

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
