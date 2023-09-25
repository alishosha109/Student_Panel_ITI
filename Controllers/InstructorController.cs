using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos;
using Student_Panel_ITI.Repos.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Student_Panel_ITI.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorRepository instructorRepository;
        private readonly ICourseRepository courseRepository;
        private readonly IInstructor_CourseRepository instructor_CourseRepository;
        private readonly IIntakeRepository intakeRepository;
        private readonly IIntake_InstructorRepository intake_InstructorRepository;
        private readonly UserManager<AppUser> _userManager;


        public InstructorController(IInstructorRepository instructorRepository, UserManager<AppUser> userManager, ICourseRepository courseRepository, IInstructor_CourseRepository instructor_CourseRepository, IIntakeRepository intakeRepository, IIntake_InstructorRepository intake_InstructorRepository) {
            this.instructorRepository = instructorRepository;
            this.courseRepository = courseRepository;
            this.instructor_CourseRepository = instructor_CourseRepository;
            this.intakeRepository = intakeRepository;
            this.intake_InstructorRepository = intake_InstructorRepository;
            _userManager = userManager;
        }
        // GET: InstructorController
        public ActionResult Index(int pageNumber)
        {
            var instructors = instructorRepository.GetInstructors(pageNumber, 10);
            ViewBag.PageNumber = pageNumber;
            return View(instructors);
        }

        public ActionResult UpdateTableData(int pageNumber)
        {


            var instructors = instructorRepository.GetInstructors(pageNumber, 10);
            if (instructors.Count == 0 && pageNumber > 1)
            {
                instructors = instructorRepository.GetInstructors(pageNumber - 1, 10);
                pageNumber--;
            }


            ViewBag.PageNumber = pageNumber;
            return PartialView("_TableDataPartial", instructors);
        }





        //public ActionResult Details(string id)
        //{
        //    var instructor = instructorRepository.GetInstructorbyID(id);
        //    ViewData["tracks"] = instructor.Tracks.Select(t => t.Name).Distinct().ToList();
        //    ViewData["intakes"] = instructor.IntakeInstructors
        //            .Select(itc => itc.Intake.Name)
        //            .Distinct()
        //            .ToList();
        //    ViewData["courses"] = instructor.InstructorCourses
        //            .Select(itc => itc.Course.Name)
        //            .Distinct()
        //            .ToList();

        //    return View(instructor);
        //}

        // GET: InstructorController/Details/5
        //public ActionResult Details(string id)
        //{
        //    var instructor = instructorRepository.GetInstructorbyID(id);
        //    ViewData["tracks"] = instructor.Tracks.Select(t=>t.Name).Distinct().ToList();
        //    ViewData["intakes"] = instructor.IntakeInstructors
        //            .Select(itc => itc.Intake.Name)
        //            .Distinct()
        //            .ToList();
        //    ViewData["courses"] = instructor.InstructorCourses
        //            .Select(itc => itc.Course.Name)
        //            .Distinct()
        //            .ToList();

        //    return View(instructor);
        //}

        // GET: TrackController/Create
        //public ActionResult Create()
        //{

        //    var intakes = intakeRepository.GetAllIntakes();
        //    ViewBag.AllIntakes = new SelectList(intakes, "ID", "Name");
        //    return View();
        //}




        ////POST: TrackController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //async public Task<ActionResult> Create(Instructor instructor)
        //{
        //    var intakes = intakeRepository.GetAllIntakes();
        //    ViewBag.AllIntakes = new SelectList(intakes, "ID", "Name");
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user != null)
        //    {
        //        string userId = user.Id;
        //        instructor.AdminID = "admin1";
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        instructorRepository.CreateInstructor(instructor);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(instructor);
        //}



        // GET: InstructorController/Edit/5
        public ActionResult Edit(string id)
        {
            var instructor = instructorRepository.GetInstructorbyID(id);
            var intakes = intakeRepository.GetAllIntakes();
            List<Intake> intakessSelected = new List<Intake>();
            foreach (var item in instructor.IntakeInstructors)
            {
                intakessSelected.Add(item.Intake);
            }
            ViewBag.SelectedIntakes = intakessSelected;
            ViewBag.AllIntakes = new SelectList(intakes.Except(intakessSelected), "ID", "Name");
            return View(instructor);
        }

        // POST: InstructorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Instructor instructor, List<string> SelectedIntakeIds)
        {
            if (ModelState.IsValid)
            {

                foreach (var item in SelectedIntakeIds)
                {
                    Intake_Instructor ins = new Intake_Instructor()
                    {
                        IntakeID = int.Parse(item),
                        InstructorID = id
                    };
                    intake_InstructorRepository.AddIntake_Instructor(ins);

                }

                instructorRepository.UpdateInstructor(id, instructor);

                ViewBag.PageNumber = 0;

                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }


        [HttpPost]
        public ActionResult RemoveIntake_Instructor(string intakeID, string insID)
        {
            intake_InstructorRepository.deleteIntake_Instructor(intakeID, insID);
            return RedirectToAction("Edit", new { id = insID });
        }


        public ActionResult Delete(List<string> selectedInstructorIds,int pageNumber)
        {
            instructorRepository.DeleteInstructor(selectedInstructorIds);


            var instructors = instructorRepository.GetInstructors(pageNumber, 10);
            if (instructors.Count == 0 && pageNumber > 1)
            {
                instructors = instructorRepository.GetInstructors(pageNumber - 1, 10);
                pageNumber--;
            }



            ViewBag.PageNumber = pageNumber;
            return PartialView("_TableDataPartial", instructors);
        }
    }
}
