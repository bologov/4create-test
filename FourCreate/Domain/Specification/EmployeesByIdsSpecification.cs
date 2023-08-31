using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Specification
{
    public class EmployeesByIdsSpecification : Specification<Employee>
    {
        private readonly Guid[] _employeeIds;

        public EmployeesByIdsSpecification(Guid[] employeeIds)
        {
            _employeeIds = employeeIds;
        }

        public override Expression<Func<Employee, bool>> Expression
        {
            get
            {
                return employee => _employeeIds.Contains(employee.Id);
            }
        }
    }
}