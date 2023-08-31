namespace Domain.Entities
{
	public class Company : Entity<Guid>, IAuditedEntity
	{
        public Company(Guid id, string name, DateTime createdAt)
        {
            Id = id;
            Name = name;
            CreatedAt = createdAt;
            Employees = new List<Employee>();
        }

        public string Name { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public ICollection<Employee> Employees { get; private set; }
	}
}

