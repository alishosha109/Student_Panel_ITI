using Student_Panel_ITI.Models;

namespace Student_Panel_ITI.Repos.Interfaces
{
    public interface IIntake_Track_CourseRepository
    {
        //---//
        public List<Intake_Track_Course> GetTracksByIntakeID(int intakeID);

        public void DeleteIntake_Track_CoursebyCourseID(int courseID);
        public void DeleteIntake_Track_CoursebyTrackID(int trackID);
        public void DeleteIntake_Track_CoursebyIntakeID(int intakeID);

        public void CreateIntake_Track_Course(int intakeID, int trackID, int courseID);
    }
}
