using Project.BLL.RepositoryPattern.ConcreteRep;
using Project.MODEL.Entities;
using Project.MVCUI.Models.AuthenticationClasses;
using Project.MVCUI.Models.Filters;
using Project.TOOLS.CustomTools;
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
    [ActFilter, ResFilter]
    public class ProductController : Controller
    {
        CategoryRepository crep;
        ProductRepository prep;
        public ProductController()
        {
            crep = new CategoryRepository();
            prep = new ProductRepository();
        }

        // GET: Admin/Product
        [Route("urun-listesi")]
        public ActionResult ListProduct()
        {
            return View(prep.SelectActives());
        }

        [Route("urun-detay/{id:int}")]
        public ActionResult ProductDetail(int id)
        {
            return View(prep.GetByID(id));
        }

        [Route("urun-ekle")]
        public ActionResult AddProduct()
        {
            return View(Tuple.Create(new Product(), crep.SelectActives()));
        }

        [HttpPost]
        [Route("urun-ekle")]
        public ActionResult AddProduct([Bind(Prefix ="Item1")] Product item1, HttpPostedFileBase resim)
        {
            item1.ImagePath = ImageUploader.UploadImage("~/Pictures/", resim);
            prep.Add(item1);
            return RedirectToAction("ListProduct");
        }

        [Route("urun-guncelle")]
        public ActionResult UpdateProduct(int id)
        {
            return View(Tuple.Create( prep.GetByID(id),crep.SelectActives()));
        }

        [HttpPost]
        [Route("urun-guncelle")]
        public ActionResult UpdateProduct([Bind(Prefix ="Item1")] Product item, HttpPostedFileBase resim, string resimSil)
        {
            if (resimSil == "true")
            {
                item.ImagePath = "Dosya Boş";

            }
            else
            {
                if (resim == null)
                {
                    Product tempProduct = prep.GetByID(item.ID);
                    item.ImagePath = ImageUploader.UploadImage("~/Pictures/", resim);
                }
            }
            prep.Update(item);
            return RedirectToAction("ListProduct");
        }

        [Route("urun-sil")]
        public ActionResult DeleteProduct(int id)
        {
            prep.Delete(prep.GetByID(id));
            return RedirectToAction("ListProduct");
        }

        [Route("silinmis-urun-listesi")]
        public ActionResult ListDeletedProduct()
        {
            return View(prep.SelectDeleteds());
        }

    }
}