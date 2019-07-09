using Project.BLL.SingletonPattern;
using Project.DAL.Context;
using Project.MODEL.Entities;
using System.Web;
using System.Web.Mvc;
using static Project.MODEL.Entities.Log;

namespace Project.MVCUI.Models.Filters
{
    public class ActFilter : FilterAttribute, IActionFilter
    {
        public ActFilter()
        {
            user = new Log();
        }
        Log user;

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            LoglamaYap(filterContext, false);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LoglamaYap(filterContext, true);
        }

        public void LoglamaYap(ControllerContext filterContext, bool girisMi)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;

            if (filterContext.HttpContext.Session["member"] == null && filterContext.HttpContext.Session["admin"] == null)
            {
                user.WhoIs = "Anonim";
            }
            else if (filterContext.HttpContext.Session["member"] != null)
            {
                user.WhoIs = (filterContext.HttpContext.Session["member"] as AppUser).UserName;
                user.IPAdress = (filterContext.HttpContext.Session["member"] as AppUser).AppUserDetail.UserIP;
            }
            else if (filterContext.HttpContext.Session["admin"] != null)
            {
                user.WhoIs = (filterContext.HttpContext.Session["admin"] as AppUser).UserName;
                user.IPAdress = (filterContext.HttpContext.Session["admin"] as AppUser).AppUserDetail.UserIP;
            }
           
            switch (girisMi)
            {
                case false:
                    user.ActionName = (filterContext as ActionExecutedContext).ActionDescriptor.ActionName;
                    user.ControllorName = (filterContext as ActionExecutedContext).ActionDescriptor.ControllerDescriptor.ControllerName;
                    user.Information = "Kullanıcı Action'dan çıkış yaptı.";
                    user.Description = Keyword.Exit;
                    break;
                case true:
                    user.ActionName = (filterContext as ActionExecutingContext).ActionDescriptor.ActionName;
                    user.ControllorName = (filterContext as ActionExecutingContext).ActionDescriptor.ControllerDescriptor.ControllerName;
                    user.Information = "Kullanıcı Action'a giriş yaptı.";
                    user.Description = Keyword.Enter;
                    break;
            }

            user.UrlAccessed = request.RawUrl;
            using (LogContext logDb = new LogContext())
            {
                logDb.Logs.Add(user);
                logDb.SaveChanges();
            }

        }
    }
}