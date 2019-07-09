using Project.BLL.RepositoryPattern.BaseRep;
using Project.VM.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.VMRepository
{
    public class ProductVMRepository : BaseRepository<ProductVM>
    {
        public override List<ProductVM> SelectActives()
        {
            return db.Products.Select(x => new ProductVM {
                ID = x.ID,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock
            }).ToList();
        }
    }
}
