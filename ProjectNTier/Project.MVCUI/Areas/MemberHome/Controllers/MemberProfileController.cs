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

namespace Project.MVCUI.Areas.MemberHome.Controllers
{
    [RouteArea("MemberHome")]
    [Route("MemberHome")]
    [MemberAuthentication]
    [ActFilter, ResFilter]
    public class MemberProfileController : Controller
    {
        AppUserRepository apprepo;
        AppUserDetailRepository appdetrepo;
        public MemberProfileController()
        {
            apprepo = new AppUserRepository();
            appdetrepo = new AppUserDetailRepository();
        }

        // GET: Member/MemberHome
        [Route("kullanici-profil/{id:int}")]
        public ActionResult MemberProfile(int id)
        {
            return View(apprepo.GetByID(id));
        }

        [Route("kullanici-profil-guncelle/{id:int}")]
        public ActionResult UpdateProfile(int id)
        {
            return View(Tuple.Create(apprepo.GetByID(id), appdetrepo.GetByID(id)));
        }

        [HttpPost]
        [Route("kullanici-profil-guncelle/{id:int}")]
        public ActionResult UpdateProfile([Bind(Prefix = "Item1")]AppUser item, [Bind(Prefix = "Item2")] AppUserDetail item2)
        {
            apprepo.Update(item);
            item.ID = item2.ID;
            appdetrepo.Update(item2);
            return RedirectToAction("MemberProfile", new { id = (Session["member"] as AppUser).ID });
        }

        [Route("sifre-guncelleme")]
        public ActionResult UpdatePassword()
        {
            return View();
        }

        [Route("sifre-guncelleme")]
        [HttpPost]
        public ActionResult UpdatePassword(AppUser item)
        {
            AppUser guncellenen = apprepo.GetByID((Session["member"] as AppUser).ID);
            guncellenen.Password = Crypto.HashPassword(item.Password);
            apprepo.Update(guncellenen);
            return RedirectToAction("MemberProfile", new { id = (Session["member"] as AppUser).ID });
        }
    }
}