using Student_Panel_ITI.Data;
using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos.Interfaces;

namespace Student_Panel_ITI.Repos.RepoServices
{
    public class CourseDayRepoServices : ICourseDayRepository
    {
        private readonly ICourse_Day_MaterialRepository course_Day_MaterialRepository;
        private readonly IStudent_SubmissionRepository student_SubmissionRepository;

        public MainDBContext Context { get; set; }


        public CourseDayRepoServices(MainDBContext context, ICourse_Day_MaterialRepository course_Day_MaterialRepository, IStudent_SubmissionRepository student_SubmissionRepository)
        {
            Context = context;
            this.course_Day_MaterialRepository = course_Day_MaterialRepository;
            this.student_SubmissionRepository = student_SubmissionRepository;
        }

        void ICourseDayRepository.CreateCourseDay(CourseDay courseday)
        {
            Context.CourseDays.Add(courseday);
            Context.SaveChanges();
        }

        void ICourseDayRepository.DeleteCourseDay(int courseDayID)
        {

            //Delete courseDayMaterial Records
            course_Day_MaterialRepository.DeleteCourseDayMaterialbyCourseDayID(courseDayID);


            //Delete student Submission Records
            student_SubmissionRepository.DeleteStudent_SubmissionbyCourseDayID(courseDayID);


            
            var courseDay = Context.CourseDays.FirstOrDefault(cd => cd.ID == courseDayID);
            Context.CourseDays.Remove(courseDay);
            Context.SaveChanges();

        }

        CourseDay ICourseDayRepository.GetCourseDaybyID(int courseDayID)
        {
            var courseDay = Context.CourseDays.FirstOrDefault(cd => cd.ID == courseDayID);
            return courseDay;
        }

        List<CourseDay> ICourseDayRepository.GetCourseDays()
        {
            var courseDays = Context.CourseDays.ToList();
            return courseDays;
        }

        int ICourseDayRepository.GetCourseDaysNumber()
        {
            return Context.CourseDays.Count();
        }

        void ICourseDayRepository.UpdateCourseDay(int courseDayID, CourseDay courseDay)
        {
            var courseDayUpdated = Context.CourseDays.FirstOrDefault(cd => cd.ID == courseDayID);
            courseDayUpdated.DayNumber = courseDay.DayNumber;
            courseDayUpdated.Date = courseDay.Date;
            courseDayUpdated.TaskPath = courseDay.TaskPath;
            courseDayUpdated.DeadLine = courseDay.DeadLine;
            Context.SaveChanges();
        }




        /*---------------------------------------------- Instructor Services -----------------------------------------------*/
        public List<CourseDay> GetCourseDaysByCourseID(int courseID)
        {
            return Context.Course_Day_Materials
                    .Where(cdm => cdm.CourseID == courseID)
                    .Select(cdm => cdm.CourseDay)
                    .Distinct()
                    .ToList();
        }

    }
}
