using Application.Contracts;

namespace Application.Company
{
	public interface ICompanyService
	{
		Task<Guid> CreateCompanyAsync(CreateCompanyDto createCompanyDto);
	}
}

