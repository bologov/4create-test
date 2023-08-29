using Domain.Shared;

namespace Application.Contracts;

/// <summary>
/// A dto containing information needed to create an employee.
/// </summary>
public class CreateEmployeeDto
{
    /// <summary>
    /// Employee's email.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Employee's title.
    /// </summary>
    public EmployeeTitle? Title { get; set; }

    /// <summary>
    /// Guids of the companies that the employee is added to.
    /// </summary>
    public Guid[]? CompanyIds { get; set; }
}

