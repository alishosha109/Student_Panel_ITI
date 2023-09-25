using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos;
using Student_Panel_ITI.Repos.Interfaces;
using Student_Panel_ITI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Student_Panel_ITI.Models;

namespace Student_Panel_ITI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IIntakeRepository intakeRepository;
        private readonly ITrackRepository trackRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IInstructorRepository instructorRepository;
        private readonly ICourseRepository courseRepository;
        private readonly UserManager<AppUser> userManager;

        public HomeController(ILogger<HomeController> logger, IIntakeRepository intakeRepository, ITrackRepository trackRepository, IStudentRepository studentRepository, IInstructorRepository instructorRepository, ICourseRepository courseRepository, UserManager<AppUser> userManager)
        {
            _logger = logger;
            this.intakeRepository = intakeRepository;
            this.trackRepository = trackRepository;
            this.studentRepository = studentRepository;
            this.instructorRepository = instructorRepository;
            this.courseRepository = courseRepository;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {

                HomePageViewModel hmPageViewModel = new HomePageViewModel()
                {
                    StudentName = "",
                    PhoneNumber = "",
                    Intake = "",
                    Track = "",
                    StudentID = 0,
                    IntakeID = 0,
                    TrackID = 0

                };

                //var user = userManager.GetUserAsync(User).Result; // Get the current user

                //if (user != null)
                //{
                //    // You can pass user-related data to the view here
                //    ViewData["FullName"] = user.FullName;
                //}
                return View(hmPageViewModel);

            }
            else
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

       



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}