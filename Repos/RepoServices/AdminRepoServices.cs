using Student_Panel_ITI.Data;
using Student_Panel_ITI.Models;

namespace Student_Panel_ITI.Repos.RepoServices
{
    public class AdminRepoServices : IAdminRepository
    {
        public MainDBContext Context { get; set; }
        public AdminRepoServices(MainDBContext context)
        {
            Context = context;
        }


        void IAdminRepository.CreateAdmin(Admin admin)
        {
            Context.Admins.Add(admin);
            Context.SaveChanges();
        }

        //void IAdminRepository.CreateAdmin(string adminID)
        //{
        //    var admin = Context.Admins.FirstOrDefault(a => a.Id == adminID);
        //    Console.WriteLine(admin);
        //    //Context.Admins.Add(admin);
        //    //Context.SaveChanges();
        //}

        void IAdminRepository.DeleteAdmin(int adminID)
        {
            var admin = Context.Admins.FirstOrDefault(ad=>ad.AspNetUserID == adminID.ToString());
            Context.Admins.Remove(admin);
            Context.SaveChanges();

        }

        Admin IAdminRepository.GetAdminbyID(int adminID)
        {
            var admin = Context.Admins.FirstOrDefault(ad => ad.AspNetUserID == adminID.ToString());
            return admin;
        }

        int IAdminRepository.GetAdminNumber()
        {
            return Context.Admins.Count();
        }

        List<Admin> IAdminRepository.GetAdmins()
        {
            return Context.Admins.ToList();
        }

        void IAdminRepository.UpdateAdmin(int adminID, Admin admin)
        {
            var admin_Updated = Context.Admins.FirstOrDefault(ad => ad.AspNetUserID == adminID.ToString());
            admin_Updated.AspNetUser.FullName = admin.AspNetUser.FullName;
            admin_Updated.AspNetUser.UserName = admin.AspNetUser.UserName;
            admin_Updated.AspNetUser.PhoneNumber = admin.AspNetUser.PhoneNumber;
            Context.SaveChanges();
        }
    }
}
