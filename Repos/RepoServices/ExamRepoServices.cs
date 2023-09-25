using Student_Panel_ITI.Data;
using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Student_Panel_ITI.Repos.RepoServices
{
    public class ExamRepoServices : IExamRepository
    {
        private readonly IExam_QuestionRepository exam_QuestionRepository;
        private readonly IExam_Std_QuestionRepository exam_Std_QuestionRepository;

        public MainDBContext Context { get; set; }


        public ExamRepoServices(MainDBContext context, IExam_QuestionRepository exam_QuestionRepository ,IExam_Std_QuestionRepository exam_Std_QuestionRepository)
        {
            Context = context;
            this.exam_QuestionRepository = exam_QuestionRepository;
            this.exam_Std_QuestionRepository = exam_Std_QuestionRepository;
        }

        void IExamRepository.CreateExam(Exam exam)
        {
            Context.Exams.Add(exam);
            Context.SaveChanges();
        }


        void IExamRepository.DeleteExam(int examID)
        {

            var exam_records = exam_QuestionRepository.GetExamsRecordsbyExamID(examID);

            if(exam_records.Count == 0)
            {
                var exam = Context.Exams.FirstOrDefault(ex => ex.ID == examID);
                Context.Exams.Remove(exam);
                Context.SaveChanges();
            }

        }

        //Exam IExamRepository.GetExambyID(int examID)
        //{
        //    var exam = Context.Exams
        //        .Include(e => e.Instructor).ThenInclude(i => i.AspNetUser)
        //        .Include(e => e.Course)
        //        .Include(e => e.Exam_Question).ThenInclude(eq => eq.Exam)
        //        .FirstOrDefault(ex => ex.ID == examID); 

        //    return exam;
        //}

        Exam IExamRepository.GetExambyID(int examID)
        {
            var exam = Context.Exams
                .Include(e => e.Instructor).ThenInclude(i => i.AspNetUser)
                .Include(e => e.Course)
                .Include(e => e.Exam_Question)
                    .ThenInclude(eq => eq.Question)
                .FirstOrDefault(ex => ex.ID == examID);

            return exam;
        }

        List<Exam> IExamRepository.GetExams(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            return Context.Exams
                .Include(e => e.Instructor)
                .ThenInclude(i => i.AspNetUser)
                .Include(e => e.Course)
                .Include(e => e.Student_Quest_Exam)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }


        List<Exam> IExamRepository.GetExamsbycourseID(int courseID)
        {
            var exams = Context.Exams.Include(e => e.Instructor).ThenInclude(i => i.AspNetUser)
                                .Include(e => e.Course)
                                    .Where(e => e.CourseID == courseID)
                                       .Include(e => e.Student_Quest_Exam)
                                            .ToList();
            return exams;
        }

        int IExamRepository.GetExamNumbers()
        {
            return Context.Exams.Count();
        }        
        
        int IExamRepository.GetExamNumbersForCourse(int courseID)
        {
            return Context.Exams.Where(e=>e.CourseID==courseID).Count();
        }

       

        List<Exam> IExamRepository.GetExamsbyinstructorID(int instructorID)
        {
            var exams = Context.Exams.Where(e => e.InstructorID == instructorID.ToString()).ToList();
            return exams;
        }

        void IExamRepository.UpdateExam(int examID, Exam exam)
        {
            var exam_updated = Context.Exams.FirstOrDefault(e => e.ID == examID);
            exam_updated.Name = exam.Name;
            exam_updated.Duration = exam.Duration;
            exam_updated.CreationDate = exam.CreationDate;
            exam_updated.CourseID = exam.CourseID;
            exam_updated.InstructorID = exam.InstructorID;
            Context.SaveChanges();
        }



        //check null or na
        void IExamRepository.RemoveInstructor(string instructorID)
        {
            var exams = Context.Exams.Where(e => e.InstructorID == instructorID.ToString()).ToList();
            foreach (var exam in exams)
            {
                exam.InstructorID = null;
            }
            Context.SaveChanges();
        }
    }
}
