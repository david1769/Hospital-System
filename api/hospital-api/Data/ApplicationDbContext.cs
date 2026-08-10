using hospital_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hospital_api.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<ReferenceData> ReferenceData { get; set; }
        public DbSet<ReferenceDataCategory> ReferenceDataCategory { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>().HasData(
                new Patient { Id = 1, FirstName = "John",LastName = "Doe", DateOfBirth = new DateTime(1990, 1, 1),Gender = true,PhoneNumber = 012334944,Address = "Washington Street Akasia",EmergencyContact = 038388832 }
            );

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasOne(a => a.Patient)
                      .WithMany()                          
                      .HasForeignKey(a => a.PatientId)
                      .OnDelete(DeleteBehavior.SetNull);    

                entity.HasOne(a => a.Doctor)
                      .WithMany()
                      .HasForeignKey(a => a.DoctorId);

                entity.HasOne(a => a.Department)
                      .WithMany()
                      .HasForeignKey(a => a.DepartmentId);

                entity.HasOne(a => a.Status)
                      .WithMany()
                      .HasForeignKey(a => a.StatusId);
            });

        }
    }
}