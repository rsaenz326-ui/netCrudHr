using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace WebAppHr4.Models;

public partial class Hr9Context : DbContext
{
    public Hr9Context()
    {
    }

    public Hr9Context(DbContextOptions<Hr9Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Dependent> Dependents { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserStatus> UserStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PRIMARY");

            entity.ToTable("countries");

            entity.HasIndex(e => e.RegionId, "region_id");

            entity.Property(e => e.CountryId)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("country_id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(40)
                .HasColumnName("country_name");
            entity.Property(e => e.RegionId)
                .HasColumnType("int(11)")
                .HasColumnName("region_id");

            entity.HasOne(d => d.Region).WithMany(p => p.Countries)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("countries_ibfk_1");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PRIMARY");

            entity.ToTable("departments");

            entity.HasIndex(e => e.LocationId, "location_id");

            entity.Property(e => e.DepartmentId)
                .HasColumnType("int(11)")
                .HasColumnName("department_id");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(30)
                .HasColumnName("department_name");
            entity.Property(e => e.LocationId)
                .HasColumnType("int(11)")
                .HasColumnName("location_id");

            entity.HasOne(d => d.Location).WithMany(p => p.Departments)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("departments_ibfk_1");
        });

        modelBuilder.Entity<Dependent>(entity =>
        {
            entity.HasKey(e => e.DependentId).HasName("PRIMARY");

            entity.ToTable("dependents");

            entity.HasIndex(e => e.EmployeeId, "employee_id");

            entity.Property(e => e.DependentId)
                .HasColumnType("int(11)")
                .HasColumnName("dependent_id");
            entity.Property(e => e.EmployeeId)
                .HasColumnType("int(11)")
                .HasColumnName("employee_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Relationship)
                .HasMaxLength(25)
                .HasColumnName("relationship");

            entity.HasOne(d => d.Employee).WithMany(p => p.Dependents)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("dependents_ibfk_1");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

            entity.ToTable("employees");

            entity.HasIndex(e => e.DepartmentId, "department_id");

            entity.HasIndex(e => e.JobId, "job_id");

            entity.HasIndex(e => e.ManagerId, "manager_id");

            entity.Property(e => e.EmployeeId)
                .HasColumnType("int(11)")
                .HasColumnName("employee_id");
            entity.Property(e => e.DepartmentId)
                .HasColumnType("int(11)")
                .HasColumnName("department_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("first_name");
            entity.Property(e => e.HireDate).HasColumnName("hire_date");
            entity.Property(e => e.JobId)
                .HasColumnType("int(11)")
                .HasColumnName("job_id");
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .HasColumnName("last_name");
            entity.Property(e => e.ManagerId)
                .HasColumnType("int(11)")
                .HasColumnName("manager_id");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.Salary)
                .HasPrecision(8, 2)
                .HasColumnName("salary");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("employees_ibfk_2");

            entity.HasOne(d => d.Job).WithMany(p => p.Employees)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("employees_ibfk_1");

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("employees_ibfk_3");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PRIMARY");

            entity.ToTable("jobs");

            entity.Property(e => e.JobId)
                .HasColumnType("int(11)")
                .HasColumnName("job_id");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(35)
                .HasColumnName("job_title");
            entity.Property(e => e.MaxSalary)
                .HasPrecision(8, 2)
                .HasColumnName("max_salary");
            entity.Property(e => e.MinSalary)
                .HasPrecision(8, 2)
                .HasColumnName("min_salary");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PRIMARY");

            entity.ToTable("locations");

            entity.HasIndex(e => e.CountryId, "country_id");

            entity.Property(e => e.LocationId)
                .HasColumnType("int(11)")
                .HasColumnName("location_id");
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .HasColumnName("city");
            entity.Property(e => e.CountryId)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("country_id");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(12)
                .HasColumnName("postal_code");
            entity.Property(e => e.StateProvince)
                .HasMaxLength(25)
                .HasColumnName("state_province");
            entity.Property(e => e.StreetAddress)
                .HasMaxLength(40)
                .HasColumnName("street_address");

            entity.HasOne(d => d.Country).WithMany(p => p.Locations)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("locations_ibfk_1");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PRIMARY");

            entity.ToTable("permissions");

            entity.Property(e => e.PermissionId)
                .HasColumnType("int(11)")
                .HasColumnName("permission_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("PRIMARY");

            entity.ToTable("regions");

            entity.Property(e => e.RegionId)
                .HasColumnType("int(11)")
                .HasColumnName("region_id");
            entity.Property(e => e.RegionName)
                .HasMaxLength(25)
                .HasColumnName("region_name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("role_permissions");

            entity.HasIndex(e => e.PermissionId, "idx_permission");

            entity.HasIndex(e => new { e.RoleId, e.PermissionId }, "uk_role_permission").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.PermissionId)
                .HasColumnType("int(11)")
                .HasColumnName("permission_id");
            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("fk_permission");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("fk_role");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.EmployeeId, "employee_id");

            entity.HasIndex(e => e.StatusId, "fk_users_status");

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.EmployeeId)
                .HasColumnType("int(11)")
                .HasColumnName("employee_id");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.StatusId)
                .HasColumnType("int(11)")
                .HasColumnName("status_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Employee).WithMany(p => p.Users)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_ibfk_1");

            entity.HasOne(d => d.Status).WithMany(p => p.Users)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("fk_users_status");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PRIMARY");

            entity.ToTable("user_roles");

            entity.HasIndex(e => new { e.UserId, e.RoleId }, "uk_user_role").IsUnique();

            entity.HasIndex(e => e.RoleId, "user_roles_ibfk_2");

            entity.Property(e => e.UserRoleId)
                .HasColumnType("int(11)")
                .HasColumnName("user_role_id");
            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_roles_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_roles_ibfk_1");
        });

        modelBuilder.Entity<UserStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PRIMARY");

            entity.ToTable("user_status");

            entity.Property(e => e.StatusId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(20)
                .HasColumnName("status_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
