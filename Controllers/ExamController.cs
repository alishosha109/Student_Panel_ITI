using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos;
using Student_Panel_ITI.Repos.Interfaces;
using Newtonsoft.Json;


namespace Admin_Panel_ITI.Areas.InstructorsArea.Controllers
{
    public class ExamController : Controller
    {
        private readonly IExamRepository examRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IExam_QuestionRepository exam_QuestionRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly IExam_Std_QuestionRepository exam_Std_QuestionRepository;
        private readonly IQuestionRepository questionRepository;

        public ExamController(IExamRepository examRepository, IStudentRepository studentRepository, IExam_QuestionRepository exam_QuestionRepository, UserManager<AppUser> userManager, IExam_Std_QuestionRepository exam_Std_QuestionRepository, IQuestionRepository questionRepository)
        {
            this.examRepository = examRepository;
            this.studentRepository = studentRepository;
            this.exam_QuestionRepository = exam_QuestionRepository;
            this.userManager = userManager;
            this.exam_Std_QuestionRepository = exam_Std_QuestionRepository;
            this.questionRepository = questionRepository;
        }
        // GET: ExamController
        public ActionResult Index()
        {
            var student = studentRepository.getStdbyID(userManager.GetUserId(User));
            ViewBag.trackName = student.Track.Name;
            return View(examRepository.GetExams(student.IntakeID, student.TrackID));
        }

        // GET: ExamController/Details/5
        public ActionResult Details(int id)
        {
            string examName = examRepository.GetExambyID(id).Name;
            ViewBag.examName = examName;
            ViewBag.examId = id;
            return View(exam_QuestionRepository.GetExamsRecordsbyExamID(id));
            //return View(exam_Std_QuestionRepository.GetExambyExamID(id));
        }

        [HttpPost]
        public ActionResult Details(IFormCollection form)
        {
            string studentResults = form["studentResults"];
            string questionIds = form["parentIds"];
            string examId = form["examId"];
            var studentId = userManager.GetUserId(User);
    
            int[] questionsIds = JsonConvert.DeserializeObject<int[]>(questionIds);
            string[] studentsResults = JsonConvert.DeserializeObject<string[]>(studentResults);

            for(int i = 0; i< studentsResults.Length; i++)
            {
                Question ques = questionRepository.getQuestionbyID(questionsIds[i]);
                string correctAns = ques.Answer;
                string stdAns = studentsResults[i];


                if(correctAns.Trim() == stdAns.Trim())
                {
                    Exam_Std_Question exam_Std_Question = new Exam_Std_Question()
                    {
                        ExamID = int.Parse(examId),
                        QuestionID = questionsIds[i],
                        StudentID = studentId,
                        StudentAnswer = stdAns,
                        StudentGrade = ques.Mark
                    };
                    exam_Std_QuestionRepository.CreateExam_Std_Question(exam_Std_Question);
                }
            }

            var student = studentRepository.getStdbyID(userManager.GetUserId(User));
            ViewBag.trackName = student.Track.Name;
            return View("Index", examRepository.GetExams(student.IntakeID, student.TrackID));
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }




        // GET: ExamController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExamController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExamController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ExamController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
