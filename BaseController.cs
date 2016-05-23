using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MvcSinemaOdev.Controllers
{
    public class BaseController : System.Web.Mvc.Controller
    {
        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            if (Session["Admin"]==null || Session["Admin"].ToString()!="1")
            {
                filterContext.Result = new RedirectResult("~/Login/LoginForm");
                return ;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}