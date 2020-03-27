using FluentValidation;

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
