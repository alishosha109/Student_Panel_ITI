using Student_Panel_ITI.ViewModels;
using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos.Interfaces;
using Student_Panel_ITI.Repos.RepoServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Student_Panel_ITI.ViewModels;
using Student_Panel_ITI.Repos;

namespace Student_Panel_ITI.Areas.InstructorsArea.Controllers
{
    public class CourseDayController : Controller
    {
        private readonly ICourseDayRepository courseDayRepo;
        private readonly IWebHostEnvironment webHostingEnvironment;
        private readonly UserManager<AppUser> userManager;
        private readonly IMaterialRepository materialRepo;
        private readonly ICourse_Day_MaterialRepository courseDayMaterialRepo;
        private readonly IStudent_SubmissionRepository studentSubmissionRepo;
        public CourseDayController(ICourseDayRepository _courseDayRepo, 
            IWebHostEnvironment _hostingEnvironment, 
            UserManager<AppUser> _userManager, 
            IMaterialRepository _materialRepo, 
            ICourse_Day_MaterialRepository _courseDayMaterialRepo,
            IStudent_SubmissionRepository _studentSubmissionRepo)
        {
            courseDayRepo = _courseDayRepo;
            webHostingEnvironment = _hostingEnvironment;
            
            userManager = _userManager;
            materialRepo = _materialRepo;
            courseDayMaterialRepo = _courseDayMaterialRepo;
            studentSubmissionRepo = _studentSubmissionRepo;
        }




        //id(courseID) , name(courseName)
        public ActionResult Index(int id , string name)
        {   
            ViewBag.Id = id;    
            ViewBag.Name = name;  
             
            
            return View(courseDayRepo.GetCourseDaysByCourseID(id));
        }






        //id(course id) , name(course name)  ---> Materials & Tsak Table 
        public ActionResult Details(int id, string name, int coursedayID, int coursedayNum)
        {
            var courseDay = courseDayRepo.GetCourseDaybyID(id);
            ViewBag.CourseDay = courseDay;

            ViewBag.Id = id;
            ViewBag.Name = name;

            
            ViewBag.CourseDayId = coursedayID;
            ViewBag.CourseDayNum = coursedayNum;

            ViewBag.Submissions = new List<IFormFile>();

            string studentID = userManager.GetUserId(User);

            var submissions = studentSubmissionRepo.GetStudent_SubmissionsByStdIDCrsDayID(studentID, coursedayID);

            if (submissions)
            {
                ViewBag.hassubmit = true;

            }
            else
            {
                ViewBag.hassubmit = false;


            }

            return View(courseDayMaterialRepo.GetCourseDaysbyCourseDayID(coursedayID));
        }


        [HttpPost]
        public ActionResult UploadSubmissions(int id, string name, int coursedayID, int coursedayNum)
        {
            if (ModelState.IsValid)
            {
                var Submissions = Request.Form.Files;

                string? uniqueFileName = null; //varaible to store the materials name after make it uniqe using GUID.
                string? userID = userManager.GetUserId(User);

                List<Student_Submission> submissions = new();

                if (Submissions != null)
                {
                    string SubmissionsFilePath = Path.Combine(webHostingEnvironment.WebRootPath, "Materials"); //where the materials gonna be store(~/wwwroot/Materials/)
                    foreach (var submission in Submissions)
                    {
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + submission.FileName; //give each material a uniqu name to prevent override Files
                        string SubmissionPath = Path.Combine(SubmissionsFilePath, uniqueFileName); //store the selected materials in "Materials" file(make string path: ~/wwwroot/Materials/filename.txt)

                        submission.CopyTo(new FileStream(SubmissionPath, FileMode.Create));


                        submissions.Add(new Student_Submission() { CourseDayID = coursedayID, SubmissionPath = SubmissionPath, SubmissionGrade = 0, StudentID = userID });
                    }

                    studentSubmissionRepo.CreateStudent_Submission(submissions);


                    var courseDay = courseDayRepo.GetCourseDaybyID(id);
                    ViewBag.Id = id;
                    ViewBag.Name = name;

                    ViewBag.CourseDay = courseDay;
                    ViewBag.CourseDayId = coursedayID;
                    ViewBag.CourseDayNum = coursedayNum;

                    ViewBag.Submissions = new List<IFormFile>();

                    return RedirectToAction(nameof(Details), new { id, name, coursedayID, coursedayNum });
                }
            }

            return View();

        }







        public ActionResult Edit(int id)
        {
            return View();
        }


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






        public ActionResult Delete(int id)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete()
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
