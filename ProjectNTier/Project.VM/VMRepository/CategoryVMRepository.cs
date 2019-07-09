using Project.BLL.RepositoryPattern.BaseRep;
using Project.VM.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.VMRepository
{
    public class CategoryVMRepository :BaseRepository<CategoryVM>
    {
        public override List<CategoryVM> SelectActives()
        {
            return db.Categories.Select(x => new CategoryVM
            {
                ID = x.ID,
                CategoryName = x.CategoryName,
                Description = x.Description
            }).ToList();
        }
    }
}
