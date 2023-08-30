using Application.Contracts;
using Application.Employee;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    /// <summary>
    /// Creates an employee.
    /// </summary>
    /// <param name="createCompanyDto">Employee details dto.</param>
    /// <returns>Guid of the created employee.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateNewEmployee(CreateEmployeeDto createEmployeeDto)
    {
        // return OK instead of Created as there is no GET endpoints ot generate link to.
        return Ok(await _employeeService.CreateEmployeeAsync(createEmployeeDto));
    }
}

