using Application.Contracts.Company;
using Domain.Entities;
using Domain.Managers;
using Domain.Repository;
using Domain.Specification;

namespace Application.Services
{
	public class CompanyService : ICompanyService
	{
        private readonly IRepository<Employee, Guid> _employeeRepository;
        private readonly IRepository<Company, Guid> _companyRepository;
        private readonly IEmployeeManager _employeeManager;
        private readonly ICompanyManager _companyManager;

        public CompanyService(IRepository<Employee, Guid> employeeRepository, IRepository<Company, Guid> companyRepository, IEmployeeManager employeeManager, ICompanyManager companyManager)
        {
            _employeeRepository = employeeRepository;
            _companyRepository = companyRepository;
            _employeeManager = employeeManager;
            _companyManager = companyManager;
        }

        public async Task<Guid> CreateCompanyAsync(CreateCompanyDto input)
        {
            var company = await _companyManager.CreateCompany(input.Name);

            var existingEmployeeIds = input.Employees.Where(x => x.EmployeeId.HasValue).Select(x => x.EmployeeId.Value).ToArray();
            var newEmployeeDtos = input.Employees.Where(x => x.EmployeeId is null).ToList();

            if (existingEmployeeIds.Any())
            {
                var existingEmployees = await _employeeRepository.FindManyAsync(new EmployeesByIdsSpecification(existingEmployeeIds));

                var missingEmployees = existingEmployeeIds.Except(existingEmployees.Select(x => x.Id)).ToList();
                if (missingEmployees.Any())
                {
                    throw new ArgumentException($"Requested employees {string.Join(',', missingEmployees)} were not found.");
                }

                foreach (var existingEmployee in existingEmployees)
                {
                    _companyManager.AddEmployee(company, existingEmployee);
                    _employeeRepository.Update(existingEmployee);
                }
            }

            if (newEmployeeDtos.Any())
            {
                foreach (var employeeDto in newEmployeeDtos)
                {
                    var employee = await _employeeManager.CreateEmployee(employeeDto.Title.Value, employeeDto.Email);

                    _companyManager.AddEmployee(company, employee);
                    _employeeRepository.Add(employee);
                }
            }

            _companyRepository.Update(company);

            return company.Id;
        }
	}
}

