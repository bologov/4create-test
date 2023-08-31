namespace Application.Contracts.Company
{
	public interface ICompanyService
	{
		Task<Guid> CreateCompanyAsync(CreateCompanyDto createCompanyDto);
	}
}

