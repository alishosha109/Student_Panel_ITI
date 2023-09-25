using Student_Panel_ITI.Data;
using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace Student_Panel_ITI.Repos.RepoServices
{
    public class InstructorRepoServices : IInstructorRepository
    {
        private readonly ITrackRepository trackRepository;
        private readonly IExamRepository examRepository;
        private readonly IMaterialRepository materialRepository;
        private readonly IInstructor_CourseRepository instructor_CourseRepository;
        private readonly IIntake_InstructorRepository intake_InstructorRepository;

        public MainDBContext Context { get; set; }


        public InstructorRepoServices(MainDBContext context, ITrackRepository trackRepository, IExamRepository examRepository, IMaterialRepository materialRepository, IInstructor_CourseRepository instructor_CourseRepository, IIntake_InstructorRepository intake_InstructorRepository )
        {
            Context = context;
            this.trackRepository = trackRepository;
            this.examRepository = examRepository;
            this.materialRepository = materialRepository;
            this.instructor_CourseRepository = instructor_CourseRepository;
            this.intake_InstructorRepository = intake_InstructorRepository;
        }


        void IInstructorRepository.CreateInstructor(Instructor instructor)
        {
            Context.Instructors.Add(instructor);
            Context.SaveChanges();
        }

        void IInstructorRepository.DeleteInstructor(string instructorID)
        {

            // make manager of track na or null
            trackRepository.RemoveManager(instructorID);

            // make instructor id of exam na or null
            examRepository.RemoveInstructor(instructorID);

            // make instructor id of material na or null
            materialRepository.RemoveInstructor(instructorID);

            //delete record of instructor_course
            instructor_CourseRepository.DeleteInstructor_Course(instructorID);


            // delete record of intake_instructor
            intake_InstructorRepository.deleteIntake_InstructorbyInstructorID(instructorID);

            var ins = Context.Instructors.FirstOrDefault(i => i.AspNetUserID == instructorID.ToString());
            Context.Instructors.Remove(ins);
            Context.SaveChanges();
        }

        void IInstructorRepository.DeleteInstructor(List<String> instructorIDs)
        {


            foreach (var id in instructorIDs)
            {
                // make manager of track na or null
                trackRepository.RemoveManager(id);

                // make instructor id of exam na or null
                examRepository.RemoveInstructor(id);

                // make instructor id of material na or null
                materialRepository.RemoveInstructor(id);

                //delete record of instructor_course
                instructor_CourseRepository.DeleteInstructor_Course(id);


                // delete record of intake_instructor
                intake_InstructorRepository.deleteIntake_InstructorbyInstructorID(id);

                var ins = Context.Instructors.FirstOrDefault(i => i.AspNetUserID == id);
                Context.Instructors.Remove(ins);
            }
            
            Context.SaveChanges();
        }


        Instructor IInstructorRepository.GetInstructorbyID(string instructorID)
        {
            var ins = Context.Instructors
                             .Include(i=>i.Admin)
                             .Include(i=>i.Tracks)
                             .Include(i=>i.InstructorCourses)
                             .ThenInclude(it=>it.Course)
                             .Include(i=>i.IntakeInstructors)
                             .ThenInclude(ii=>ii.Intake)
                             .FirstOrDefault(i => i.AspNetUserID == instructorID.ToString());
            return ins;
        }

        int IInstructorRepository.GetInstructorNumber()
        {
            return Context.Instructors.Count();
        }

        int IInstructorRepository.GetInstructorNumberbyIntakeID(int intakeID)
        {
            var tracks = Context.Intake_Instructors.Where(it => it.IntakeID == intakeID);
            return tracks.Count();

        }

         List<Instructor> IInstructorRepository.GetInstructors(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            var instructors = Context.Instructors
                .Include(i=>i.Admin)
                .Include(i=>i.AspNetUser)
                .Include(i => i.Tracks)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return instructors;
        }
         


        void IInstructorRepository.UpdateInstructor(string instructorID, Instructor instructor)
        {
            var instructor_updated = Context.Instructors.FirstOrDefault(i=>i.AspNetUserID == instructorID.ToString());
            //instructor_updated.AspNetUser.FullName = instructor.AspNetUser.FullName;
            //instructor_updated.AspNetUser.UserName = instructor.AspNetUser.UserName;
            //instructor_updated.AdminID = instructor.AdminID;
            //instructor_updated.AspNetUser.Email = instructor.AspNetUser.Email;
            //instructor_updated.AspNetUser.PhoneNumber = instructor.AspNetUser.PhoneNumber;
            instructor_updated.CurrentlyWorking = instructor.CurrentlyWorking;
            Context.SaveChanges();
        }

        List<Instructor> IInstructorRepository.GetInstructors()
        {
            var instructors = Context.Instructors
                .Include(i => i.Admin)
                .Include(i => i.AspNetUser)
                .Include(i => i.Tracks)
                .ToList();

            return instructors;
        }
    }
}
