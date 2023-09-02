using Domain.Shared;

namespace Domain.Entities
{
	public class Employee : Entity<Guid>
	{
		public Employee(Guid id, EmployeeTitle title, string email, DateTime createdAt) : base(id, createdAt)
		{
			Title = title;
			Email = email;
			Companies = new List<Company>();
		}

		public EmployeeTitle Title { get; init; }

        public string Email { get; init; }

		public ICollection<Company> Companies { get; init; }
	}
}

