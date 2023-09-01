using Application.Contracts.Company;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompaniesController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    /// <summary>
    /// Creates a company along with its employees.
    /// </summary>
    /// <param name="createCompanyDto">Company details dto.</param>
    /// <returns>Guid of the created company.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateNewCompany(CreateCompanyDto createCompanyDto)
    {
        // return OK instead of Created as there is no GET endpoints ot generate link to.
        return Ok(await _companyService.CreateCompanyAsync(createCompanyDto));
    }
}

