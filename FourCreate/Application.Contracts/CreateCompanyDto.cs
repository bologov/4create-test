using Domain.Shared;

namespace Application.Contracts;

/// <summary>
/// A dto containing information needed to create a company.
/// </summary>
public class CreateCompanyDto
{
    /// <summary>
    /// The company's name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The list of both existing and employees to be created..
    /// </summary>
    public CreateCompanyEmployeeDto[]? Employees { get; set; }
}

/// <summary>
/// A dto cointaining information needed to add an employee to the company being created.
/// </summary>
public class CreateCompanyEmployeeDto
{
    /// <summary>
    /// Id of the existing employee to be added to the company.
    /// Causes an error if set along with other fields.
    /// </summary>
    public Guid? EmployeeId { get; set; }

    /// <summary>
    /// Employee's email.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Employee's title.
    /// </summary>
    public EmployeeTitle? EmployeeTitle { get; set; }
}

