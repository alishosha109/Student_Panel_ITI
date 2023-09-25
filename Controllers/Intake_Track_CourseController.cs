using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Student_Panel_ITI.Areas.InstructorsArea.Controllers
{
    public class Intake_Track_CourseController : Controller
    {
        private readonly IIntake_Track_CourseRepository intakeTrackCourseRepo;
        private readonly UserManager<AppUser> userManager;
        public Intake_Track_CourseController(IIntake_Track_CourseRepository _intakeTrackCourseRepo, 
            UserManager<AppUser> _userManager)
        {
            intakeTrackCourseRepo = _intakeTrackCourseRepo;
            userManager = _userManager;
        }

    }
}
