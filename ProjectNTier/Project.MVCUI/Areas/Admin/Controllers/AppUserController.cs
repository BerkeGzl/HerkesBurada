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
    public class AppUserController : Controller
    {
        AppUserRepository apprep;
        AppUserDetailRepository appdetrepo;
        public AppUserController()
        {
            apprep = new AppUserRepository();
            appdetrepo = new AppUserDetailRepository();
        }
        // GET: Admin/AppUser
        [Route("kullanici-listesi")]
        public ActionResult ListAppUser()
        {
            return View(apprep.SelectActives());
        }

        [Route("silinmis-kullanici-listesi")]
        public ActionResult ListDeletedAppUser()
        {
            return View(apprep.SelectDeleteds());
        }

        [Route("kullanici-ekle")]
        public ActionResult AddAppUser()
        {
            return View();
        }

        [HttpPost]
        [Route("kullanici-ekle")]
        public ActionResult AddAppUser([Bind(Prefix = "Item1")]AppUser item, [Bind(Prefix = "Item2")] AppUserDetail item2)
        {
            if (apprep.Any(x => x.UserName == item.UserName || x.Email == item.Email))
            {
                ViewBag.Kayitli = "Böyle bir kullanıcı zaten mevcut";
                return View();
            }
            item.CreatedBy = (Session["admin"] as AppUser).UserName;
            item.Password = Crypto.HashPassword(item.Password);
            item2.UserIP = Request.UserHostAddress;
            apprep.Add(item);
            item.ID = item2.ID;
            appdetrepo.Add(item2);
            return RedirectToAction("ListAppUser");
        }

        [Route("kullanici-guncelle/{id:int}")]
        public ActionResult UpdateAppUser(int id)
        {
            return View(Tuple.Create(apprep.GetByID(id), appdetrepo.GetByID(id)));
        }

        [HttpPost]
        [Route("kullanici-guncelle/{id:int}")]
        public ActionResult UpdateAppUser([Bind(Prefix = "Item1")]AppUser item, [Bind(Prefix = "Item2")] AppUserDetail item2)
        {
            apprep.Update(item);
            item.ID = item2.ID;
            appdetrepo.Update(item2);
            return RedirectToAction("ListAppUser");
        }

        [Route("kullanici-sil/{id:int}")]
        public ActionResult DeleteAppUser(int id)
        {
            apprep.Delete(apprep.GetByID(id));
            return RedirectToAction("ListAppUser");
        }

        [Route("kullanici-sifre-guncelleme/{id:int}")]
        public ActionResult UpdateAppUserPassword(int id)
        {
            Session["gelenKullanici"] = apprep.GetByID(id);
            return View();
        }

        [Route("kullanici-sifre-guncelleme/{id:int}")]
        [HttpPost]
        public ActionResult UpdateAppUserPassword(AppUser item)
        {
            AppUser guncellenen = apprep.GetByID((Session["gelenKullanici"] as AppUser).ID);
            guncellenen.Password = Crypto.HashPassword(item.Password);
            guncellenen.ModifiedBy = (Session["admin"] as AppUser).UserName;
            apprep.Update(guncellenen);
            return RedirectToAction("ListAppUser");
        }
    }
}