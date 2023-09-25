using Student_Panel_ITI.Data;
using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Student_Panel_ITI.Repos
{
    public class IntakeRepoServices : IIntakeRepository
    {
        private readonly IStudentRepository studentRepository;
        private readonly IIntake_Track_CourseRepository intake_Track_CourseRepository;
        public MainDBContext Context { get; set; }
        public IntakeRepoServices(
            MainDBContext context,
            IStudentRepository studentRepository,
             IIntake_Track_CourseRepository intake_Track_CourseRepository)
        {
            Context = context;
            this.studentRepository = studentRepository;
            this.intake_Track_CourseRepository = intake_Track_CourseRepository;
        }

        void IIntakeRepository.CreateIntake(Intake intake)
        {
            Context.Intakes.Add(intake);
            Context.SaveChanges();
        }

        //void IIntakeRepository.DeleteIntake(int intakeID)
        //{
        //    var intake_students = studentRepository.getStudentsbyIntakeID(intakeID);

        //    if(intake_students.Count() == 0)
        //    {
        //        intake_TrackRepository.DeleteIntake_Track(intakeID);
        //        var intake = Context.IntakesIDs.FirstOrDefault(i => i.ID == intakeID);
        //        Context.IntakesIDs.Remove(intake);
        //    }
        //}


        public Intake getIntakebyID(int intakeID)
        {
            var intake = Context.Intakes.FirstOrDefault(i => i.ID == intakeID);
            return intake;
        }

        int IIntakeRepository.getIntakeNumber()
        {
            return Context.Intakes.Count();
        }

        List<Intake> IIntakeRepository.GetIntakes()
        {
            return Context.Intakes.Include(c => c.Admin)
                                 .Include(c => c.IntakeTrackCourse)
                                 .ToList();
        }



        void IIntakeRepository.UpdateIntake(int IntakeID, Intake intake)
        {
            var intakeUpdated = Context.Intakes.FirstOrDefault(i => i.ID == IntakeID);

            intakeUpdated.Name = intake.Name;
            intakeUpdated.StartDate = intake.StartDate;
            intakeUpdated.EndDate = intake.EndDate;
            intakeUpdated.Duration = intake.Duration;
            intakeUpdated.CreationDate = intake.CreationDate;

            Context.SaveChanges();
        }



        //---------------------- asem_updates ---------------------------------------//

        public List<Intake> GetAllIntakes()
        {
            return Context.Intakes.Include(i => i.Admin).ToList();
        }

        public List<Intake> GetCurrentAvIntakes()
        {
            return Context.Intakes.Where(i => i.EndDate >= DateTime.Now).ToList();
        }

        public List<Intake> getIntakesbyIDs(int[] intakeIDs)
        {
            List<Intake> selectedIntakes = new List<Intake>();
            foreach(int id in intakeIDs)
            {
                selectedIntakes.Add(getIntakebyID(id));
            }

            return selectedIntakes;
        }

        public List<Intake> GetAllIntakes(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            return Context.Intakes.Include(i => i.Admin)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToList();
        }


        public List<Intake> GetIntakesbyDuration(int duration, int pageNumber, int pageSize)
        {
            return Context.Intakes.Include(i => i.Admin)
                                  .Where(i=>i.Duration == duration)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToList();
        }





        void IIntakeRepository.DeleteIntake(int intakeID)
        {
            var intake_students = studentRepository.getStudentsbyIntakeID(intakeID);

            if (intake_students.Count() == 0)
            {
                //intake_TrackRepository.DeleteIntake_Track(intakeID);

                intake_Track_CourseRepository.DeleteIntake_Track_CoursebyIntakeID(intakeID);

                var intake = Context.Intakes.FirstOrDefault(i => i.ID == intakeID);
                Context.Intakes.Remove(intake);
                Context.SaveChanges();
            }
        }


        void IIntakeRepository.DeleteIntake(List<int> selectedIntakeIds)
        {

            foreach (var intakeID in selectedIntakeIds)
            {
                var intake_students = studentRepository.getStudentsbyIntakeID(intakeID);

                if (intake_students.Count() == 0)
                {
                    //intake_TrackRepository.DeleteIntake_Track(intakeID);

                    intake_Track_CourseRepository.DeleteIntake_Track_CoursebyIntakeID(intakeID);

                    var intake = Context.Intakes.FirstOrDefault(i => i.ID == intakeID);
                    Context.Intakes.Remove(intake);
                }
            }
            Context.SaveChanges();

        }
    }
}