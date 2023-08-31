using System.Linq.Expressions;
using Domain.Entities;
using Domain.Shared;

namespace Domain.Specification
{
    public class MatchingEmployeeByTitleSpecification : Specification<Employee>
    {
        private readonly EmployeeTitle _title;

        public MatchingEmployeeByTitleSpecification(EmployeeTitle title)
        {
            _title = title;
        }

        public override Expression<Func<Employee, bool>> Expression
        {
            get
            {
                return employee => employee.Title == _title;
            }
        }
    }
}