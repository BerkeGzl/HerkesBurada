using PagedList;
using Project.BLL.RepositoryPattern.ConcreteRep;
using Project.MODEL.Entities;
using Project.MVCUI.Models.Filters;
using Project.TOOLS.CustomTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    [ActFilter, ResFilter]
    public class MemberController : Controller
    {
        ProductRepository prep;
        CategoryRepository crep;
        OrderRepository orep;
        OrderDetailRepository odrep;
        public MemberController()
        {
            prep = new ProductRepository();
            crep = new CategoryRepository();
            orep = new OrderRepository();
            odrep = new OrderDetailRepository();
        }

        // GET: Member
        [Route("")]
        [Route("urunler")]
        [Route("urunler/tum-urunler")]
        public ActionResult Index(int? page)
        {
            return View(Tuple.Create(prep.SelectActives().ToPagedList(page ?? 1, 9), crep.SelectActives()));
        }

        [Route("urunler/{category = Kategoriler}/{id:int}")]
        public ActionResult SelectByCategory(int id, int? page)
        {
            ViewBag.KategoriID = id;
            return View(Tuple.Create(prep.Where(x => x.CategoryID == id).ToPagedList(page ?? 1, 9), crep.SelectActives()));
        }

        [Route("karta-ekle")]
        public ActionResult AddToCart(int id)
        {
            if (Session["member"] == null)
            {
                TempData["uyeDegil"] = "Lütfen sepete ürün eklemeden önce üye olun";
                Product bekleyenUrun = prep.GetByID(id);
                Session["bekleyenUrun"] = bekleyenUrun;
                return RedirectToAction("Register", "Home");
            }
            Cart c = Session["scart"] == null ? new Cart() : Session["scart"] as Cart;
            Product eklenecekUrun = prep.GetByID(id);
            CartItem ci = new CartItem();
            ci.ID = eklenecekUrun.ID;
            ci.Name = eklenecekUrun.ProductName;
            ci.Price = Convert.ToDecimal(eklenecekUrun.UnitPrice);
            ci.ImagePath = eklenecekUrun.ImagePath;
            c.SepeteEkle(ci);
            Session["scart"] = c;
            return RedirectToAction("Index");
        }

        [Route("sepet")]
        public ActionResult CartPage()
        {
            if (Session["scart"] != null)
            {
                Cart c = Session["scart"] as Cart;
                return View(c);
            }
            TempData["message"] = "Sepetinizde ürün bulunmamaktadır";
            return RedirectToAction("Index");
        }

        [Route("siparis-onay")]
        public ActionResult SiparisiOnayla()
        {
            return View();
        }

        [HttpPost]
        [Route("siparis-onay")]
        public ActionResult SiparisiOnayla(Order item)
        {

            if (item.RequiredDate < DateTime.Now)
            {
                ViewBag.Tarih = "Geçmiş bir tarih seçemezsiniz";
                return View();
            }
            else if ((item.RequiredDate - DateTime.Now).Value.Days <= 2)
            {
                ViewBag.Tarih = "2 gün veya daha az sürede bir gönderim maalesef yapamayız";
                return View();
            }
            item.AppUserID = (Session["member"] as AppUser).ID;
            orep.Add(item);
            Cart sepet = Session["scart"] as Cart;
            string gonderilecekMesaj = null;
            foreach (CartItem urun in sepet.Sepetim)
            {
                OrderDetail od = new OrderDetail();
                od.OrderID = item.ID;
                od.ProductID = urun.ID;
                od.TotalPrice = urun.SubTotal;
                od.Quantity = urun.Amount;
                odrep.Add(od);
                gonderilecekMesaj += $"Urun:{urun.Name}, Adedi:{urun.Amount}, Toplam Fiyatı:{urun.SubTotal}\n";
            }
            gonderilecekMesaj += $"Ödemeniz gereken toplam tutar {sepet.TotalPrice}";
            Session.Remove("scart");
            TempData["satıs"] = "Siparişiniz bize ulaşmıştır. Detaylı liste mail adresinize gönderildi...";
            MailSender.Send((Session["member"] as AppUser).Email, body: gonderilecekMesaj, subject: "Sipariş Detay!");
            return RedirectToAction("Index");
        }

        //[Route("urun-arama/{keyword}")]
        public PartialViewResult SearchProducts(string keyword, int? page)
        {
            ViewBag.Keyword = keyword;
            return PartialView(Tuple.Create(prep.Where(x => x.ProductName.Contains(keyword)).ToPagedList(page ?? 1, 9), crep.SelectActives()));
        }

        [HttpGet]
        //[Route("urun-filtre/{item}")]
        public PartialViewResult FiltreliUrunler(string item, int? page)
        {
            ViewBag.Fiyat = item;
            //"$75 - $355"
            string[] filter = item.Split('-');
            //"$75"
            //" $355"
            filter[1] = filter[1].TrimStart(); //başındaki boşluk alındı.
            for (int i = 0; i < 2; i++)
            {
                filter[i] = filter[i].TrimStart('$');
            }

            decimal start = Convert.ToDecimal(filter[0]);
            decimal end = Convert.ToDecimal(filter[1]);
            return PartialView("_FiltreUrunler", Tuple.Create(crep.SelectActives(), prep.Where(x => x.UnitPrice > start && x.UnitPrice < end).ToPagedList(page ?? 1, 9)));
        }

        //[Route("urun-sozcuk-filtre/{fiyat}/{keyword}/{categoryId:int}")]
        public PartialViewResult FiltreKeywordUrunleri(string fiyat, string keyword, int categoryId, int? page)
        {
            ViewBag.Keyword = keyword;
            ViewBag.Fiyat = fiyat;
            string[] filter = fiyat.Split('-');
            filter[1] = filter[1].TrimStart();
            for (int i = 0; i < 2; i++)
            {
                filter[i] = filter[i].TrimStart('$');
            }

            decimal start = Convert.ToDecimal(filter[0]);

            decimal end = Convert.ToDecimal(filter[1]);

            return PartialView(Tuple.Create(crep.SelectActives(), prep.Where(x => x.UnitPrice > start && x.UnitPrice < end && x.CategoryID == categoryId).ToPagedList(page ?? 1, 9)));
        }

        [Route("urunler/{category}/{Title}-{id:int}")]
        public ActionResult ProductDetail(int id)
        {
            return View(Tuple.Create(prep.GetByID(id), crep.SelectActives(), prep.SelectActives()));
        }

        [Route("hakkimizda")]
        public ActionResult About()
        {
            return View();
        }

        [Route("iletişim")]
        public ActionResult Contact()
        {
            return View();
        }
    }
}