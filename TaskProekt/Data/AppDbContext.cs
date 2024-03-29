﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskProekt.Entities;
using Task = TaskProekt.Entities.Task;

namespace TaskProekt.Data;

public class AppDbContext :IdentityDbContext<User>
{
	public AppDbContext(DbContextOptions<AppDbContext> options, IServiceProvider services) : base(options) => this.Services = services;
	public IServiceProvider Services { get; set; }
	public DbSet<AuditLog> AuditLog { get; set; }
	public DbSet<Task> Tasks { get; set; }
	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.ApplyConfiguration<IdentityRole>(new RoleConfiguration(Services));
	}
}
