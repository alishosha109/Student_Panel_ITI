using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Student_Panel_ITI.Controllers
{
    public class Course_Day_MaterialController : Controller
    {
        // GET: Course_Day_MaterialController
        public ActionResult Index()
        {
            return View();
        }

        // GET: Course_Day_MaterialController/DetailsForManager/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Course_Day_MaterialController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Course_Day_MaterialController/Create
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

        // GET: Course_Day_MaterialController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Course_Day_MaterialController/Edit/5
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

        // GET: Course_Day_MaterialController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Course_Day_MaterialController/Delete/5
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
