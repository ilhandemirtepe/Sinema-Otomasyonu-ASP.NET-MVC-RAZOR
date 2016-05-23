using MvcSinemaOdev.Models;
using MvcSinemaOdev.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSinemaOdev.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult LoginForm()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginForm(LoginViewModel model)
        {
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
            if (ModelState.IsValid)
            {
                try
                {
                    var q = db.Admin.Where(m => m.AdminUserName == model.UserNameViewModel && m.AdminPassWord == model.PasswordViewModel).ToList();
                    if (q.Count > 0)
                    {
                        Session["Admin"] = "1";
                        //return Redirect("~/Calisan/Index");
                        return RedirectToAction("Index", "Calisan");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı Adı veya şifre yanlış");
                    }
                }
                catch (Exception ex)
                {
                    
                   
                }
            }
           
            return View(model);
        }

    }
}
