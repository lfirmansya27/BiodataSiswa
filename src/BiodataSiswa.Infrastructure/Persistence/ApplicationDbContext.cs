using BiodataSiswa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BiodataSiswa.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<DataCollectionPeriod> DataCollectionPeriods => Set<DataCollectionPeriod>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("students");
            entity.HasKey(s => s.ID);
            entity.Property(s => s.StudentNumber).HasColumnName("student_number").HasMaxLength(20).IsRequired();
            entity.HasIndex(s => s.StudentNumber).IsUnique();
            entity.Property(s => s.FullName).HasColumnName("full_name").HasMaxLength(150).IsRequired();
            entity.Property(s => s.CreatedAtUtc).HasColumnName("created_at_utc").IsRequired();
        });

        modelBuilder.Entity<DataCollectionPeriod>(entity =>
        {
            entity.ToTable("data_collection_periods", table =>
            {
                table.HasCheckConstraint("ck_data_collection_periods_dates", "starts_at_utc < ends_at_utc");
                table.HasCheckConstraint("ck_data_collection_periods_status", "status IN ('draft','open','closed','finalized')");
            });

            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasColumnName("id");
            entity.Property(p => p.Name).HasColumnName("name").HasMaxLength(150).IsRequired();
            entity.Property(p => p.AcademicYear).HasColumnName("academic_year").HasMaxLength(10).IsRequired();
            entity.Property(p => p.StartsAtUtc).HasColumnName("starts_at_utc").IsRequired();
            entity.Property(p => p.EndsAtUtc).HasColumnName("ends_at_utc").IsRequired();
            entity.Property(p => p.Status).HasColumnName("status").HasMaxLength(20).IsRequired();
            entity.Property(p => p.CreatedAtUtc).HasColumnName("created_at_utc").IsRequired();
            entity.HasIndex(p => new { p.AcademicYear, p.Name }).IsUnique();
        });
    }
}