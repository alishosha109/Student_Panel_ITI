using Student_Panel_ITI.Models;

namespace Student_Panel_ITI.Repos
{
    public interface ICourseRepository
    {
        public int GetCourseNumber();
        public int GetCourseNumberbyIntakeID(int intakeID);
        public Course GetCoursebyID(int courseID);
        public List<Course> GetCourses(int pageNumber, int pageSize);
        public List<Course> GetCoursesbyTrackID(int trackID, int pageNumber, int pageSize);
        //public List<Course> GetCoursesbyTrackID(int trackID,int pageNumber, int pageSize);
        public List<Course> GetCourses();
        public void UpdateCourse(int CourseID, Course course);
        public void DeleteCourse(int courseID);
        public void CreateCourse(Course course);


        public void DeleteCourse(List<int> courseids);


        /*---------------------------------------------- Instructor Repos -----------------------------------------------*/
        public List<Course> GetCoursesByIntakeTrackID(int intakeID, int trackID);
        public List<Course> GetCoursesForStudent(string studentID);
    }
}
