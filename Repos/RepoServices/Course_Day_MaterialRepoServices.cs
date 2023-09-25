using Student_Panel_ITI.Data;
using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Student_Panel_ITI.Repos.RepoServices
{
    public class Course_Day_MaterialRepoServices : ICourse_Day_MaterialRepository
    {
        private readonly IMaterialRepository materialRepository;

        public MainDBContext Context { get; set; }



        public Course_Day_MaterialRepoServices(MainDBContext context, IMaterialRepository materialRepository)
        {
            Context = context;
            this.materialRepository = materialRepository;
        }

       
        void ICourse_Day_MaterialRepository.CreateCourseDayMaterial(Course_Day_Material cdm)
        {
            Context.Course_Day_Materials.Add(cdm);
            Context.SaveChanges();
        }

        void ICourse_Day_MaterialRepository.DeleteCourseDayMaterial(int courseID, int courseDayID, int materialID)
        {
            var cdm = Context.Course_Day_Materials.SingleOrDefault(cdm => cdm.CourseDayID == courseDayID && cdm.CourseID == courseID && cdm.MaterialID == materialID);
            Context.Course_Day_Materials.Remove(cdm);
            Context.SaveChanges();
        }    
        
        void ICourse_Day_MaterialRepository.DeleteAllRelatedCourseDays_Materials(int courseID)
        {
            var cdms = Context.Course_Day_Materials.Where(cdm=>cdm.CourseID == courseID).ToList();
            foreach (var cdm in cdms)
            {
                materialRepository.DeleteMaterial(cdm.MaterialID);
                ((ICourse_Day_MaterialRepository)this).DeleteCourseDayMaterial(courseID,cdm.CourseDayID,cdm.MaterialID);

            }
            Context.SaveChanges() ;

        }
        

        void ICourse_Day_MaterialRepository.DeleteCourseDayMaterialbyCourseDayID(int courseDayID)
        {
            var cdm = Context.Course_Day_Materials.Where(cdm => cdm.CourseDayID == courseDayID);
            foreach (var item in cdm)
            {
                Context.Course_Day_Materials.Remove(item);

            }
            Context.SaveChanges();
        }

        int ICourse_Day_MaterialRepository.GetCourseDayMaterialNumberbyCourseDayID(int courseDayID)
        {
            return Context.Course_Day_Materials.Where(cdm=>cdm.CourseDayID == courseDayID).ToList().Count();
        }

        int ICourse_Day_MaterialRepository.GetCourseDayMaterialNumberbyCourseID(int courseID)
        {
            return Context.Course_Day_Materials.Where(cdm => cdm.CourseDayID == courseID).ToList().Count();
        }

        List<Course_Day_Material> ICourse_Day_MaterialRepository.GetCourseDaysbyCourseDayID(int courseDayID)
        {
            return Context.Course_Day_Materials
                .Where(cdm => cdm.CourseDayID == courseDayID)
                .Include(cdm => cdm.CourseDay)
                .Include(cdm => cdm.MaterialOfDay)
                .Include(cdm => cdm.Course)
                .ToList();
        }

        //replace cdm.CourseDayID --> CourseID
        List<Course_Day_Material> ICourse_Day_MaterialRepository.GetCourseDaysbyCourseID(int courseID)
        {
            var cdms = Context.Course_Day_Materials.Where(cdm => cdm.CourseID == courseID).Include(cdm=>cdm.CourseDay).Include(cdm => cdm.MaterialOfDay).ToList();
            return cdms;
        }

        void ICourse_Day_MaterialRepository.UpdateCourseDayMaterial(int courseDayID, int courseID, int MaterialID, Course_Day_Material courseDayMaterial)
        {
            var cdm_updated = Context.Course_Day_Materials.FirstOrDefault(cdm => cdm.CourseDayID == courseDayID && cdm.CourseID == courseID && cdm.MaterialID == MaterialID);
            cdm_updated.CourseDayID = courseDayMaterial.CourseDayID;
            cdm_updated.CourseID = courseDayMaterial.CourseID;
            cdm_updated.MaterialID = courseDayMaterial.MaterialID;
            Context.SaveChanges();
        }




        //---------------------------------- Instructor Services ------------------//

        public void CreateCourseDayMaterial(List<Course_Day_Material> cdm)
        {
            Context.Course_Day_Materials.AddRange(cdm); 
            Context.SaveChanges();
        }


    }
}
