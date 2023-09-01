using Application.Contracts.Company;
using FluentValidation;

namespace Application.Validators
{
	public class CreateCompanyDtoValidator : AbstractValidator<CreateCompanyDto>
	{
		public CreateCompanyDtoValidator(IValidator<CreateCompanyEmployeeDto> employeeValidator)
		{
			RuleFor(company => company.Name).NotEmpty();

			RuleFor(company => company.Employees)
				.NotNull() //could be empty
				.ForEach(employee => employee.NotNull().SetValidator(employeeValidator));
		}
	}
}

