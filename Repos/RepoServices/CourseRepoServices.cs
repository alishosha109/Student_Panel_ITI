using Student_Panel_ITI.Data;
using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Student_Panel_ITI.Repos
{
    public class CourseRepoServices : ICourseRepository
    {
        private readonly IIntake_Track_CourseRepository intake_track_CourseRepository;
        private readonly IInstructor_CourseRepository instructor_CourseRepository;
        private readonly IStudent_CourseRepository student_CourseRepository;
        private readonly ICourse_Day_MaterialRepository course_Day_MaterialRepository;
        private readonly ICourseDayRepository courseDayRepository;
        private readonly IMaterialRepository materialRepository;

        public MainDBContext Context { get; set; }

        public CourseRepoServices(MainDBContext context, IIntake_Track_CourseRepository Intake_track_CourseRepository, IInstructor_CourseRepository instructor_CourseRepository, IStudent_CourseRepository student_CourseRepository, ICourse_Day_MaterialRepository course_Day_MaterialRepository, ICourseDayRepository courseDayRepository, IMaterialRepository materialRepository)
        {
            Context = context;
            this.intake_track_CourseRepository = Intake_track_CourseRepository;
            this.instructor_CourseRepository = instructor_CourseRepository;
            this.student_CourseRepository = student_CourseRepository;
            this.course_Day_MaterialRepository = course_Day_MaterialRepository;
            this.courseDayRepository = courseDayRepository;
            this.materialRepository = materialRepository;
        }

        void ICourseRepository.CreateCourse(Course course)
        {
            Context.Courses.Add(course);
            Context.SaveChanges();
        }

        void ICourseRepository.DeleteCourse(int courseID)
        {

            // Delete track course records
            intake_track_CourseRepository.DeleteIntake_Track_CoursebyCourseID(courseID);

            // Delete Instructor Course records
            instructor_CourseRepository.DeleteInstructor_CourseByCourseID(courseID);

            // Delete Student Course records
            student_CourseRepository.DeleteStudent_CourseByCourseID(courseID);


            // Delete Course_Day_Materials, all CourseDays, all Materials
            course_Day_MaterialRepository.DeleteAllRelatedCourseDays_Materials(courseID);



            var course = Context.Courses.FirstOrDefault(c => c.ID == courseID);
            Context.Courses.Remove(course);
            Context.SaveChanges();
        }

        void ICourseRepository.DeleteCourse(List<int> courseIDs)
        {
            foreach (var id in courseIDs)
            {
                // Delete track course records
                intake_track_CourseRepository.DeleteIntake_Track_CoursebyCourseID(id);

                // Delete Instructor Course records
                instructor_CourseRepository.DeleteInstructor_CourseByCourseID(id);

                // Delete Student Course records
                student_CourseRepository.DeleteStudent_CourseByCourseID(id);


                // Delete Course_Day_Materials, all CourseDays, all Materials
                course_Day_MaterialRepository.DeleteAllRelatedCourseDays_Materials(id);



                var course = Context.Courses.FirstOrDefault(c => c.ID == id);
                Context.Courses.Remove(course);
            }

            
            Context.SaveChanges();
        }

        Course ICourseRepository.GetCoursebyID(int courseID)
        {
            var course = Context.Courses
                .Include(c => c.Admin)
                .Include(c => c.InstructorCourses)
                    .ThenInclude(cc=>cc.Instructor)
                .Include(c => c.IntakeTrackCourse)
                    .ThenInclude(itc => itc.Track)
                .FirstOrDefault(c => c.ID == courseID);

            if (course != null)
            {
                var uniqueTracks = course.IntakeTrackCourse
                    .Select(itc => itc.Track)
                    .Distinct()
                    .ToList();

                course.IntakeTrackCourse = uniqueTracks
                    .Select(track => new Intake_Track_Course
                    {
                        Track = track,
                        Course = course
                    })
                    .ToList();
            }

            return course;
        }

        int ICourseRepository.GetCourseNumber()
        {
            return Context.Courses.Count();
        }    
        
        int ICourseRepository.GetCourseNumberbyIntakeID(int intakeID)
        {
            return Context.Intake_Track_Courses.Where(itc=>itc.IntakeID == intakeID).Count();
        }

        List<Course> ICourseRepository.GetCourses(int pageNumber, int pageSize)
        {
            if(pageNumber < 1)
            {
                pageNumber =1;
            }
            var courses = Context.Courses
                .Include(c => c.Admin)
                .Include(c => c.InstructorCourses)
                .Include(c => c.IntakeTrackCourse)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return courses;
        }
        List<Course> ICourseRepository.GetCourses()
        {
            
            var courses = Context.Courses
                .Include(c => c.Admin)
                .Include(c => c.InstructorCourses)
                .Include(c => c.IntakeTrackCourse)
                .ToList();

            return courses;
        }

        void ICourseRepository.UpdateCourse(int CourseID, Course course)
        {
            var courseUpdated = Context.Courses.FirstOrDefault(c => c.ID == CourseID);
            courseUpdated.Name = course.Name;
            courseUpdated.Duration = course.Duration;
            courseUpdated.CreationDate = course.CreationDate;
            Context.SaveChanges();
        }

        public List<Course> GetCoursesByIntakeTrackID(int intakeID, int trackID)
        {
            var query =
                (from c in Context.Courses
                 join tc in Context.Intake_Track_Courses on c.ID equals tc.CourseID
                 where tc.TrackID == trackID && tc.IntakeID == intakeID
                 select c)
                 .Include(c => c.Admin)
                 .ThenInclude(a => a.AspNetUser)
                 .ToList();

            return query;
        }

        List<Course> ICourseRepository.GetCoursesbyTrackID(int trackID, int pageNumber, int pageSize)
        {
            var courses = Context.Intake_Track_Courses
            .Where(t => t.TrackID == trackID)
            .Select(t => t.Course)
            .Distinct()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

            return courses;

        }
        List<Course> ICourseRepository.GetCoursesForStudent(string studentID)
        {
            var courses = Context.Student_Courses
                .Where(sc => sc.StudentID == studentID)
                .Select(sc => sc.Course)
                .ToList();

            return courses;
        }

    }
}
