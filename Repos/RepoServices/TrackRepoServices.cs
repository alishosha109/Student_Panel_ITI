using Student_Panel_ITI.Data;
using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Student_Panel_ITI.Repos
{
    public class TrackRepoServices : ITrackRepository
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IIntake_Track_CourseRepository _intake_Track_CourseRepository;

    

        public MainDBContext Context { get; set; }
        public TrackRepoServices( MainDBContext context, IStudentRepository studentRepository,  IIntake_Track_CourseRepository intake_Track_CourseRepository)
        {
            Context = context;
            _studentRepository = studentRepository;
            _intake_Track_CourseRepository = intake_Track_CourseRepository;
        }
        Track ITrackRepository.getTrackbyID(int trackID)
        {
            var track =  Context.Tracks.Include(t=>t.Manager)
                                       .Include(t=>t.Admin)
                                       .FirstOrDefault(t=> t.ID == trackID);
            return track;
        }

        int ITrackRepository.getTrackNumber()
        {
            return Context.Tracks.Count();
        }       
        
        int ITrackRepository.getTrackNumberbyIntakeID(int intakeID)
        {
            return Context.Intake_Track_Courses.Where(it => it.IntakeID == intakeID)
                                                .Select(it => it.TrackID) // Select the CourseID to identify duplicates
                                                .Distinct() // Remove duplicates
                                                .Count();
        }

        List<Track> ITrackRepository.getTracks()
        {
            return Context.Tracks.Include(t => t.Manager)
                                .Include(t => t.IntakeTrackCourse)
                                .Include(t => t.IntakeTrackCourse).ToList();
        }

        List<Track> ITrackRepository.getTracks(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            var tracks =  Context.Tracks.Include(t => t.Manager)
                                           .Include(t=>t.Admin)
                                           .Include(t => t.IntakeTrackCourse)
                                           .Skip((pageNumber - 1) * pageSize)
                                           .Take(pageSize)
                                           .ToList();

            return tracks;
        }

        List<Intake_Track_Course> ITrackRepository.getTrackbyIntakeID(int intakeID, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            var tracks = Context.Intake_Track_Courses
                                          .Include(t => t.Intake)
                                          .Include(t => t.Track)
                                          .ThenInclude(t=>t.Manager)
                                          .Include(t => t.Track)
                                          .ThenInclude(t => t.Admin)
                                          .Where(t => t.IntakeID == intakeID)
                                          
                                          .Skip((pageNumber - 1) * pageSize)
                                          .Take(pageSize)
                                          .ToList();
            return tracks;

        }


        // Check should we update virtual navigation properties 
        void ITrackRepository.UpdateTrack(int trackID, Track track)
        {
            var trackUpdated  = Context.Tracks.FirstOrDefault(t=>t.ID == trackID);

            trackUpdated.Name = track.Name;
            trackUpdated.CreationDate = track.CreationDate;
            trackUpdated.ManagerID = track.ManagerID;
            Context.SaveChanges();
        }

        void ITrackRepository.DeleteTrack(int trackID)
        {

            var students = _studentRepository.getStudentsbyTrackID(trackID);
            if (students.Count == 0)
            {

                //_intake_trackRepository.DeleteIntake_Track(trackID);
                _intake_Track_CourseRepository.DeleteIntake_Track_CoursebyTrackID(trackID);
                var track = Context.Tracks.FirstOrDefault(t => t.ID == trackID);
                Context.Tracks.Remove(track);
                Context.SaveChanges();
            }

        }


        void ITrackRepository.DeleteTrack(List<int> trackIDs)
        {

            List<Track> trackstoDelete = new List<Track>();
            foreach (var trackID in trackIDs)
            {
                var students = _studentRepository.getStudentsbyTrackID(trackID);
                if (students.Count == 0)
                {   
                    //_intake_trackRepository.DeleteIntake_Track(trackID);
                    _intake_Track_CourseRepository.DeleteIntake_Track_CoursebyTrackID(trackID);
                    var track = Context.Tracks.SingleOrDefault(t => t.ID == trackID);
                    trackstoDelete.Add(track);
                }
            }
            
            Context.Tracks.RemoveRange(trackstoDelete);
            Context.SaveChanges();



        }

        void ITrackRepository.CreateTrack( Track track)
        {
            Context.Tracks.Add(track);
            Context.SaveChanges();
        }
        

        // check null or na

        void ITrackRepository.RemoveManager(string managerID)
        {
            var tracks = Context.Tracks.Where(t=>t.ManagerID == managerID.ToString()).ToList();
            foreach (var track in tracks)
            {
                track.ManagerID = null;
            }
            Context.SaveChanges();
        }


        //--//
        public List<Track> GetTracksByIntakeID(int intakeid)
        {
            var query = 
                (from t in Context.Tracks
                join itc in Context.Intake_Track_Courses on t.ID equals itc.TrackID
                where itc.IntakeID == intakeid 
                select t)
                .Distinct()
                .ToList();

            return query;
        }





        /*---------------------------------------------- Instructor Services -----------------------------------------------*/

        //get the tracks based on specific intake, and specific manager.
        public List<Track> getTracks(string ManagerID, int intakeID)
        {
            return
                (from t in Context.Tracks
                 join itc in Context.Intake_Track_Courses on t.ID equals itc.TrackID
                 where t.ManagerID == ManagerID && itc.IntakeID == intakeID
                 select t).Distinct()
                 .ToList();
        }



        //get the tracks a specific Instructor teach in, in a specific intake
        public List<Track> GetTracksByTeacher(int intakeID, string InstructorID)
        {
            var query =
                (from itc in Context.Intake_Track_Courses
                 where itc.IntakeID == intakeID
                 join ic in Context.Instructor_Courses on itc.CourseID equals ic.CourseID
                 where ic.InstructorID == InstructorID
                 select itc.Track)
                .Distinct()
                .ToList();

            return query;
        }
    }
}
