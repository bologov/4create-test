using Common;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repository;
using Domain.Shared;
using Domain.Specification;

namespace Domain.Managers
{
	public class EmployeeManager : IEmployeeManager
	{
        private readonly IRepository<Employee, Guid> _employeeRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IDateTimeProvider _dateTimeProvider;

        public EmployeeManager(IRepository<Employee, Guid> employeeRepository, IGuidGenerator guidGenerator, IDateTimeProvider dateTimeProvider)
        {
            _employeeRepository = employeeRepository;
            _guidGenerator = guidGenerator;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Employee> CreateEmployeeAsync(EmployeeTitle title, string email)
        {
            await CheckForMatchingEmployeeEmailAsync(email);

            var guid = _guidGenerator.GenerateGuid();
            var utcNow = _dateTimeProvider.GetUtcDateTimeNow();

            return new Employee(guid, title, email, utcNow);
        }

        private async Task CheckForMatchingEmployeeEmailAsync(string email)
        {
            var matchingEmployee = await _employeeRepository.FindOrDefaultAsync(new MatchingEmployeeByEmailSpecification(email));
            if (matchingEmployee is not null)
            {
                throw new BusinessException($"Employee with email {email} already exists in the system.");
            }
        }
    }
}

