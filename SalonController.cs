using MvcSinemaOdev.Models;
using MvcSinemaOdev.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSinemaOdev.Controllers
{
    public class SalonController : BaseController
    {

        public ActionResult Index()
        {
            SalonViewModel model = new SalonViewModel();
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
            ViewBag.SeansIDNumber = new SelectList(db.Seans, "SeansID", "SeansSaat");
            return View(model);
        }
        public PartialViewResult SalonList()  //var olan  filmleri listeleme yap
        {
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();

            List<SalonViewModel> Listele = (from k in db.Salon
                                            select new SalonViewModel
                                           {
                                               SalonAdNumber = k.SalonAd,
                                               SalonIDNumber = k.SalonID,
                                               SalonKapasiteNumber = k.SalonKapasite,
                                               SalonSiraSayisiNumber = k.SalonSiraSayisi,
                                               SeansIDNumber = k.SeansID
                                           }).OrderByDescending(f => f.SalonIDNumber).ToList();
            return PartialView(Listele);
        }
        [HttpPost]
        public string Create(SalonViewModel model)  //yeni bir tane film tur ekleme yapar
        {
            try
            {
                SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
                Salon ft = null;
                if (model.SalonIDNumber > 0)
                    ft = db.Salon.FirstOrDefault(f => f.SalonID == model.SalonIDNumber);
                else
                    ft = new Salon();
                ft.SalonAd = model.SalonAdNumber;
                ft.SalonKapasite = model.SalonKapasiteNumber;
                ft.SalonSiraSayisi = model.SalonSiraSayisiNumber;
                ft.SeansID = model.SeansIDNumber;

         

                if (model.SalonIDNumber == 0)
                    db.Salon.Add(ft);
                db.SaveChanges();
                return "1";
            }
            catch
            {
                return "-1";
            }
        }
        [HttpPost]
        public JsonResult SalonGetir(int id)
        {
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
            Salon kullanici = db.Salon.FirstOrDefault(f => f.SalonID== id);
            SalonViewModel model = new SalonViewModel();
            model.SalonIDNumber = kullanici.SalonID;
            model.SalonAdNumber = kullanici.SalonAd;
            model.SalonKapasiteNumber = kullanici.SalonKapasite;
            model.SalonSiraSayisiNumber = kullanici.SalonSiraSayisi;
            model.SeansIDNumber = kullanici.SeansID;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int Delete(int id)
        {
            try
            {
                SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
                var fx = db.Salon.FirstOrDefault(f => f.SalonID == id);
                db.Salon.Remove(fx);
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
