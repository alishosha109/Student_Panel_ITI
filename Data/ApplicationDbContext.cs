using Student_Panel_ITI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Student_Panel_ITI.Data
{
    public class MainDBContext : IdentityDbContext<AppUser>
    {
        public MainDBContext(DbContextOptions<MainDBContext> options) : base(options)
        {

        }


        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseDay> CourseDays { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Instructor_Course> Instructor_Courses { get; set; }
        public virtual DbSet<Intake> Intakes { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Exam_Std_Question> Exam_Std_Questions { get; set; }
        public virtual DbSet<Exam_Question> Exam_Questions { get; set; }
        public virtual DbSet<Student_Course> Student_Courses { get; set; }
        public virtual DbSet<Student_Submission> Student_Submissions { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
        public virtual DbSet<Course_Day_Material> Course_Day_Materials { get; set; }
        public virtual DbSet<Intake_Track_Course> Intake_Track_Courses { get; set; }
        public virtual DbSet<Intake_Instructor> Intake_Instructors { get; set; }
        public virtual DbSet<Material> Materials { get; set; }



        // Define composite primary keys in the junction tables
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instructor_Course>()
                .HasKey(ic => new { ic.InstructorID, ic.CourseID });
            
            
            modelBuilder.Entity<Intake_Instructor>()
                .HasKey(ii => new { ii.InstructorID, ii.IntakeID });

            

            modelBuilder.Entity<Exam_Std_Question>()
                .HasKey(sqe => new { sqe.StudentID, sqe.QuestionID, sqe.ExamID });

            modelBuilder.Entity<Student_Course>()
                .HasKey(sc => new { sc.StudentID, sc.CourseID });


            modelBuilder.Entity<Course_Day_Material>()
                .HasKey(cdm => new { cdm.CourseID, cdm.CourseDayID, cdm.MaterialID });

            modelBuilder.Entity<Intake_Track_Course>()
                .HasKey(cdm => new { cdm.IntakeID, cdm.TrackID, cdm.CourseID });

            modelBuilder.Entity<Student_Submission>()
                .HasKey(ss => new { ss.StudentID, ss.CourseDayID });

            modelBuilder.Entity<Exam_Question>()
                .HasKey(eq => new { eq.ExamID, eq.QuestionID });





            //why this line?
            /*if u don't call this method from the base(IdentityDbContext) u will face the follwoing problem when mapping:
              " The entity type 'IdentityUserLogin<string>' requires a primary key to be defined". 

            - The Solution: 
            pasically the keys of Identity tables are mapped in OnModelCreating method of IdentityDbContext 
            and if this method is not called, you will end up getting the error that you got. 
            This method is not called if you derive from IdentityDbContext and provide your own definition of OnModelCreating as you did in your code.
            With this setup you have to explicitly call the OnModelCreating method of IdentityDbContext using "base.OnModelCreating" statement.
             */
            base.OnModelCreating(modelBuilder);
        }
    }
}