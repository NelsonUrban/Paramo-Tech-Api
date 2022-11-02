using FluentValidation;
using Sat.Recruitment.Business.Dtos;

namespace Sat.Recruitment.Custom.Validatiors
{
    public class CreateUserValidator : AbstractValidator<UserDto>
    {
        public CreateUserValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("The name is required");
            RuleFor(u => u.Email).NotEmpty().WithMessage("The email is required");
            RuleFor(u => u.Email).EmailAddress().WithMessage("The email has invalid format");
            RuleFor(u => u.Address).NotEmpty().WithMessage("The address is required");
            RuleFor(u => u.Phone).NotEmpty().WithMessage("The phone is required");
        }
    }
}
