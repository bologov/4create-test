using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Specification
{
    public class CompaniesByIdsSpecification : Specification<Company>
    {
        private readonly Guid[] _companyIds;

        public CompaniesByIdsSpecification(Guid[] companyIds)
        {
            _companyIds = companyIds;
        }

        public override Expression<Func<Company, bool>> Expression
        {
            get
            {
                return company => _companyIds.Contains(company.Id);
            }
        }
    }
}