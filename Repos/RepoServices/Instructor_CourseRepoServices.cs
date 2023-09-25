using Student_Panel_ITI.Data;
using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Student_Panel_ITI.Repos.RepoServices
{
    public class Instructor_CourseRepoServices : IInstructor_CourseRepository
    {
        public MainDBContext Context { get; set; }

        public Instructor_CourseRepoServices(MainDBContext context)
        {
            Context = context;
        }

        void IInstructor_CourseRepository.CreateInstructor_Course(Instructor_Course instructor_course)
        {
            Context.Instructor_Courses.Add(instructor_course);
            Context.SaveChanges();
        }


        void IInstructor_CourseRepository.CreateInstructor_Course(List<int> courseIds, int instructorId)
        {

            foreach (var id in courseIds)
            {
                Instructor_Course inc = new Instructor_Course() { 
                    CourseID = id,
                    InstructorID = instructorId.ToString(),

                };
                Context.Instructor_Courses.Add(inc);
            }
            Context.SaveChanges();
        }

        void IInstructor_CourseRepository.DeleteInstructor_Course(int courseID, string instructorID)
        {
            var instructor_course = Context.Instructor_Courses.SingleOrDefault(ic=>ic.CourseID==courseID && ic.InstructorID == instructorID);
            Context.Instructor_Courses.Remove(instructor_course);
            Context.SaveChanges();

        }

        void IInstructor_CourseRepository.DeleteInstructor_CourseByCourseID(int courseID)
        {
            var instructor_course = Context.Instructor_Courses.Where(ic => ic.CourseID == courseID).ToList();
            foreach (var item in instructor_course)
            {
                Context.Instructor_Courses.Remove(item);
            }
            Context.SaveChanges();
        }

        List<Instructor_Course> IInstructor_CourseRepository.GetCoursesbyInstructorID(int instructorID)
        {
            var instructor_courses = Context.Instructor_Courses.Where(ic=>ic.InstructorID==instructorID.ToString()).Include(c=>c.Course).ToList();
            return instructor_courses;
        }

        List<Instructor_Course> IInstructor_CourseRepository.GetInstructorsbyCourseID(int courseID)
        {
            var instructor_courses = Context.Instructor_Courses.Where(ic => ic.CourseID == courseID).Include(c => c.Instructor).ToList();
            return instructor_courses;
        }

       

        int IInstructor_CourseRepository.GetInstructor_CourseNumber()
        {
            return Context.Instructor_Courses.Count();
        }        
        
        int IInstructor_CourseRepository.GetInstructor_CourseNumberbyInstructorID(int instructorID)
        {
            return Context.Instructor_Courses.Where(ic=>ic.InstructorID==instructorID.ToString()).ToList().Count(); 
        }        
        int IInstructor_CourseRepository.GetInstructor_CourseNumberbyCourseID(int courseID)
        {
            return Context.Instructor_Courses.Where(ic=>ic.CourseID== courseID).ToList().Count(); 
        }

        List<Instructor_Course> IInstructor_CourseRepository.GetInstructor_Courses()
        {
            return Context.Instructor_Courses.ToList();
        }

        void IInstructor_CourseRepository.UpdateInstructor_Course(int CourseID, int instructorID, Instructor_Course instructor_course)
        {
            var instructor_course_updated = Context.Instructor_Courses.SingleOrDefault(ic=>ic.CourseID == CourseID && ic.InstructorID == instructorID.ToString());
            instructor_course_updated.CourseID = instructor_course.CourseID;
            instructor_course_updated.InstructorID = instructor_course.InstructorID;
            Context.SaveChanges();
        }

        void IInstructor_CourseRepository.DeleteInstructor_Course(string instructorID)
        {
            var records = Context.Instructor_Courses.Where(ins => ins.InstructorID == instructorID.ToString()).ToList();
            foreach (var item in records)
            {
                Context.Instructor_Courses.Remove(item);
            }
            Context.SaveChanges();
        }




        /*---------------------------------------------- Instructor Services -----------------------------------------------*/

        public Instructor_Course GetInstructorTeachCourse(int courseId)
        {
            var result = Context.Instructor_Courses.Where(ic => ic.CourseID == courseId).Include(ic => ic.Instructor).ThenInclude(i => i.AspNetUser).FirstOrDefault();
            
            if (result != null)
                return result;

            return null;
        }
    }
}
