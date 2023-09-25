using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Student_Panel_ITI.Controllers
{
    public class Student_SubmissionController : Controller
    {
        // GET: Student_SubmissionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: Student_SubmissionController/DetailsForManager/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Student_SubmissionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student_SubmissionController/Create
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

        // GET: Student_SubmissionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Student_SubmissionController/Edit/5
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

        // GET: Student_SubmissionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Student_SubmissionController/Delete/5
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
