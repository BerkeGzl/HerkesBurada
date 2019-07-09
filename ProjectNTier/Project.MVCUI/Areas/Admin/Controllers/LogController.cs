using Project.BLL.RepositoryPattern.RepositoryConcrete;
using Project.MVCUI.Models.AuthenticationClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [Route("admin")]
    [AdminAuthentication]
    public class LogController : Controller
    {
        // GET: Admin/Log
        public LogController()
        {
            log_repo = new LogRepository();
        }
        LogRepository log_repo;

        [Route("gunluk-listesi")]
        public ActionResult LogList()
        {
            return View(log_repo.SelectActives());
        }
    }
}