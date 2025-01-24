using System;
using Microsoft.EntityFrameworkCore;
using MusicHub.Backend.Models;

namespace MusicHub.Backend.Data
{
	public class ApplicationDbContext : DbContext
	{
		// Constructor to configure teh DbContext with options
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public DbSet<Songs> Songs { get; set; }
	}
}

