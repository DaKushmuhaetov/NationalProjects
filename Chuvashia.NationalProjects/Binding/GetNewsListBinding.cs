using FluentValidation;
using System;
using System.Text.Json.Serialization;

namespace Chuvashia.NationalProjects.Binding
{
    public sealed class GetNewsListBinding
    {
        /// <summary>
        /// Offset for pagination. Optional. 0 by default.
        /// </summary>
        public int Offset { get; set; } = 0;

        /// <summary>
        /// Number of items per page. Optional. 10 by default.
        /// </summary>
        public int Limit { get; set; } = 10;

        /// <summary>
        /// Date from which start counting. Optional.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Date from which end counting. Optional.
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// Type of news. Optional. All by default.
        /// </summary>
        [JsonConverter(typeof(NewsTypeBinding))]
        public NewsTypeBinding Type { get; set; } = NewsTypeBinding.All;

        /// <summary>
        /// Filter by title. Optional.
        /// </summary>
        public string TitleFilter { get; set; }
    }

    public enum NewsTypeBinding
    {
        All,
        Healthcare,
        Education,
        Road
    }

    public sealed class GetNewsListBindingValidator : AbstractValidator<GetNewsListBinding>
    {
        public GetNewsListBindingValidator()
        {
            RuleFor(b => b.Offset)
                .GreaterThanOrEqualTo(0);
            RuleFor(b => b.Limit)
                .InclusiveBetween(2, 1000);
        }
    }
}
