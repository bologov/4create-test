using Application.Contracts.Employee;
using FluentValidation;

namespace Application.Validators
{
	public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
	{
		public CreateEmployeeDtoValidator()
		{
            RuleFor(employee => employee.Email).NotEmpty().EmailAddress();
            RuleFor(employee => employee.Title).NotEmpty().IsInEnum();
			RuleFor(employee => employee.CompanyIds)
				.NotEmpty()
				.ForEach(companyId => companyId.NotEmpty());;
		}
	}
}

