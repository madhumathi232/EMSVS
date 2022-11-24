using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EMSAdminService.Models;

public partial class EmsContext : DbContext
{
    public EmsContext()
    {
    }

    public EmsContext(DbContextOptions<EmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ReportingManager> ReportingManagers { get; set; }

    public virtual DbSet<RoleMaster> RoleMasters { get; set; }

    public virtual DbSet<TblDesignation> TblDesignations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=CTSDOTNET802;Initial Catalog=EMS;User ID=sa;Password=pass@word1;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentName).HasName("PK__Departme__D949CC3529AF97B9");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentName)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.DepartmentId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC07297DAC38");

            entity.Property(e => e.FileExtension).HasMaxLength(50);
            entity.Property(e => e.FileName).HasMaxLength(500);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.MimeType).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__AF2DBB990D59BAF0");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.ContactNo, "UQ__Employee__5C667C05D9B9571A").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Employee__A9D105345138943F").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(800);
            entity.Property(e => e.ContactNo)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.DateofJoining).HasColumnType("date");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Designation)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(500);
            entity.Property(e => e.FileExtension).HasMaxLength(50);
            entity.Property(e => e.FileName).HasMaxLength(500);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.FirstName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.LeaveBalance)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.MaxMonthLeave)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.MaxYearLeave)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.MimeType).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(500);
            entity.Property(e => e.ProjectName)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.ReportingManager)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.RoleName)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Salary)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.DepartmentNameNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentName)
                .HasConstraintName("FK__Employee__Depart__52593CB8");

            entity.HasOne(d => d.DesignationNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Designation)
                .HasConstraintName("FK__Employee__Design__534D60F1");

            entity.HasOne(d => d.ProjectNameNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ProjectName)
                .HasConstraintName("FK__Employee__Projec__5629CD9C");

            entity.HasOne(d => d.ReportingManagerNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ReportingManager)
                .HasConstraintName("FK__Employee__Report__5535A963");

            entity.HasOne(d => d.RoleNameNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleName)
                .HasConstraintName("FK__Employee__RoleNa__5441852A");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectName).HasName("PK__Projects__BCBE781D35BC2AD5");

            entity.Property(e => e.ProjectName)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.ProjectId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<ReportingManager>(entity =>
        {
            entity.HasKey(e => e.ManagerName).HasName("PK__Reportin__51D000A5CF1C31F1");

            entity.Property(e => e.ManagerName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ManagerId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<RoleMaster>(entity =>
        {
            entity.HasKey(e => e.RoleName).HasName("PK__RoleMast__8A2B6161A081249B");

            entity.ToTable("RoleMaster");

            entity.Property(e => e.RoleName)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TblDesignation>(entity =>
        {
            entity.HasKey(e => e.Designation).HasName("PK__tblDesig__5638C9D6E970F29E");

            entity.ToTable("tblDesignation");

            entity.Property(e => e.Designation)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.DesignationId).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
