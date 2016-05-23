using MvcSinemaOdev.Models;
using MvcSinemaOdev.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSinemaOdev.Controllers
{
    public class SeansController : BaseController
    {
        public ActionResult Index()
        {
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
            ViewBag.FilmIDNumber = new SelectList(db.Film, "FilmID", "FilmAd");
            SeansViewModel model = new SeansViewModel();
            return View(model);
        }
        public PartialViewResult SeansList()  //var olan  seansları listeleme yap
        {
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();

            List<SeansViewModel> Listele = (from k in db.Seans
                                            select new SeansViewModel
                                              {
                                                  SeansIDNumber=k.SeansID,
                                                  SeansSaatNumber=k.SeansSaat,
                                                  FilmIDNumber=k.FilmID
                                                  
                                              }).OrderByDescending(f => f.SeansIDNumber).ToList();
            return PartialView(Listele);
        }
        [HttpPost]
        public string Create(SeansViewModel model)  //yeni bir tane seans ekleme yapar
        {
            try
            {
                SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
                Seans ft = null;
                if (model.SeansIDNumber > 0)
                    ft = db.Seans.FirstOrDefault(f => f.SeansID == model.SeansIDNumber);
                else
                    ft = new Seans();
                ft.SeansSaat = model.SeansSaatNumber;
                ft.FilmID = model.FilmIDNumber;
             
                if (model.SeansIDNumber == 0)
                    db.Seans.Add(ft);

                db.SaveChanges();

                return "1";
            }
            catch
            {
                return "-1";
            }
        }
        [HttpPost]
        public JsonResult SeansGetir(int id)
        {
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
            Seans kullanici = db.Seans.FirstOrDefault(f => f.SeansID == id);
            SeansViewModel model = new SeansViewModel();
            model.SeansIDNumber = kullanici.SeansID;
            model.SeansSaatNumber = kullanici.SeansSaat;
            model.FilmIDNumber = kullanici.FilmID;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int Delete(int id)
        {
            try
            {
                SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
                var tur = db.Seans.FirstOrDefault(f => f.SeansID == id);
                db.Seans.Remove(tur);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return -1;
            }
        }


    }
}
