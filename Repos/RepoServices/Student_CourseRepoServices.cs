using Student_Panel_ITI.Data;
using Student_Panel_ITI.Models;
using Microsoft.EntityFrameworkCore;

namespace Student_Panel_ITI.Repos
{
    public class Student_CourseRepoServices : IStudent_CourseRepository
    {

        public MainDBContext Context { get; set; }


        public Student_CourseRepoServices(MainDBContext context)
        {
            Context = context;
        }

        void IStudent_CourseRepository.CreateStudent_Course(Student_Course student_course)
        {
            Context.Student_Courses.Add(student_course);
            Context.SaveChanges();
        }        
        
        void IStudent_CourseRepository.CreateStudent_Course(List<int> courseids, int studentid)
        {
            foreach (var id in courseids)
            {
                Student_Course sc = new Student_Course() { 
                    StudentID = studentid.ToString(),
                    CourseID = id
                };
                Context.Student_Courses.Add(sc);
            }
            Context.SaveChanges();
        }

        void IStudent_CourseRepository.DeleteStudent_Course(string studentID, int courseID)
        {
            var student_course = Context.Student_Courses.SingleOrDefault(sc=>sc.StudentID==studentID.ToString() && sc.CourseID==courseID);
            Context.Student_Courses.Remove(student_course);
            Context.SaveChanges();
        }
        void IStudent_CourseRepository.DeleteStudent_Course(string studentID)
        {
            var student_course = Context.Student_Courses.Where(sc=>sc.StudentID==studentID.ToString());
            foreach (var item in student_course)
            {
                Context.Student_Courses.Remove(item);
            }
            Context.SaveChanges() ;
        }   
        void IStudent_CourseRepository.DeleteStudent_CourseByCourseID(int courseID)
        {
            var student_course = Context.Student_Courses.Where(sc=>sc.CourseID== courseID);
            foreach (var item in student_course)
            {
                Context.Student_Courses.Remove(item);
            }
            Context.SaveChanges() ;
        }

        int IStudent_CourseRepository.GetStudent_CourseNumber(int courseID)
        {
            return Context.Student_Courses.Select(sc=>sc.CourseID == courseID).Count();
        }

        List<Student_Course> IStudent_CourseRepository.GetStudent_Courses()
        {
            return Context.Student_Courses.ToList();
        }

        List<Student_Course> IStudent_CourseRepository.GetStudent_CoursesbyCourseID(int courseID)
        {
            return Context.Student_Courses.Include(sc=>sc.Student).Where(sc=>sc.CourseID==courseID).ToList();
        }

        List<Student_Course> IStudent_CourseRepository.GetStudent_CoursesbyStudentID(int studentID)
        {
            return Context.Student_Courses.Include(sc => sc.Course).Where(sc=>sc.StudentID==studentID.ToString()).ToList();
        }

        void IStudent_CourseRepository.UpdateStudent_Course(int studentID, int courseID, Student_Course student_course)
        {
            var student_course_update = Context.Student_Courses.SingleOrDefault(sc => sc.StudentID == studentID.ToString() && sc.CourseID == courseID);
            student_course_update.StudentID = student_course.StudentID;
            student_course_update.CourseID = student_course.CourseID;
            Context.SaveChanges();
        }
    }
}
