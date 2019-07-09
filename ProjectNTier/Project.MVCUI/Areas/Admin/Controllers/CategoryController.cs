using Project.BLL.RepositoryPattern.ConcreteRep;
using Project.MODEL.Entities;
using Project.MVCUI.Models.AuthenticationClasses;
using Project.MVCUI.Models.Filters;
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
    public class CategoryController : Controller
    {
        CategoryRepository crep;
        public CategoryController()
        {
            crep = new CategoryRepository();
        }

        // GET: Admin/Category
        [Route("kategori-listesi")]
        public ActionResult ListCategory()
        {
            return View(crep.SelectActives());
        }

        [Route("kategori-ekle")]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [Route("kategori-ekle")]
        public ActionResult AddCategory(Category item)
        {
            crep.Add(item);
            return RedirectToAction("ListCategory");
        }

        [Route("kategori-guncelle")]
        public ActionResult UpdateCategory(int id)
        {
            return View(crep.GetByID(id));
        }

        [HttpPost]
        [Route("kategori-guncelle")]
        public ActionResult UpdateCategory(Category item)
        {
            crep.Update(item);
            return RedirectToAction("ListCategory");
        }

        [Route("kategori-sil")]
        public ActionResult DeleteCategory(int id)
        {
            crep.Delete(crep.GetByID(id));
            return RedirectToAction("ListCategory");
        }

        [Route("silinmis-kategori-listesi")]
        public ActionResult ListDeletedCategory()
        {
            return View(crep.SelectDeleteds());
        }

    }
}