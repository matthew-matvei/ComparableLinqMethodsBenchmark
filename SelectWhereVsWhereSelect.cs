using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace ComparableLinqMethodsBenchmark
{
    public class SelectWhereVsWhereSelect
    {
        [Benchmark]
        public IEnumerable<SomeModel> BenchmarkSelectWhere() =>
            SomeModel.Generate(20)
                .Select(ModifyImmutably)
                .Where(ValueHalfFull);

        [Benchmark]
        public IEnumerable<SomeModel> BenchmarkWhereSelect() =>
            SomeModel.Generate(20)
                .Where(ValueHalfFull)
                .Select(ModifyImmutably);

        private static SomeModel ModifyImmutably(SomeModel model) =>
            new SomeModel
            {
                Id = model.Id,
                Name = model.Name + ": modified",
                Value = model.Value * 2,
                When = model.When.AddDays(1)
            };

        private static bool ValueHalfFull(SomeModel model) =>
            model.Value <= 50;
    }
}