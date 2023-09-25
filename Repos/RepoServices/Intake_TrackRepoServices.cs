//using Student_Panel_ITI.Data;
//using Student_Panel_ITI.Models;
//using Student_Panel_ITI.Repos.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace Student_Panel_ITI.Repos.RepoServices
//{
//    public class Intake_TrackRepoServices : IIntake_TrackRepository
//    {

//        public MainDBContext Context { get; set; }


//        public Intake_TrackRepoServices(MainDBContext context)
//        {
//            Context = context;
//        }

//        void IIntake_TrackRepository.CreateIntake_Track(Intake_Track intake_track)
//        {
//            Context.Intake_Tracks.Add(intake_track);
//            Context.SaveChanges();
//        }


//        void IIntake_TrackRepository.CreateIntake_Track(List<int> intakeIds, int trackid, int numberofStudents)
//        {
//            foreach (var id in intakeIds)
//            {
//                Intake_Track itk = new Intake_Track()
//                {
//                    TrackID = trackid,
//                    IntakeID = id,
//                    NumOfStdsInTrack = numberofStudents
//                };
//                Context.Intake_Tracks.Add(itk);
//            }
//            Context.SaveChanges();
//        }

//        void IIntake_TrackRepository.DeleteIntake_Track(int trackID, int intakeID)
//        {

//            var intake_track = Context.Intake_Tracks.SingleOrDefault(it=>it.TrackID == trackID && it.IntakeID == intakeID);
//            Context.Intake_Tracks.Remove(intake_track);
//            Context.SaveChanges();
//        }


//        void IIntake_TrackRepository.DeleteIntake_Track(int trackID)
//        {
//            var intake_tracks = Context.Intake_Tracks.Where(it => it.TrackID == trackID).ToList();
//            foreach (var item in intake_tracks)
//            {
//                Context.Intake_Tracks.Remove(item);
//            }
//            Context.SaveChanges();
//        }        
        
//        void IIntake_TrackRepository.DeleteIntake_TrackbyintakeID(int intakeID)
//        {
//            var intake_tracks = Context.Intake_Tracks.Where(it => it.IntakeID == intakeID).ToList();
//            foreach (var item in intake_tracks)
//            {
//                Context.Intake_Tracks.Remove(item);
//            }
//            Context.SaveChanges();
//        }

//        int IIntake_TrackRepository.GetIntakeNumber(int trackid)
//        {
//            return Context.Intake_Tracks.Where(it=>it.TrackID==trackid).Count();

//        }


//        List<Intake_Track> IIntake_TrackRepository.GetIntake_TrackbyintakeID(int intakeid)
//        {
//            var intake_tracks = Context.Intake_Tracks.Include(it => it.Track).Where(it => it.IntakeID == intakeid).ToList();
//            return intake_tracks;

//        }

//        List<Intake_Track> IIntake_TrackRepository.GetIntake_TrackbytrackID(int trackid)
//        {
//            var intake_tracks = Context.Intake_Tracks.Include(it => it.Intake).Where(it => it.TrackID == trackid).ToList();
//            return intake_tracks;
//        }

//        int IIntake_TrackRepository.GetTrackNumber(int intakeid)
//        {
//            return Context.Intake_Tracks.Where(it => it.IntakeID == intakeid).Count();
//        }

//        void IIntake_TrackRepository.UpdateIntake_Track(int trackID, int intakeID, Intake_Track intake_track)
//        {
//            var intake_track_updated = Context.Intake_Tracks.SingleOrDefault(it => it.IntakeID == intakeID && it.TrackID == trackID);
//            intake_track_updated.NumOfStdsInTrack = intake_track.NumOfStdsInTrack;
//            intake_track_updated.IntakeID = intake_track.IntakeID;
//            intake_track_updated.TrackID = intake_track.TrackID;
//            Context.SaveChanges();
//        }
//    }
//}
