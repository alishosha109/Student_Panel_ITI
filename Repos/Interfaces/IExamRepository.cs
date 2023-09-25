using Student_Panel_ITI.Models;

namespace Student_Panel_ITI.Repos.Interfaces
{
    public interface IExamRepository
    {
        public int GetExamNumbers();


        public Exam GetExambyID(int examID);


        public List<Exam> GetExamsbyinstructorID(int instructorID);
        public List<Exam> GetExamsbycourseID(int courseID);


        public List<Exam> GetExams(int pageNumber, int pageSize);


        public void UpdateExam(int examID, Exam exam);


        public void DeleteExam(int examID);


        public void RemoveInstructor(string instructorID);
        public void CreateExam(Exam exam);
        public int GetExamNumbersForCourse(int courseID);
    }
}
