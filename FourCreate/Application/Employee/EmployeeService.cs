using Application.Contracts;

namespace Application.Employee
{
	public class EmployeeService : IEmployeeService
	{
		public EmployeeService()
		{
		}

        public async Task<Guid> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto)
		{
			throw new NotImplementedException();
		}
    }
}

