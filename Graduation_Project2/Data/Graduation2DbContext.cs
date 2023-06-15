using Graduation_Project2.Models;
using Graduation_Project2.Models.SharedClass;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Graduation_Project2.Models.ViewModel;



namespace Graduation_Project2.Data
{
    public class Graduation2DbContext: IdentityDbContext
    {
        public Graduation2DbContext(DbContextOptions<Graduation2DbContext> options):base(options)
        {

        }

        public DbSet<Patient> patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<DoctorAdmin> doctorAdmins { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<MedicalCentre> medicalCentres { get; set; }
        public DbSet<Schedule> schedules { get; set; }
        public DbSet<AllSchedule> allSchedules { get; set; }
        public DbSet<RegistrationReq> registrationReqs { get; set; }
        public DbSet<DoctorReq> doctorReqs { get; set; }
        public DbSet<SystemAdmin> systemAdmins { get; set; }


        //public DbSet<CustomIdentityUserRole> customIdentityUserRoles { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();

            /*****/
            // base.OnModelCreating(modelBuilder);
            /*
             modelBuilder.Entity<IdentityUserRole<string>>()
                 .HasKey(r => new { r.UserId, r.RoleId });

             modelBuilder.Entity<IdentityRole<string>>()
                 .HasOne(r => r.Id)
                 .WithMany(r => r.UsersId)
                 .HasForeignKey(r => r.RoleId)
                 .IsRequired();

             modelBuilder.Entity<IdentityUserRole<string>>()
                 .HasOne(r => r.UserId)
                 .WithMany(u => u.Roles)
                 .HasForeignKey(r => r.UserId)
                 .IsRequired();
            */

            /******/

            modelBuilder.Entity<MedicalCentre>()
    .HasMany(m => m.doctorAdmins)
    .WithOne(d => d.medicalCentre)
    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MedicalCentre>()
.HasMany(m => m.guides)
.WithOne(d => d.medicalCentre)
.OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Appointment>()
        .HasOne<Patient>(x => x.patient)
        .WithMany(y => y.Appointments)
        .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Appointment>()
      .HasOne<DoctorAdmin>(x => x.doctorAdmin)
      .WithMany(y => y.Appointments)
      .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Appointment>()
                .HasOne<Guide>(x=>x.guide)
                .WithMany(y=>y.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<AllSchedule>()
               .HasOne<DoctorAdmin>(x => x.doctorAdmin)
               .WithOne(y => y.allSchedule)
               .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Appointment>()
                  .HasOne<Schedule>(x => x.schedule)
                  .WithOne(y => y.appointment)
                  .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Schedule>()
                .HasOne<AllSchedule>(x => x.allSchedule)
                .WithMany(y => y.schedules)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<DoctorAdmin>()
    .HasOne<MedicalCentre>(x => x.medicalCentre)
    .WithMany(y => y.doctorAdmins)
    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Guide>()
.HasOne<MedicalCentre>(x => x.medicalCentre)
.WithMany(y => y.guides)
.OnDelete(DeleteBehavior.ClientSetNull);

            //modelBuilder.Entity<CridetPayment>()
            //    .HasOne<Patient>(x=>x.patient)
            //    .WithOne(y=>y.cridet_payment)
            //    .HasForeignKey<Patient>(w=>w.CridetPaymentId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);
  
            //modelBuilder.Entity<Driver>()
            //    .HasOne<Fleet>(x => x.fleet)
            //    .WithOne(y => y.driver)
            //    .HasForeignKey<Fleet>(f => f.DriverId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);

        }

    }


    //modelBuilder.Entity<School>()
    //    .HasMany(s => s.transportationEntities)
    //    .WithMany(t => t.schools)
    //    .UsingEntity<TransportationEntityAndSchoolRel>(

    //    r => r
    //    .HasOne(x => x.transportationEntity)
    //    .WithMany(t => t.transportationEntityAndSchoolRels)
    //    .HasForeignKey(f => f.TransportationEntityId)
    //    .OnDelete(DeleteBehavior.ClientSetNull),

    //    r => r
    //    .HasOne(x => x.school)
    //    .WithMany(t => t.transportationEntityAndSchoolRels)
    //    .HasForeignKey(f => f.SchoolId)
    //    .OnDelete(DeleteBehavior.ClientSetNull)

    //    );



    //modelBuilder.Entity<Driver>()
    //    .HasOne<Fleet>(x => x.fleet)
    //    .WithOne(y => y.driver)
    //    .HasForeignKey<Fleet>(f => f.DriverId)
    //    .OnDelete(DeleteBehavior.ClientSetNull);

    //modelBuilder.Entity<Fleet>()
    //    .HasOne<TransportationEntity>(x => x.transportationEntity)
    //    .WithMany(y => y.fleets)
    //    .HasForeignKey(f => f.TransportationEntityId)
    //    .OnDelete(DeleteBehavior.ClientSetNull);




}
