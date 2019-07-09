using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.VMs
{
    public class OrderVM : BaseEntity
    {
        public string ShippingAddress { get; set; }
    }
}
