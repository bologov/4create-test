namespace Domain.Shared;

/// <summary>
/// Enum of possible employee titles.
/// </summary>
public enum EmployeeTitle
{
    // start from 1, so default value of the underlying type - 0, won't be valid enum.
    Developer = 1,
    Manager,
    Tester
}

