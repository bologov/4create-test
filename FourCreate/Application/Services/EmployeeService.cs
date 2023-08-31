using Application.Contracts.Employee;
using Domain.Entities;
using Domain.Managers;
using Domain.Repository;
using Domain.Specification;

namespace Application.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IRepository<Employee, Guid> _employeeRepository;
		private readonly IRepository<Company, Guid> _companyRepository;
		private readonly IEmployeeManager _employeeManager;
		private readonly ICompanyManager _companyManager;

		public EmployeeService(IRepository<Employee, Guid> employeeRepository, IRepository<Company, Guid> companyRepository, IEmployeeManager employeeManager, ICompanyManager companyManager)
		{
			_employeeRepository = employeeRepository;
			_companyRepository = companyRepository;
			_employeeManager = employeeManager;
			_companyManager = companyManager;
		}

        public async Task<Guid> CreateEmployeeAsync(CreateEmployeeDto input)
		{
			var employee = await _employeeManager.CreateEmployee(input.Title.Value, input.Email);

			var companies = (await _companyRepository.FindAsync(new CompaniesByIdsSpecification(input.CompanyIds))).ToList();

			var missingCompanies = input.CompanyIds.Except(companies.Select(x => x.Id)).ToList();

			if (missingCompanies.Any())
			{
				throw new ArgumentException($"Requested companies {string.Join(',', missingCompanies)} were not found.");
			}

			foreach (var company in companies)
			{
				_companyManager.AddEmployee(company, employee);
				_companyRepository.Update(company);
			}

            _employeeRepository.Add(employee);

            return employee.Id;
		}
    }
}

