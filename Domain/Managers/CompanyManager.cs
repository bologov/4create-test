using Common;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repository;
using Domain.Specification;

namespace Domain.Managers
{
	public class CompanyManager : ICompanyManager
	{
        private readonly IRepository<Company, Guid> _companyRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CompanyManager(IRepository<Company, Guid> companyRepository, IGuidGenerator guidGenerator, IDateTimeProvider dateTimeProvider)
        {
            _companyRepository = companyRepository;
            _guidGenerator = guidGenerator;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Company> CreateCompanyAsync(string name)
        {
            await CheckForMatchingCompanyNameAsync(name);

            var guid = _guidGenerator.GenerateGuid();
            var utcNow = _dateTimeProvider.GetUtcDateTimeNow();

            return new Company(guid, name, utcNow);
        }

        public void AddEmployee(Company company, Employee employee)
        {
            CheckForMatchingEmployeeTitle(company, employee);

            company.Employees.Add(employee);
            employee.Companies.Add(company);
        }

        private async Task CheckForMatchingCompanyNameAsync(string name)
        {
            var matchingCompany = await _companyRepository.FindOrDefaultAsync(new MatchingCompanyByNameSpecification(name));
            if (matchingCompany is not null)
            {
                throw new BusinessException($"Company with the same name already exists in the system - {matchingCompany.Id}.");
            }
        }

        private static void CheckForMatchingEmployeeTitle(Company company, Employee employee)
        {
            var spec = new MatchingEmployeeByTitleSpecification(employee.Title);

            var matchingTitleEmployee = company.Employees.Where(spec.IsSatisfiedBy).FirstOrDefault();
            if (matchingTitleEmployee is not null)
            {
                throw new BusinessException($"Cannot add employee with title {employee.Title} to company {company.Name} as it already has an employee with the same title - {matchingTitleEmployee.Email}.");
            }
        }
    }
}

