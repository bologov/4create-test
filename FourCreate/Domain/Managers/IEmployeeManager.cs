
using Domain.Entities;
using Domain.Shared;

namespace Domain.Managers
{
	public interface IEmployeeManager
	{
		Task<Employee> CreateEmployee(EmployeeTitle title, string email);
	}
}

