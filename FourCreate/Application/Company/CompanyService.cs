using Application.Contracts;

namespace Application.Company
{
	public class CompanyService : ICompanyService
	{
		public CompanyService()
		{
		}

		public async Task<Guid> CreateCompanyAsync(CreateCompanyDto createCompanyDto)
		{
			throw new NotImplementedException();
		}
	}
}

