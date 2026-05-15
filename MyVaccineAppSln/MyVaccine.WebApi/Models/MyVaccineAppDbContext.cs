using Microsoft.EntityFrameworkCore;

namespace MyVaccine.WebApi.Models;

public class MyVaccineAppDbContext : DbContext
{
    public MyVaccineAppDbContext(DbContextOptions<MyVaccineAppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Dependent> Dependents { get; set; }

    public DbSet<VaccineCategory> VaccineCategories { get; set; }

    public DbSet<Vaccine> Vaccines { get; set; }

    public DbSet<VaccineRecord> VaccineRecords { get; set; }

    public DbSet<Allergy> Allergies { get; set; }

    public DbSet<FamilyGroup> FamilyGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);
        });

        // Dependent
        modelBuilder.Entity<Dependent>(entity =>
        {
            entity.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.HasOne(d => d.User)
                .WithMany(u => u.Dependents)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // VaccineCategory
        modelBuilder.Entity<VaccineCategory>(entity =>
        {
            entity.Property(vc => vc.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        // Vaccine
        modelBuilder.Entity<Vaccine>(entity =>
        {
            entity.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(255);
        });

        // VaccineRecord
        modelBuilder.Entity<VaccineRecord>(entity =>
        {
        });

        // Allergy
        modelBuilder.Entity<Allergy>(entity =>
        {
            entity.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(255);
        });

        // FamilyGroup
        modelBuilder.Entity<FamilyGroup>(entity =>
        {
            entity.Property(fg => fg.Name)
                .IsRequired()
                .HasMaxLength(255);
        });
    }
}