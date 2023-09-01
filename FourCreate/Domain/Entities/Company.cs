namespace Domain.Entities
{
	public class Company : Entity<Guid>
	{
        public Company(Guid id, string name, DateTime createdAt) : base(id, createdAt)
        {
            Name = name;
            Employees = new List<Employee>();
        }

        public string Name { get; init; }

		public ICollection<Employee> Employees { get; init; }
	}
}

