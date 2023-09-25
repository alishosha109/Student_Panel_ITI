using Student_Panel_ITI.ViewModels;
using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos;
using Student_Panel_ITI.Repos.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Student_Panel_ITI.ViewModels;
using Student_Panel_ITI.ViewModels;

namespace Studebt_Panel_ITI.Areas.InstructorsArea.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository icourseRepo;
        private readonly UserManager<AppUser> userManager;
        private readonly IIntake_Track_CourseRepository intakeTrackCourseRepo;
        private readonly IInstructor_CourseRepository instructorCourseRepo;
        public CourseController(
            ICourseRepository _icourseRepo,
            UserManager<AppUser> _userManager,
            IIntake_Track_CourseRepository _intaketrackCourseRepo,
            IInstructor_CourseRepository _instructorCourseRepo)
        {
            icourseRepo = _icourseRepo;
            userManager = _userManager;
            intakeTrackCourseRepo = _intaketrackCourseRepo;
            instructorCourseRepo = _instructorCourseRepo;
        }



        public ActionResult DetailsForStudent(string studentID, int trackID, int intakeID, string trackName)
        {
            ViewBag.TracKName = trackName;
            ViewBag.IntakeID = intakeID;
            ViewBag.TrackID = trackID;


            var StudentCourses = icourseRepo.GetCoursesForStudent(studentID); 

           


            return View(StudentCourses);


            #region additional info
            //int intakeId = (int)HttpContext.Session.GetInt32("IntakeID"); //get the intakeID in this session: GetInt32 returns nullable integer(int?) so convert it to (int) only.
            #endregion
        }


       
    }
}
