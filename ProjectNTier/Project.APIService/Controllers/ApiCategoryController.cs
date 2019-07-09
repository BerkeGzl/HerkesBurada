using Project.VM.VMRepository;
using Project.VM.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.APIService.Controllers
{
    public class ApiCategoryController : ApiController
    {
        CategoryVMRepository crvmRep;
        public ApiCategoryController()
        {
            crvmRep = new CategoryVMRepository();
        }

        [HttpGet]
        public List<CategoryVM> ListCategories()
        {
            return crvmRep.SelectActives();
        }
    }
}
