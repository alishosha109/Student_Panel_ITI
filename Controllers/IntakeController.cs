using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos;
using Student_Panel_ITI.Repos.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Student_Panel_ITI.Areas.InstructorsArea.Controllers
{
    public class IntakeController : Controller
    {
        private readonly IIntakeRepository intakeRepo;
        private readonly UserManager<AppUser> userManager;
        private readonly IIntake_InstructorRepository intakeInstructorRepo;
        public IntakeController(IIntakeRepository _intakeRepo, 
            UserManager<AppUser> _userManager, 
            IIntake_InstructorRepository _intakeInstructorRepo)
        {
            intakeRepo = _intakeRepo;
            userManager = _userManager;
            intakeInstructorRepo = _intakeInstructorRepo;
        }


        //---// //List of all intakes that instructor works in
        public ActionResult Index()
        {
            ViewBag.InstructorName = userManager.GetUserAsync(User).Result.FullName;

            return View(intakeInstructorRepo.GetIntakesByInstructorID(userManager.GetUserId(User)));
        }
    }
}
