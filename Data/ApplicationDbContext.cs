using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetCareHub.Models;

namespace PetCareHub.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Vaccination> Vaccinations => Set<Vaccination>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Pet>().Property(p => p.Name).HasMaxLength(80).IsRequired();
        builder.Entity<Pet>().Property(p => p.Species).HasMaxLength(40).IsRequired();
        builder.Entity<Appointment>().Property(a => a.Reason).HasMaxLength(250).IsRequired();
        builder.Entity<Vaccination>().Property(v => v.VaccineName).HasMaxLength(100).IsRequired();

        // Avoid SQL Server multiple-cascade-path errors: child records follow their Pet,
        // while their OwnerId relationship does not create a second cascade path.
        builder.Entity<Appointment>()
            .HasOne(a => a.Pet).WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PetId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Appointment>()
            .HasOne<ApplicationUser>().WithMany()
            .HasForeignKey(a => a.OwnerId).OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Vaccination>()
            .HasOne(v => v.Pet).WithMany(p => p.Vaccinations)
            .HasForeignKey(v => v.PetId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Vaccination>()
            .HasOne<ApplicationUser>().WithMany()
            .HasForeignKey(v => v.OwnerId).OnDelete(DeleteBehavior.NoAction);
    }
}
