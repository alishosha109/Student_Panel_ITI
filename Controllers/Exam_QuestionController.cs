using Student_Panel_ITI.Repos.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Student_Panel_ITI.Controllers
{
    public class Exam_QuestionController : Controller
    {
        private readonly IExam_QuestionRepository exam_QuestionRepository;

        public Exam_QuestionController(IExam_QuestionRepository exam_QuestionRepository)
        {
            this.exam_QuestionRepository = exam_QuestionRepository;
        }
        // GET: Exam_Std_QuestionController
        //public ActionResult Index(int examid , int pageNumber)
        //{

        //    var exam_Questions = exam_QuestionRepository.GetQuestionsbyExamId( examid, pageNumber, 10);
        //    return View(exam_Questions);
        //}

        // GET: Exam_Std_QuestionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Exam_Std_QuestionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Exam_Std_QuestionController/Create
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

        // GET: Exam_Std_QuestionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Exam_Std_QuestionController/Edit/5
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

        // GET: Exam_Std_QuestionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Exam_Std_QuestionController/Delete/5
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
