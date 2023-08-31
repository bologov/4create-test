using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Specification
{
    public class MatchingEmployeeByEmailSpecification : Specification<Employee>
    {
        private readonly string _email;

        public MatchingEmployeeByEmailSpecification(string email)
        {
            _email = email;
        }

        public override Expression<Func<Employee, bool>> Expression
        {
            get
            {
                return employee => employee.Email == _email;
            }
        }
    }
}