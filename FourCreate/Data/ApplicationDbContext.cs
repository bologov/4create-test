﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
	public class ApplicationDbContext : DbContext
    {
		public DbSet<Company> Companies { get; private set; }

		public DbSet<Employee> Employee { get; private set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
		{

		}
	}
}
