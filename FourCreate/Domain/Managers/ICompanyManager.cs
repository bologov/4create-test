
using Domain.Entities;

namespace Domain.Managers
{
	public interface ICompanyManager
	{
		Task<Company> CreateCompany(string name);

		void AddEmployee(Company company, Employee employee);
    }
}

