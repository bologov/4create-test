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
                // email field is case insensitive in the db, so toLower is to be used for checks at .NET
                return employee => employee.Email.ToLower() == _email.ToLower();
            }
        }
    }
}