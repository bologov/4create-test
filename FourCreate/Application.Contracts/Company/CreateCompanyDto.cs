namespace Application.Contracts.Company
{
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
}

