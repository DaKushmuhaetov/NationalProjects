using Chuvashia.NationalProjects.Model.News;
using FluentValidation;

namespace Chuvashia.NationalProjects.Binding
{
    public sealed class CreateNewsBinding
    {
        public NationalProjectType Type { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }

    public sealed class CreateNewsBindingValidator : AbstractValidator<CreateNewsBinding>
    {
        public CreateNewsBindingValidator()
        {
            RuleFor(b => b.Type)
                .NotNull();
            RuleFor(b => b.Title)
                .NotEmpty();
            RuleFor(b => b.Text)
                .NotEmpty();
        }
    }
}
