using Project.BLL.RepositoryPattern.BaseRep;
using Project.VM.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.VMRepository
{
    public class AppUserVMRepository : BaseRepository<AppUserVM>
    {
        public override List<AppUserVM> SelectActives()
        {
            return db.AppUsers.Select(x => new AppUserVM
            {
                ID = x.ID,
                UserName = x.UserName,
                Password = x.Password
            }).ToList();
        }
    }
}
