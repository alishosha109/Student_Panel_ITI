using Student_Panel_ITI.Models;

namespace Student_Panel_ITI.Repos
{
    public interface IStudent_SubmissionRepository
    {


        public int GetStudent_SubmissionNumber();
        public int GetStudent_SubmissionNumber(int studentID);


        public Student_Submission GetStudent_Submission(int studentID,int courseDayID);

        public List<Student_Submission> GetStudent_SubmissionsByStdIDCrsDayID(int studentID,int courseDayID);
        public List<Student_Submission> GetStudent_SubmissionsByStdIDCrsDayID(int studentIDs);



        public List<Student_Submission> GetStudent_Submissions();


        public void UpdateStudent_Submission(int studentID,int courseID, Student_Submission student_Submission);



        public void DeleteStudent_Submission(string studentID, int courseID);
        public void DeleteStudent_Submission(string studentID);
        public void DeleteStudent_SubmissionbyCourseDayID(int courseDayID);


        public void CreateStudent_Submission(Student_Submission student_Submission);
        public void CreateStudent_Submission(int stdID, int crsDayID, string submissionPath);
    }
}
