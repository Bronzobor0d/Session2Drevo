using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Session2DrevoTry2.DB;

public partial class _2025rchContext : DbContext
{
    public _2025rchContext()
    {
    }

    public _2025rchContext(DbContextOptions<_2025rchContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cabinet> Cabinets { get; set; }

    public virtual DbSet<Calendar> Calendars { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EventCalendar> EventCalendars { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<StatusEvent> StatusEvents { get; set; }

    public virtual DbSet<StatusMaterial> StatusMaterials { get; set; }

    public virtual DbSet<Subdivision> Subdivisions { get; set; }

    public virtual DbSet<TypeEvent> TypeEvents { get; set; }

    public virtual DbSet<TypeMaterial> TypeMaterials { get; set; }

    public virtual DbSet<TypePeriod> TypePeriods { get; set; }

    public virtual DbSet<WorkingCalendar> WorkingCalendars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=HOME-PC;Trusted_Connection=true;database=2025RCH;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cabinet>(entity =>
        {
            entity.ToTable("Cabinet");

            entity.Property(e => e.Number).HasMaxLength(50);
        });

        modelBuilder.Entity<Calendar>(entity =>
        {
            entity.ToTable("Calendar");

            entity.Property(e => e.DateEnd).HasColumnType("datetime");
            entity.Property(e => e.DateStart).HasColumnType("datetime");
            entity.Property(e => e.Justification).HasMaxLength(150);

            entity.HasOne(d => d.Employee).WithMany(p => p.Calendars)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Calendar_Employee");

            entity.HasOne(d => d.TypePeriod).WithMany(p => p.Calendars)
                .HasForeignKey(d => d.TypePeriodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Calendar_TypePeriod");

            entity.HasMany(d => d.Materials).WithMany(p => p.Calendars)
                .UsingEntity<Dictionary<string, object>>(
                    "CalendarMaterial",
                    r => r.HasOne<Material>().WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CalendarMaterial_Material"),
                    l => l.HasOne<Calendar>().WithMany()
                        .HasForeignKey("CalendarId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CalendarMaterial_Calendar"),
                    j =>
                    {
                        j.HasKey("CalendarId", "MaterialId");
                        j.ToTable("CalendarMaterial");
                    });
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Fio)
                .HasMaxLength(150)
                .HasColumnName("FIO");
            entity.Property(e => e.MobilePhone).HasMaxLength(20);
            entity.Property(e => e.WorkPhone).HasMaxLength(20);

            entity.HasOne(d => d.Cabinet).WithMany(p => p.Employees)
                .HasForeignKey(d => d.CabinetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Cabinet");

            entity.HasOne(d => d.Helper).WithMany(p => p.InverseHelper)
                .HasForeignKey(d => d.HelperId)
                .HasConstraintName("FK_Employee_Employee1");

            entity.HasOne(d => d.Leader).WithMany(p => p.InverseLeader)
                .HasForeignKey(d => d.LeaderId)
                .HasConstraintName("FK_Employee_Employee");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Role");

            entity.HasOne(d => d.Subdivision).WithMany(p => p.Employees)
                .HasForeignKey(d => d.SubdivisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Subdivision");
        });

        modelBuilder.Entity<EventCalendar>(entity =>
        {
            entity.ToTable("EventCalendar");

            entity.Property(e => e.DateEnd).HasColumnType("datetime");
            entity.Property(e => e.DateStart).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Employee).WithMany(p => p.EventCalendars)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_EventCalendar_Employee");

            entity.HasOne(d => d.StatusEvent).WithMany(p => p.EventCalendars)
                .HasForeignKey(d => d.StatusEventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventCalendar_StatusEvent");

            entity.HasOne(d => d.Subdivision).WithMany(p => p.EventCalendars)
                .HasForeignKey(d => d.SubdivisionId)
                .HasConstraintName("FK_EventCalendar_Subdivision");

            entity.HasOne(d => d.TypeEvent).WithMany(p => p.EventCalendars)
                .HasForeignKey(d => d.TypeEventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventCalendar_TypeEvent");

            entity.HasMany(d => d.Employees).WithMany(p => p.EventCalendarsNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "EventEmployee",
                    r => r.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_EventEmployee_Employee"),
                    l => l.HasOne<EventCalendar>().WithMany()
                        .HasForeignKey("EventCalendarId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_EventEmployee_EventCalendar"),
                    j =>
                    {
                        j.HasKey("EventCalendarId", "EmployeeId");
                        j.ToTable("EventEmployee");
                    });
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.ToTable("Material");

            entity.Property(e => e.Area).HasMaxLength(150);
            entity.Property(e => e.Author).HasMaxLength(150);
            entity.Property(e => e.DateApproval).HasColumnType("datetime");
            entity.Property(e => e.DateChange).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.StatusMaterial).WithMany(p => p.Materials)
                .HasForeignKey(d => d.StatusMaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Material_StatusMaterial");

            entity.HasOne(d => d.TypeMaterial).WithMany(p => p.Materials)
                .HasForeignKey(d => d.TypeMaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Material_TypeMaterial");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<StatusEvent>(entity =>
        {
            entity.ToTable("StatusEvent");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<StatusMaterial>(entity =>
        {
            entity.ToTable("StatusMaterial");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Subdivision>(entity =>
        {
            entity.ToTable("Subdivision");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.Leader).WithMany(p => p.Subdivisions)
                .HasForeignKey(d => d.LeaderId)
                .HasConstraintName("FK_Subdivision_Employee");

            entity.HasOne(d => d.SubdivisionNavigation).WithMany(p => p.InverseSubdivisionNavigation)
                .HasForeignKey(d => d.SubdivisionId)
                .HasConstraintName("FK_Subdivision_Subdivision");
        });

        modelBuilder.Entity<TypeEvent>(entity =>
        {
            entity.ToTable("TypeEvent");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<TypeMaterial>(entity =>
        {
            entity.ToTable("TypeMaterial");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<TypePeriod>(entity =>
        {
            entity.ToTable("TypePeriod");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<WorkingCalendar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("WorkingCalendar_pk");

            entity.ToTable("WorkingCalendar", tb => tb.HasComment("Список дней исключений в производственном календаре"));

            entity.Property(e => e.Id).HasComment("Идентификатор строки");
            entity.Property(e => e.ExceptionDate).HasComment("День-исключение");
            entity.Property(e => e.IsWorkingDay).HasComment("0 - будний день, но законодательно принят выходным");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
