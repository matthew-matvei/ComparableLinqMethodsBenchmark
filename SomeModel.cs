using System;
using System.Collections.Generic;
using Bogus;

namespace ComparableLinqMethodsBenchmark
{
    public class SomeModel
    {
        private static Random _random = new Random();

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public DateTimeOffset When { get; set; }

        public static IEnumerable<SomeModel> Generate(
            uint count,
            Guid? id = null)
        {
            var targetElementIndex = _random.Next((int)count);

            return new Faker<SomeModel>()
                .StrictMode(true)
                .RuleFor(
                    m => m.Id,
                    f => f.IndexFaker == targetElementIndex
                        ? (id.HasValue ? id.Value : Guid.NewGuid())
                        : Guid.NewGuid())
                .RuleFor(m => m.Name, f => f.Name.FirstName())
                .RuleFor(m => m.Value, f => f.Random.Int(0, 100))
                .RuleFor(m => m.When, f => f.Date.SoonOffset())
                .GenerateLazy((int)count);
        }
    }
}
