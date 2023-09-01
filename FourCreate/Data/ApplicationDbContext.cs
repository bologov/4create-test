using System.Reflection;
using Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
	public class ApplicationDbContext : AuditableDbContext
    {
		public DbSet<Company> Companies { get; private set; }

		public DbSet<Employee> Employee { get; private set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeProvider dateTimeProvider) : base (options, dateTimeProvider)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}

