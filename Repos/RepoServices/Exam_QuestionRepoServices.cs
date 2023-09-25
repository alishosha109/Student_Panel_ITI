using Student_Panel_ITI.Data;
using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Student_Panel_ITI.Repos.RepoServices
{
    public class Exam_QuestionRepoServices : IExam_QuestionRepository
    {

        public MainDBContext Context { get; set; }


        public Exam_QuestionRepoServices(MainDBContext context)
        {
            Context = context;
        }

        void IExam_QuestionRepository.CreateExam_Question(Exam_Question exam_question)
        {
            Context.Exam_Questions.Add(exam_question);
            Context.SaveChanges();
        }        

        void IExam_QuestionRepository.CreateExam_QuestionForQuestions(List<int> qIds, int examID)
        {
            foreach (var qid in qIds)
            {
                Exam_Question eq = new Exam_Question() {
                    QuestionID = qid,
                    ExamID = examID
                };
                Context.Exam_Questions.Add(eq);
            }
            Context.SaveChanges();
        }


        void IExam_QuestionRepository.CreateExam_QuestionForExams(List<int> examIDs, int qid)
        {
            foreach (var exid in examIDs)
            {
                Exam_Question eq = new Exam_Question()
                {
                    QuestionID = qid,
                    ExamID = exid
                };
                Context.Exam_Questions.Add(eq);
            }
            Context.SaveChanges();
        }

      



        void IExam_QuestionRepository.CreateExam_Question(List<Exam_Question> exam_question)
        {
            foreach (var item in exam_question)
            {
                Context.Exam_Questions.Add(item);

            }
            Context.SaveChanges();
        }

        List<Exam_Question> IExam_QuestionRepository.GetExamsRecordsbyExamID(int exID)
        {
            return Context.Exam_Questions.Where(es => es.ExamID == exID).Include(e=>e.Question).ToList();
        }



        void IExam_QuestionRepository.DeleteExam_Question(int examID, int questionID)
        {
            var exam_question = Context.Exam_Questions.SingleOrDefault(eq => eq.ExamID == examID && eq.QuestionID == questionID);
            Context.Exam_Questions.Remove(exam_question);
            Context.SaveChanges();
        }

        void IExam_QuestionRepository.DeleteExam_Question(int questionID)
        {
            var exam_question = Context.Exam_Questions.Where(eq=>eq.QuestionID == questionID);
            foreach (var item in exam_question)
            {
                Context.Exam_Questions.Remove(item);
            }
            Context.SaveChanges();
        }
        List<Exam_Question> IExam_QuestionRepository.GetExambyqid(int qid)
        {
            var exam_question = Context.Exam_Questions.Include( eq=>eq.Exam).Where(eq => eq.QuestionID == qid).ToList();
            return exam_question;
        }

        int IExam_QuestionRepository.GetQuestionNumbersInExam(int examiD)
        {
            return Context.Exam_Questions.Where(eq => eq.ExamID == examiD).Count();

        }
    }
}
