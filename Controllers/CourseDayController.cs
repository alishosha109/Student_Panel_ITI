using Student_Panel_ITI.ViewModels;
using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos.Interfaces;
using Student_Panel_ITI.Repos.RepoServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Student_Panel_ITI.ViewModels;

namespace Student_Panel_ITI.Areas.InstructorsArea.Controllers
{
    [Area("InstructorsArea")] //have to be added(mandatory)
    public class CourseDayController : Controller
    {
        private readonly ICourseDayRepository courseDayRepo;
        private readonly IWebHostEnvironment webHostingEnvironment;
        private readonly UserManager<AppUser> userManager;
        private readonly IMaterialRepository materialRepo;
        private readonly ICourse_Day_MaterialRepository courseDayMaterialRepo;
        public CourseDayController(ICourseDayRepository _courseDayRepo, 
            IWebHostEnvironment _hostingEnvironment, 
            UserManager<AppUser> _userManager, 
            IMaterialRepository _materialRepo, 
            ICourse_Day_MaterialRepository _courseDayMaterialRepo)
        {
            courseDayRepo = _courseDayRepo;
            webHostingEnvironment = _hostingEnvironment;
            userManager = _userManager;
            materialRepo = _materialRepo;
            courseDayMaterialRepo = _courseDayMaterialRepo;
        }




        //id(courseID) , name(courseName)
        public ActionResult Index(int id , string name, int intakeID, int trackID, string intakeName, string trackName)
        {
            ViewBag.Id = id;    
            ViewBag.Name = name;  
            
            ViewBag.IntakeName = intakeName;    
            ViewBag.TrackName = trackName;    
            ViewBag.IntakeID = intakeID;    
            ViewBag.TrackID = trackID;    
            
            return View(courseDayRepo.GetCourseDaysByCourseID(id));
        }






        //id(course id) , name(course name)  ---> Materials & Tsak Table 
        public ActionResult Details(int id, string name, int intakeID, int trackID, string intakeName, string trackName , int coursedayID, int coursedayNum)
        {
            ViewBag.Id = id;
            ViewBag.Name = name;

            ViewBag.IntakeName = intakeName;
            ViewBag.TrackName = trackName;
            ViewBag.IntakeID = intakeID;
            ViewBag.TrackID = trackID;
            ViewBag.CourseDayId = coursedayID;
            ViewBag.CourseDayNum = coursedayNum;

            ViewBag.Materials = new List<IFormFile>();

            return View(courseDayMaterialRepo.GetCourseDaysbyCourseDayID(coursedayID));
        }


        [HttpPost]
        public ActionResult Details(List<IFormFile> Materials,  int id, string name, int intakeID, int trackID, string intakeName, string trackName, int coursedayID, int coursedayNum)
        {
            if (ModelState.IsValid)
            {
                string? uniqueFileName = null; //varaible to store the materials name after make it uniqe using GUID.
                string? instructorID = userManager.GetUserId(User);

                List<Material> materials = new();

                if (Materials != null)
                {
                    string MaterialsFilePath = Path.Combine(webHostingEnvironment.WebRootPath, "Materials"); //where the materials gonna be store(~/wwwroot/Materials/)
                    foreach (var material in Materials)
                    {
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + material.FileName; //give each material a uniqu name to prevent override Files
                        string MaterialPath = Path.Combine(MaterialsFilePath, uniqueFileName); //store the selected materials in "Materials" file(make string path: ~/wwwroot/Materials/filename.txt)

                        material.CopyTo(new FileStream(MaterialPath, FileMode.Create));


                        materials.Add(new Material() { Name = material.FileName.Split('.')[0], Path = MaterialPath, Type = Path.GetExtension(material.FileName), InstructorID = instructorID });
                    }

                    materialRepo.CreateMaterials(materials);


                    List<Course_Day_Material> cdms = new();

                    foreach (var material in materials)
                    {
                        cdms.Add(new Course_Day_Material() { CourseID = id, CourseDayID = coursedayID, MaterialID = material.ID });
                    }

                    courseDayMaterialRepo.CreateCourseDayMaterial(cdms);

                    return RedirectToAction(nameof(Details), new { id, name, intakeID, trackID, intakeName, trackName, coursedayID, coursedayNum });
                }
            }

            return  View();
              
        }






        [HttpGet]
        public ActionResult Create(int Id, string name, int intakeID, int trackID, string intakeName, string trackName)
        {
            ViewBag.Id = Id;
            ViewBag.Name = name;

            ViewBag.IntakeName = intakeName;
            ViewBag.TrackName = trackName;
            ViewBag.IntakeID = intakeID;
            ViewBag.TrackID = trackID;

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
