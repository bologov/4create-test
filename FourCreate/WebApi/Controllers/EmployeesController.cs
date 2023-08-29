using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    public EmployeesController()
    {
    }

    /// <summary>
    /// Creates an employee.
    /// </summary>
    /// <param name="createCompanyDto">Employee details dto.</param>
    /// <returns>Guid of the created employee.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public IActionResult CreateNewEmployee(CreateEmployeeDto createEmployeeDto)
    {
        throw new NotImplementedException();

        // return OK instead of Created as there is no GET endpoints ot generate link to.
        return Ok();
    }
}

