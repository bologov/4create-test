
using Domain.Entities;
using Domain.Shared;

namespace Domain.Managers
{
	public interface IEmployeeManager
	{
		Task<Employee> CreateEmployeeAsync(EmployeeTitle title, string email);
	}
}

