using CareerCase.Domain.Resources;
using FluentValidation;

namespace CareerCase.Domain.Requests.Job
{
    public class CreateJobRequest
    {
        public int CompanyId { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string Benefits { get; set; }
        public string WorkType { get; set; }
        public int Pay { get; set; }
    }

    public class CreateJobRequestValidator : AbstractValidator<CreateJobRequest>
    {
        public CreateJobRequestValidator()
        {
            RuleFor(q => q.Position).NotNull().NotEmpty().WithMessage(ValidationMessages.ParameterCanNotBeEmpty);
            RuleFor(q => q.Description).NotNull().NotEmpty().WithMessage(ValidationMessages.ParameterCanNotBeEmpty);
        }
    }
}