using Project.BLL.RepositoryPattern.ConcreteRep;
using Project.MODEL.Entities;
using Project.MVCUI.Models.AuthenticationClasses;
using Project.MVCUI.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [Route("admin")]
    [AdminAuthentication]
    [ActFilter, ResFilter]
    public class AdminProfileController : Controller
    {
        AppUserRepository apprepo;
        AppUserDetailRepository appdetrepo;
        public AdminProfileController()
        {
            apprepo = new AppUserRepository();
            appdetrepo = new AppUserDetailRepository();
        }
        // GET: Admin/AdminProfile
        [Route("profil/{id:int}")]
        public ActionResult AdminProfile(int id)
        {
            return View(apprepo.GetByID(id));
        }

        //todo admin kullanıcı eklerken  truncat patlıyor.
        //todo search ve filtre bozuk neden belirsiz
        [Route("profil-guncelle/{id:int}")]
        public ActionResult UpdateProfile(int id)
        {
            return View(Tuple.Create(apprepo.GetByID(id), appdetrepo.GetByID(id)));
        }

        [HttpPost]
        [Route("profil-guncelle/{id:int}")]
        public ActionResult UpdateProfile([Bind(Prefix ="Item1")]AppUser item, [Bind(Prefix ="Item2")] AppUserDetail item2)
        {
            apprepo.Update(item);
            item.ID = item2.ID;
            appdetrepo.Update(item2);
            return RedirectToAction("AdminProfile", new { id= (Session["admin"] as AppUser).ID});
        }


        [Route("profil-sifre-guncelle")]
        public ActionResult UpdatePassword()
        {
            return View();
        }

        [Route("profil-sifre-guncelle")]
        [HttpPost]
        public ActionResult UpdatePassword(AppUser item)
        {
            AppUser guncellenen = apprepo.GetByID((Session["admin"] as AppUser).ID);
            guncellenen.Password = Crypto.HashPassword(item.Password);
            apprepo.Update(guncellenen);
            return RedirectToAction("AdminProfile", new { id = (Session["admin"] as AppUser).ID });
        }
    }
}