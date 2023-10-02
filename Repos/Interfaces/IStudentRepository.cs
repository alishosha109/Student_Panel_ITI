using Student_Panel_ITI.Models;

namespace Student_Panel_ITI.Repos
{
    public interface IStudentRepository
    {
        public int getStudentNumber();
        public int getStudentNumberbyIntakeID(int intakeID);
        public int getStudentNumberbyTrackID(int trackID);
        



        public Student getStudentbyID(string studentbyID);

        public Student getStdbyID(string id);
        public List<Student> getStudents(int pageNumber, int pageSize);
        public void UpdateStudent(string studentID, Student student);


        public void DeleteStudent(string studentID);
        public void DeleteStudent(List<string> studentIDs);
        public List<Student> getStudentsbyTrackID(int trackid, int pageNumber, int pageSize);
        public List<Student> getStudentsbyTrackID(int trackID);
        public void CreateStudent(Student student);
        public List<Student> getStudentsbyIntakeID(int intakeID, int pageNumber, int pageSize);
        public List<Student> getStudentsbyIntakeID(int intakeID);
        public List<int> getStudentNumberforIntakes(List<Intake> intakes);



        /*---------------------------------------------- Instructor Repos -----------------------------------------------*/
        public int GetStudentsNumberbyIntakeTrackID(int intakeID, int trackID);
        public List<Student> GetStudentsbyIntakeTrackID(int intakeID, int trackID);

    }
}
