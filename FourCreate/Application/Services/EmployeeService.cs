using Application.Contracts.Employee;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Managers;
using Domain.Repository;
using Domain.Specification;
using Domain.UnitOfWork;

namespace Application.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IRepository<Employee, Guid> _employeeRepository;
		private readonly IRepository<Company, Guid> _companyRepository;
		private readonly IEmployeeManager _employeeManager;
		private readonly ICompanyManager _companyManager;
		private readonly IUnitOfWork _unitOfWork;

		public EmployeeService(IRepository<Employee, Guid> employeeRepository, IRepository<Company, Guid> companyRepository, IEmployeeManager employeeManager, ICompanyManager companyManager, IUnitOfWork unitOfWork)
		{
			_employeeRepository = employeeRepository;
			_companyRepository = companyRepository;
			_employeeManager = employeeManager;
			_companyManager = companyManager;
			_unitOfWork = unitOfWork;
		}

        public async Task<Guid> CreateEmployeeAsync(CreateEmployeeDto input)
		{
			var employee = await _employeeManager.CreateEmployeeAsync(input.Title!.Value, input.Email!);

			var companies = (await _companyRepository.FindManyAsync(new CompaniesByIdsSpecification(input.CompanyIds!), nameof(Company.Employees))).ToList();

			CheckCompaniesForExistence(input.CompanyIds, companies.Select(x => x.Id).ToArray());

			foreach (var company in companies)
			{
				_companyManager.AddEmployee(company, employee);
			}

            _employeeRepository.Add(employee);

			await _unitOfWork.SaveChangesAsync();

            return employee.Id;
		}

        private static void CheckCompaniesForExistence(Guid[] requestedCompanyIds, Guid[] fetchedCompanyIds)
        {
            var missingCompanies = requestedCompanyIds.Except(fetchedCompanyIds).ToList();
            if (missingCompanies.Any())
            {
                throw new BusinessException($"Companies {string.Join(',', missingCompanies)} do not exist in the system.");
            }
        }
    }
}

