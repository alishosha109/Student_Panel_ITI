using Student_Panel_ITI.Models;

namespace Student_Panel_ITI.Repos
{
    public interface IIntakeRepository
    {
        public int getIntakeNumber();
        public Intake getIntakebyID(int intakeID);
        public List<Intake> GetIntakes();
        public void UpdateIntake(int IntakeID, Intake intake);
        public void DeleteIntake(int intakeID);
        public void DeleteIntake(List<int> intakeIDs);

        public void CreateIntake(Intake intake);
        public List<Intake> GetIntakesbyDuration(int duration, int pageNumber, int pageSize);




        //---------------------- asem_updates ---------------------------------------//

        public List<Intake> GetAllIntakes();
        public List<Intake> GetAllIntakes(int pageNumber, int pageSize);
        public List<Intake> GetCurrentAvIntakes();

    }
}
