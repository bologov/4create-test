using Domain.Shared;

namespace Domain.Entities
{
	public class Employee : Entity<Guid>, IAuditedEntity
	{
		public Employee(Guid id, EmployeeTitle title, string email, DateTime createdAt)
		{
			Id = id;
			Title = title;
			Email = email;
			CreatedAt = createdAt;
			Companies = new List<Company>();
		}

		public EmployeeTitle Title { get; private set; }

		public string Email { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public ICollection<Company> Companies { get; private set; }
	}
}

