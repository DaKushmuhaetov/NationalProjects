using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chuvashia.NationalProjects.Binding
{
    public sealed class CounterBinding
    {
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }
    public sealed class CounterBindingValidator : AbstractValidator<CounterBinding>
    {
        public CounterBindingValidator()
        {
            RuleFor(b => b.Type)
                .NotEmpty();
            RuleFor(b => b.Amount)
                .GreaterThanOrEqualTo(0);
        }
    }
}
