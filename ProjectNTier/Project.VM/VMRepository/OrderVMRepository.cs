using Project.BLL.RepositoryPattern.BaseRep;
using Project.VM.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.VMRepository
{
    public class OrderVMRepository : BaseRepository<OrderVM>
    {
        public override List<OrderVM> SelectActives()
        {
            return db.Orders.Select(x => new OrderVM {
                ID = x.ID,
                ShippingAddress = x.ShippedAddress
            }).ToList();
        }
    }
}
