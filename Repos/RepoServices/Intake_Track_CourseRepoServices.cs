using Student_Panel_ITI.Data;
using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos.Interfaces;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace Student_Panel_ITI.Repos.RepoServices
{
    public class Intake_Track_CourseRepoServices : IIntake_Track_CourseRepository
    {
        public MainDBContext Context { get; set; }

        public Intake_Track_CourseRepoServices(MainDBContext context)
        {
            Context = context;
        }
        void IIntake_Track_CourseRepository.DeleteIntake_Track_CoursebyCourseID(int courseID)
        {
            var records = Context.Intake_Track_Courses.Where(itc => itc.CourseID == courseID).ToList();
            foreach (var record in records)
            {
                Context.Intake_Track_Courses.Remove(record);
            }
            Context.SaveChanges();
        }

        void IIntake_Track_CourseRepository.DeleteIntake_Track_CoursebyTrackID(int trackID)
        {
            var records = Context.Intake_Track_Courses.Where(itc => itc.TrackID == trackID).ToList();
            Context.Intake_Track_Courses.RemoveRange(records);
        }


        void IIntake_Track_CourseRepository.DeleteIntake_Track_CoursebyIntakeID(int intakeID)
        {
            var records = Context.Intake_Track_Courses.Where(itc => itc.IntakeID == intakeID).ToList();
            Context.Intake_Track_Courses.RemoveRange(records);

        }

       


        //---//
        public List<Intake_Track_Course> GetTracksByIntakeID(int intakeID)
        {
            return Context.Intake_Track_Courses
                .Where(i => i.IntakeID == intakeID)
                .Include(i => i.Intake)
                .Include(i=> i.Track)
                .ToList();
        }


        void IIntake_Track_CourseRepository.CreateIntake_Track_Course(int intakeID, int trackID, int courseID)
        {
            Intake_Track_Course itc = new Intake_Track_Course()
            {
                IntakeID = intakeID,
                TrackID = trackID,
                CourseID = courseID,

            };
            Context.Intake_Track_Courses.Add(itc);
            Context.SaveChanges();
        }

        

    }
}
