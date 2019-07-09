using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.TOOLS.CustomTools
{
    public static class ControlProduct
    {
        public static Cart KeepProduct(object SessionData)
        {
            Product bekleyenUrun = SessionData as Product;
            CartItem ci = new CartItem();
            ci.ID = bekleyenUrun.ID;
            ci.Name = bekleyenUrun.ProductName;
            ci.CategoryName = bekleyenUrun.Category.CategoryName;
            ci.ImagePath = bekleyenUrun.ImagePath;
            ci.Price = Convert.ToDecimal(bekleyenUrun.UnitPrice);
            Cart c = new Cart();
            c.SepeteEkle(ci);
            return c;
        }




        //public static Cart CartCreation(Product bekleyenUrun)
        //{ 
        //    Cart gelen = new Cart();
        //    gelen.SepeteEkle(Islem(bekleyenUrun));
        //    return gelen;
        //}

        //public static Cart CartCreation(Product bekleyenUrun, Cart gelen)
        //{
        //    gelen.SepeteEkle(Islem(bekleyenUrun));
        //    return gelen;
        //}
        //public static CartItem Islem(Product bekleyenUrun)
        //{
        //    CartItem ci = new CartItem();
        //    ci.ID = bekleyenUrun.ID;
        //    ci.Name = bekleyenUrun.ProductName;
        //    ci.Price = Convert.ToDecimal(bekleyenUrun.UnitPrice);
        //    ci.ImagePath = bekleyenUrun.ImagePath;
        //    return ci;
        //}
    }
}
