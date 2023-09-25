namespace Student_Panel_ITI.Models
{
    public interface IAdminRepository
    {
        public int GetAdminNumber();



        public Admin GetAdminbyID(int adminID);



        public List<Admin> GetAdmins();


        public void UpdateAdmin(int adminID, Admin admin);


        public void DeleteAdmin(int adminID);


        public void CreateAdmin(Admin admin);


        ////---//
        //public void CreateAdmin(string adminID);
    }
}
