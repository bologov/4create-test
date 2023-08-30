using Application.Contracts;

namespace Application.Employee
{
	public interface IEmployeeService
	{
		Task<Guid> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);
	}
}

