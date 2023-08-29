using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    public CompaniesController()
    {
    }

    /// <summary>
    /// Creates a company along with its employees.
    /// </summary>
    /// <param name="createCompanyDto">Company details dto.</param>
    /// <returns>Guid of the created company.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public IActionResult CreateNewCompany(CreateCompanyDto createCompanyDto)
    {
        throw new NotImplementedException();


        // return OK instead of Created as there is no GET endpoints ot generate link to.
        return Ok();
    }
}

