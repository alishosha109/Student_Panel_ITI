using Student_Panel_ITI.Data;
using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos.Interfaces;

namespace Student_Panel_ITI.Repos.RepoServices
{
    public class QuestionRepoServices : IQuestionRepository
    {
        private readonly IExam_Std_QuestionRepository exam_Std_QuestionRepository;
        private readonly IExam_QuestionRepository exam_QuestionRepository;

        public MainDBContext Context { get; set; }

        public QuestionRepoServices(MainDBContext context, IExam_Std_QuestionRepository exam_Std_QuestionRepository, IExam_QuestionRepository exam_QuestionRepository)
        {
            Context = context;
            this.exam_Std_QuestionRepository = exam_Std_QuestionRepository;
            this.exam_QuestionRepository = exam_QuestionRepository;
        }

        void IQuestionRepository.CreateQuestion(Question question)
        {
            Context.Questions.Add(question);
            Context.SaveChanges();
        }

        void IQuestionRepository.DeleteQuestion(int questionID)
        {
            var exams_related = exam_Std_QuestionRepository.GetExamsbyqid(questionID);

            if(exams_related.Count() == 0)
            {

                exam_QuestionRepository.DeleteExam_Question(questionID);

                var question = Context.Questions.FirstOrDefault(x => x.ID == questionID);
                Context.Questions.Remove(question);
                Context.SaveChanges();
            }

        }

        Question IQuestionRepository.getQuestionbyID(int questionID)
        {
            var question = Context.Questions.FirstOrDefault(x => x.ID == questionID);
            return question;
            
        }

        List<Question> IQuestionRepository.getQuestions()
        {
            return Context.Questions.ToList();
        }

  

        int IQuestionRepository.getQuestionsNumber()
        {
            return Context.Questions.Count();
        }

        void IQuestionRepository.UpdateQuestion(int questionID, Question question)
        {
            var question_updated = Context.Questions.FirstOrDefault(question => question.ID == questionID);
            question_updated.Type = question.Type;
            question_updated.Body = question.Body;
            question_updated.Mark = question.Mark;
            question_updated.Answer = question.Answer;
            Context.SaveChanges();
        }
    }
}
