using Student_Panel_ITI.Models;

namespace Student_Panel_ITI.Repos
{
    public interface IStudent_CourseRepository
    {


        public int GetStudent_CourseNumber(int courseID);



        public List<Student_Course> GetStudent_Courses();
        public List<Student_Course> GetStudent_CoursesbyCourseID(int courseID);
        public List<Student_Course> GetStudent_CoursesbyStudentID(int studentID);


        public void UpdateStudent_Course(int studentID, int courseID, Student_Course student_course);


        public void DeleteStudent_Course(string studentID, int courseID);
        public void DeleteStudent_CourseByCourseID(int courseID);
        public void DeleteStudent_Course(string studentID);


        public void CreateStudent_Course(Student_Course student_course);
        public void CreateStudent_Course(List<int> courseids, int studentid);
    }
}
