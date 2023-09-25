using Student_Panel_ITI.Models;

namespace Student_Panel_ITI.Repos.Interfaces
{
    public interface ICourseDayRepository
    {
        public int GetCourseDaysNumber();
        public CourseDay GetCourseDaybyID(int courseDayID);
        public List<CourseDay> GetCourseDays();
        public void UpdateCourseDay(int courseDayID, CourseDay courseDay);
        public void DeleteCourseDay(int courseDayID);
        public void CreateCourseDay(CourseDay courseday);



        /*---------------------------------------------- Instructor Repos -----------------------------------------------*/
        public List<CourseDay> GetCourseDaysByCourseID(int courseID);
        //public void CreateCourseDay(CourseDay courseday);  used in Instructor Area, and exist above: Line 12



    }
}
