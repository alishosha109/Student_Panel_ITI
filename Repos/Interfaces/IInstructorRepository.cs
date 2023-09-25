using Student_Panel_ITI.Models;

namespace Student_Panel_ITI.Repos.Interfaces
{
    public interface IInstructorRepository
    {


        public int GetInstructorNumber();
        
        
        public int GetInstructorNumberbyIntakeID(int intakeID);



        public Instructor GetInstructorbyID(string instructorID);



        public List<Instructor> GetInstructors(int pageNumber, int pageSize);
        public List<Instructor> GetInstructors();


        public void UpdateInstructor(string instructorID, Instructor instructor);


        public void DeleteInstructor(string instructorID);
        public void DeleteInstructor(List<string> instructorIDs);


        public void CreateInstructor(Instructor instructor);
    }
}
