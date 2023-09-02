using Domain.Shared;

namespace Application.Contracts.Company
{
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
        public EmployeeTitle? Title { get; set; }
    }
}

