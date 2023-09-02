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
                // name field is case insensitive in the db, so toLower is to be used for checks at .NET
                return company => company.Name.ToLower() == _name.ToLower();
            }
        }
    }
}