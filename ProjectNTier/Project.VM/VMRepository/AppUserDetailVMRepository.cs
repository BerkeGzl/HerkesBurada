using Project.BLL.RepositoryPattern.BaseRep;
using Project.VM.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.VMRepository
{
    public class AppUserDetailVMRepository : BaseRepository<AppUserDetailVM>
    {
        public override List<AppUserDetailVM> SelectActives()
        {
            return db.AppUserDetails.Select(x => new AppUserDetailVM
            {
                ID = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToList();
        }
    }
}
