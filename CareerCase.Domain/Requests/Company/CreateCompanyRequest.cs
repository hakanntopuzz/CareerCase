using CareerCase.Domain.Resources;
using FluentValidation;

namespace CareerCase.Domain.Requests.Company
{
    public class CreateCompanyRequest
    {
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class CreateCompanyRequestValidator : AbstractValidator<CreateCompanyRequest>
    {
        public CreateCompanyRequestValidator()
        {
            RuleFor(q => q.Phone).NotNull().NotEmpty().WithMessage(ValidationMessages.ParameterCanNotBeEmpty);
            RuleFor(q => q.Name).NotNull().NotEmpty().WithMessage(ValidationMessages.ParameterCanNotBeEmpty);
            RuleFor(q => q.Address).NotNull().NotEmpty().WithMessage(ValidationMessages.ParameterCanNotBeEmpty);
        }
    }
}