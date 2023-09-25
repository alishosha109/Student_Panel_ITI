//using Student_Panel_ITI.Data;
//using Student_Panel_ITI.Models;

//namespace Student_Panel_ITI.Repos
//{
//    public class SubmissionRepoServices : ISubmissionRepository
//    {
//        private readonly IStudent_SubmissionRepository student_SubmissionRepository;

//        public MainDBContext Context { get; set; }

//        SubmissionRepoServices(MainDBContext context, IStudent_SubmissionRepository student_SubmissionRepository )
//        {
//            Context = context;
//            this.student_SubmissionRepository = student_SubmissionRepository;
//        }

//        void ISubmissionRepository.CreateSubmission(Submission submission)
//        {
//            Context.Submissions.Add(submission);
//            Context.SaveChanges();

//        }

//        void ISubmissionRepository.DeleteSubmission(int submissionID)
//        {

//            student_SubmissionRepository.DeleteStudent_Submission(submissionID);

//            var submission = Context.Submissions.FirstOrDefault(s=>s.ID == submissionID);
//            Context.Submissions.Remove(submission);
//            Context.SaveChanges();
//        }

//        Submission ISubmissionRepository.GetSubmissionbyID(int submissionID)
//        {
//            var submission = Context.Submissions.FirstOrDefault(s => s.ID == submissionID);
//            return submission;
//        }

//        int ISubmissionRepository.GetSubmissionNumber()
//        {
//            return Context.Submissions.Count();
//        }

//        List<Submission> ISubmissionRepository.GetSubmissions()
//        {
//            return Context.Submissions.ToList();
//        }

//        void ISubmissionRepository.UpdateSubmission(int submissionID, Submission submission)
//        {
//            var submission_updated = Context.Submissions.FirstOrDefault(s => s.ID == submissionID);
//            submission_updated.Date = submission.Date;
//            Context.SaveChanges();
//        }
//    }
//}
