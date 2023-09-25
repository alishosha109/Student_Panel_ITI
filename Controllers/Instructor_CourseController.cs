using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Student_Panel_ITI.Controllers
{
    public class Instructor_CourseController : Controller
    {
        // GET: Instructor_CourseController
        public ActionResult Index()
        {
            return View();
        }

        // GET: Instructor_CourseController/DetailsForManager/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Instructor_CourseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Instructor_CourseController/Create
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

        // GET: Instructor_CourseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Instructor_CourseController/Edit/5
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

        // GET: Instructor_CourseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Instructor_CourseController/Delete/5
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
