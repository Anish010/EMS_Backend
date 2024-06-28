using EMS.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.DB.Context;
public class EMSContext : DbContext
{

    public EMSContext()
    {
    }

    public EMSContext(DbContextOptions dbContextOptions) :base(dbContextOptions)
    {
    }

    public DbSet<Employee> Employee { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Department> Department { get; set; }
    public DbSet<Project> Project { get; set; }
    public DbSet<Location> Location { get; set; }

    public DbSet<Mode> Mode { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
                .HasKey(e => e.Id);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Employee)
            .HasForeignKey(e => e.DepartmentId)
            .IsRequired(false);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Role)
            .WithMany(r => r.Employee)
            .HasForeignKey(e => e.RoleId)
            .IsRequired(false);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Location)
            .WithMany(l => l.Employee)
            .HasForeignKey(e => e.LocationId)
            .IsRequired(false);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Project)
            .WithMany(p => p.Employee)
            .HasForeignKey(e => e.ProjectId)
            .IsRequired(false);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Mode)
            .WithMany(m => m.Employee)
            .HasForeignKey(e => e.ModeStatusId)
            .IsRequired(false);

        modelBuilder.Entity<Location>()
            .HasKey(l => l.Id);

        modelBuilder.Entity<Department>()
            .HasKey(d => d.Id);

        modelBuilder.Entity<Role>()
            .HasKey(r => r.Id);

        modelBuilder.Entity<Role>()
            .HasOne(r => r.Department)
            .WithMany()
            .HasForeignKey(r => r.DepartmentId)
            .IsRequired(true);

        modelBuilder.Entity<Role>()
            .HasOne(r => r.Location)
            .WithMany()
            .HasForeignKey(r => r.LocationId)
            .IsRequired(false);


        modelBuilder.Entity<Project>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Mode>()
            .HasKey(m => m.Id);
    }
}