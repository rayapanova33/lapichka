using LapichkaShop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LapichkaShop.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Adoption> Adoptions { get; set; }
        public DbSet<AnimalType> AnimalTypes { get; set; }
        public DbSet<VetProcedure> VetProcedures { get; set; }
        public DbSet<Application> Applications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Animal>()
            .HasOne(d => d.AnimalType)
            .WithMany(t => t.Animals)
            .HasForeignKey(d => d.TypeId);

            modelBuilder.Entity<Adoption>()
            .HasOne(d => d.Animal)
            .WithMany(t => t.AdoptionRequests)
            .HasForeignKey(d => d.AnimalId);

            modelBuilder.Entity<Adoption>()
            .HasOne(d => d.Employee)             
            .WithMany(t => t.ProcessedRequests)          
            .HasForeignKey(d => d.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Adoption>()
            .HasOne(d => d.Adopter)             
            .WithMany(t => t.AdoptionRequests)          
            .HasForeignKey(d => d.AdopterId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VetProcedure>()
            .HasOne(d => d.Animal)
            .WithMany(t => t.VetProcedures)
            .HasForeignKey(d => d.AnimalId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Application>()
            .HasOne(d => d.User)
            .WithMany(t => t.Applications)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
             {
                Id = "1",
                Name = LapichkaShop.Data.Constants.UserRoles.Admin,
                NormalizedName = LapichkaShop.Data.Constants.UserRoles.Admin.ToUpper()
             },
            new IdentityRole
            {
                Id = "2",
                Name = LapichkaShop.Data.Constants.UserRoles.Employee,
                NormalizedName = LapichkaShop.Data.Constants.UserRoles.Employee.ToUpper()
            },
            new IdentityRole
            {
                Id = "3",
                Name = LapichkaShop.Data.Constants.UserRoles.User,
                NormalizedName = LapichkaShop.Data.Constants.UserRoles.User.ToUpper()
            });

        }
    }
}
