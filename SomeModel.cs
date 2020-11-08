using System;
using System.Collections.Generic;
using Bogus;

namespace ComparableLinqMethodsBenchmark
{
    public class SomeModel
    {
        public Guid Id { get; set; }

        public static IEnumerable<SomeModel> Generate(uint count) =>
            new Faker<SomeModel>()
                .StrictMode(true)
                .RuleFor(m => m.Id, _ => Guid.NewGuid())
                .GenerateLazy((int)count);
    }
}
