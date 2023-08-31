using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Specification
{
    public class MatchingCompanyByNameSpecification : Specification<Company>
    {
        private readonly string _name;

        public MatchingCompanyByNameSpecification(string name)
        {
            _name = name;
        }

        public override Expression<Func<Company, bool>> Expression
        {
            get
            {
                return company => company.Name == _name;
            }
        }
    }
}