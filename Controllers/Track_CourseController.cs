using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Student_Panel_ITI.Controllers
{
    public class Track_CourseController : Controller
    {
        // GET: Track_CourseController
        public ActionResult Index()
        {
            return View();
        }

        // GET: Track_CourseController/DetailsForManager/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Track_CourseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Track_CourseController/Create
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

        // GET: Track_CourseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Track_CourseController/Edit/5
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

        // GET: Track_CourseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Track_CourseController/Delete/5
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
