using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.School.DAL.Data.Identity;
using SchoolSystem.School.DAL.Data.Models.Admin;
using SchoolSystem.School.DAL.Data.Models.Career;
using SchoolSystem.School.DAL.Data.Models.Students;
using SchoolSystem.School.DAL.Data.Models.TeachersAdmission;

namespace SchoolSystem.School.DAL.Data.Context
{
    public class SchoolContext : IdentityDbContext<AppUser>
    {

        public DbSet<StudentAdmission> studentAdmissions => Set<StudentAdmission>();
        public DbSet<TeacherInfoAdmission> teacherInfoAdmissions => Set<TeacherInfoAdmission>();
      
        public DbSet<PersonalInformation> personalInformation => Set<PersonalInformation>();
        public DbSet<JobData> jobdatas => Set<JobData>();
        public DbSet<Admin> Admins => Set<Admin>();



        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<JobData>()
            .HasMany(d => d.personalInformation)
            .WithOne(p => p.jobData)
            .HasForeignKey(p => p.JobDataid);





            base.OnModelCreating(modelBuilder);





        }


    }
}
