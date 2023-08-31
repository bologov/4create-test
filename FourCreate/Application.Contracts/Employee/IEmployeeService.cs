namespace Application.Contracts.Employee
{
	public interface IEmployeeService
	{
		Task<Guid> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);
	}
}

