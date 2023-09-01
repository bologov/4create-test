using Application.Contracts.Company;
using FluentValidation;

namespace Application.Validators
{
	public class CreateCompanyEmployeeDtoValidator : AbstractValidator<CreateCompanyEmployeeDto>
	{
		public CreateCompanyEmployeeDtoValidator()
		{
            When(employee => employee.EmployeeId.HasValue, () =>
            {
                RuleFor(employee => employee.EmployeeId).NotEmpty();

            }).Otherwise(() =>
            {
                RuleFor(employee => employee.Email)
                    .NotEmpty()
                    .EmailAddress();
                RuleFor(employee => employee.Title)
                    .NotEmpty()
                    .IsInEnum();
            });

            RuleFor(employee => employee).Must(x => x.EmployeeId.HasValue ^ (x.Email is not null || x.Title.HasValue))
                .WithMessage($"When {nameof(CreateCompanyEmployeeDto.EmployeeId)} is set - all other properties must be null");
        }
	}
}

