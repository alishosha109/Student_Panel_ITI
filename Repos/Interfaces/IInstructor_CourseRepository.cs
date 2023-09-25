using Student_Panel_ITI.Data;
using Student_Panel_ITI.Models;

namespace Student_Panel_ITI.Repos.Interfaces
{
    public interface IInstructor_CourseRepository
    {
        public int GetInstructor_CourseNumber();
        public List<Instructor_Course> GetInstructorsbyCourseID(int courseID);
        public List<Instructor_Course> GetCoursesbyInstructorID(int instructorID);
        public List<Instructor_Course> GetInstructor_Courses();
        public void UpdateInstructor_Course(int CourseID, int intstructorID, Instructor_Course instructor_course);


        //public void DeleteInstructor_Course(int courseID, int instructorID);
        public void DeleteInstructor_Course( string instructorID);
        public void DeleteInstructor_CourseByCourseID( int courseID);
        public void DeleteInstructor_Course(int courseID, string instructorID);
        public void CreateInstructor_Course(Instructor_Course instructor_course);
        public void CreateInstructor_Course(List<int> courseIds, int instructorId);
        public int GetInstructor_CourseNumberbyInstructorID(int instructorID);
        public int GetInstructor_CourseNumberbyCourseID(int courseID);



        /*---------------------------------------------- Instructor Repos -----------------------------------------------*/

        public Instructor_Course GetInstructorTeachCourse(int courseId);

    }
}
