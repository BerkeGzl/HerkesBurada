using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.VMs
{
    public class OrderDetailVM : BaseEntity
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal? TotalPrice { get; set; }
        public short Quantity { get; set; }
    }
}
