using Project.BLL.RepositoryPattern.ConcreteRep;
using Project.MODEL.Entities;
using Project.MVCUI.Models.Filters;
using Project.TOOLS.CustomTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    [ActFilter, ResFilter]
    public class HomeController : Controller
    {
        AppUserRepository aprep;
        AppUserDetailRepository apdRep;
        public HomeController()
        {
            aprep = new AppUserRepository();
            apdRep = new AppUserDetailRepository();
        }

        // GET: Home
        //[Route("hesap-kayit")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("hesap-kayit")]
        public ActionResult Register([Bind(Prefix ="Item1")]AppUser item1, [Bind(Prefix ="Item2")]AppUserDetail item2)
        {
            if (aprep.Any(x => x.UserName == item1.UserName))
            {
                ViewBag.ZatenVar = "Lütfen başka bir kullanıcı ismi giriniz";
                return View();
            }
            string gonderilecekMail = "Tebrikler hesabınız oluşturulmuştur. Hesabınızı aktive etmek için https://localhost:44367/Home/Aktivasyon/" + item1.ActivationCode + " linkine tıklayabilirsiniz.";
            MailSender.Send(item1.Email, body: gonderilecekMail);
            item1.Password = Crypto.HashPassword(item1.Password);
            item2.UserIP = Request.UserHostAddress;
            aprep.Add(item1);
            item2.ID = item1.ID;
            apdRep.Add(item2);
            return RedirectToAction("RegisterOk");
        }

        public ActionResult Aktivasyon(Guid id)
        {
            if (aprep.Any(x => x.ActivationCode == id))
            {
                aprep.Default(x => x.ActivationCode == id).IsActive = true;
                TempData["HesapAktif"] = "Hesabınız aktif hale getirildi";
                if(Session["bekleyenUrun"] != null)
                {
                    Session["scart"] = ControlProduct.KeepProduct(Session["bekleyenUrun"]);
                }
                return RedirectToAction("Index","Member");
            }
            TempData["HesapAktif"] = "Aktif edilecek hesap bulunamadı";
            return RedirectToAction("Register");
        }

        [Route("hesap-kayit-tamamlama")]
        public ActionResult RegisterOk()
        {
            return View();
        }

        [Route("giris")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("giris")]
        public ActionResult Login(AppUser item)
        {
            if (aprep.Any(x => x.UserName == item.UserName && x.Password == item.Password && x.Status != MODEL.Enums.DataStatus.Deleted && x.UserRole == MODEL.Enums.Role.Admin))
            {
                AppUser girisYapan = aprep.Default(x => x.UserName == item.UserName && item.Password == item.Password && x.Status != MODEL.Enums.DataStatus.Deleted && x.UserRole == MODEL.Enums.Role.Admin);
                Session["admin"] = girisYapan;
                return RedirectToAction("");
            }
            else if (aprep.Any(x => x.UserName == item.UserName && x.Password == item.Password && x.UserRole == MODEL.Enums.Role.Member && x.Status != MODEL.Enums.DataStatus.Deleted))
            {
                AppUser girisYapanUye = aprep.Default(x => x.UserName == item.UserName && x.Password == item.Password && x.UserRole == MODEL.Enums.Role.Member && x.Status != MODEL.Enums.DataStatus.Deleted);
                Session["member"] = girisYapanUye;
                if (Session["bekleyenUrun"] != null)
                {
                    Session["scart"] = ControlProduct.KeepProduct(Session["bekleyenUrun"]);
                }
                return RedirectToAction("Index", "Member");
            }
            ViewBag.KullaniciBulunamadi = "Böyle bir kullanıcı yoktur";
            return View();
        }


        public ActionResult LogOut()
        {
            if (Session["admin"] != null)
            {
                Session.Remove("admin");
                return RedirectToAction("Index", "Member");
            }
            else if (true)
            {
                Session.Remove("member");
                return RedirectToAction("Index", "Member");
            }
        }
    }
}