using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VM.VMs
{
    public class AppUserVM : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
