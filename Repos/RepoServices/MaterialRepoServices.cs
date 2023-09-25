using Student_Panel_ITI.Data;
using Student_Panel_ITI.Models;
using Student_Panel_ITI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Student_Panel_ITI.Repos.RepoServices
{
    public class MaterialRepoServices : IMaterialRepository
    {

        public MainDBContext Context { get; set; }



        public MaterialRepoServices(MainDBContext context)
        {
            Context = context;
        }

        void IMaterialRepository.CreateMaterial(Material material)
        {
            Context.Materials.Add(material);
            Context.SaveChanges();
        }

        void IMaterialRepository.DeleteMaterial(int materialID)
        {


            var material = Context.Materials.FirstOrDefault(m => m.ID == materialID);
            Context.Materials.Remove(material);
            Context.SaveChanges();
        }

        Material IMaterialRepository.GetbyMaterialbyID(int materialID)
        {
            var material = Context.Materials.FirstOrDefault(m => m.ID == materialID);
            return material;
        }

        List<Material> IMaterialRepository.GetMaterial()
        {
            return Context.Materials.ToList();
        }

        

        int IMaterialRepository.GetMaterialNumber()
        {
            return Context.Materials.Count();
        }
        int IMaterialRepository.GetMaterialNumber(int courseID, int crsDayID)
        {
            return Context.Materials.Include(m=>m.CourseDayMaterials).ThenInclude(cdm=>cdm.CourseDayID == crsDayID && cdm.CourseID == courseID).Count();
        }

        void IMaterialRepository.UpdateMaterial(int materialID, Material material)
        {
            var materialUpdated = Context.Materials.FirstOrDefault(m => m.ID == materialID);
            materialUpdated.Path = material.Path;
            materialUpdated.InstructorID = material.InstructorID;
            Context.SaveChanges();
        }

        void IMaterialRepository.RemoveInstructor(string instructorID)
        {
            var materials = Context.Materials.Where(m => m.InstructorID == instructorID.ToString()).ToList();
            foreach (var material in materials)
            {
                material.InstructorID = null;
            }
            Context.SaveChanges();
        }








        /*---------------------------------------------- Instructor Services -----------------------------------------------*/

        public void CreateMaterials(List<Material> materials)
        {
            Context.Materials.AddRange(materials);
            Context.SaveChanges();
        }

    }
}
