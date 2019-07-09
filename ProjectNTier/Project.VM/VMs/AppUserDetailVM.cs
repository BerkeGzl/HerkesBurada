using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.VMs
{
    public class AppUserDetailVM : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
    }
}
