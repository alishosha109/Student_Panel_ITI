using Student_Panel_ITI.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Student_Panel_ITI.Areas.InstructorsArea.Controllers
{
    [Area("InstructorsArea")] //have to be added(mandatory)
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepo;
        public StudentController(IStudentRepository _studentRepo)
        {
            studentRepo = _studentRepo;
        }


        public ActionResult Details(int iid , int id, string name, string name2) //id(TrackID) , iid(IntakeID), name(TrackName) , name2(intakeName)
        {
            ViewBag.TrackName = name;
            ViewBag.IntakeName = name2;

            ViewBag.TrackID = id;
            ViewBag.IntakeID = iid;

        
            return View(studentRepo.GetStudentsbyIntakeTrackID(iid, id));
        }
    }
}
