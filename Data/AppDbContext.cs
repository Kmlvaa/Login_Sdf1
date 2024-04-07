using Login.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;

namespace Login.Data
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{
		public AppDbContext()
		{
		}
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}
		public DbSet<AppUser> AppUsers { get; set; }

	}
}
