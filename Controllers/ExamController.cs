using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos;
using Student_Panel_ITI.Repos.Interfaces;

namespace Admin_Panel_ITI.Areas.InstructorsArea.Controllers
{
    public class ExamController : Controller
    {
        private readonly IExamRepository examRepository;
        private readonly IStudentRepository studentRepository;
        private readonly UserManager<AppUser> userManager;

        public ExamController(IExamRepository examRepository, IStudentRepository studentRepository, UserManager<AppUser> userManager)
        {
            this.examRepository = examRepository;
            this.studentRepository = studentRepository;
            this.userManager = userManager;
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
            return View();
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
