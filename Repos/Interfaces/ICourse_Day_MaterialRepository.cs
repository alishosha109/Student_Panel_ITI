using Student_Panel_ITI.Models;

namespace Student_Panel_ITI.Repos.Interfaces
{
    public interface ICourse_Day_MaterialRepository
    {
        public int GetCourseDayMaterialNumberbyCourseID(int courseID);
        public int GetCourseDayMaterialNumberbyCourseDayID(int courseDayID);
        public List<Course_Day_Material> GetCourseDaysbyCourseID(int courseID);
        public List<Course_Day_Material> GetCourseDaysbyCourseDayID(int courseDayID);
       
        
        public void CreateCourseDayMaterial(Course_Day_Material cdm);
        public void UpdateCourseDayMaterial(int courseID, int courseDayID, int materialID, Course_Day_Material cdm);
        public void DeleteCourseDayMaterial(int courseID, int courseDayID, int materialID);
        public void DeleteCourseDayMaterialbyCourseDayID(int courseDayID);
        public void DeleteAllRelatedCourseDays_Materials(int courseID);



        //---------------------------------- Instructor Repos ------------------//

        public void CreateCourseDayMaterial(List<Course_Day_Material> cdm);
        //public List<Course_Day_Material> GetCourseDaysbyCourseDayID(int courseDayID); exists above

    }
}
