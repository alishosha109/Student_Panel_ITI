using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Student_Panel_ITI.Areas.InstructorsArea.Controllers
{
    public class TrackController : Controller
    {
        private readonly ITrackRepository itrackRepo;
        private readonly UserManager<AppUser> userManager;
        public TrackController(
            ITrackRepository _itrackRepo,
            UserManager<AppUser> _userManager)
        {
            itrackRepo = _itrackRepo;
            userManager = _userManager;
        }


        //get the tracks in this intake, and only the tracks this instructor manage :: id(intake ID) , name(Intake ExamName)
        public ActionResult DetailsForManager(int id, string name)
        {
            string? UserID = userManager.GetUserId(User);

            ViewBag.IntakeID = id;
            ViewBag.intakeName = name;

            return View(itrackRepo.getTracks(UserID, id));


            #region additional info
            //HttpContext.Session.SetInt32("IntakeID", id); //store the intakeID in session state to use it in the whole app during session lifetime. 
            #endregion
        }



        //get the tracks in this intake, and only the tracks this instructor Teach in :: id(intake ID) , name(Intake ExamName)
        public ActionResult DetailsForTeacher(int id, string name)
        {
            ViewBag.IntakeName = name;
            ViewBag.IntakeID = id;

            return View(itrackRepo.GetTracksByTeacher(id, userManager.GetUserId(User)));


            #region additional info
            //HttpContext.Session.SetInt32("IntakeID", id); //store the intakeID in session state to use it in the whole app during session lifetime. 
            #endregion
        }


    }
}
