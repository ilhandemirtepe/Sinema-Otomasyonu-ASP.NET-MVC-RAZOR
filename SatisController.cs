using MvcSinemaOdev.Models;
using MvcSinemaOdev.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSinemaOdev.Controllers
{
    public class SatisController : BaseController
    {
        public ActionResult Index()
        {
            SatisViewModel model = new SatisViewModel();
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
            ViewBag.CalisanIDNumber = new SelectList(db.Calisan, "CalisanID", "CalisanAd");
            ViewBag.MusteriIDNumber = new SelectList(db.Musteri, "MusteriID", "MusteriAdı");

            return View(model);
        }
        public PartialViewResult SatisList()  //var olan  filmleri listeleme yap
        {
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();

            List<SatisViewModel> Listele = (from k in db.Satis
                                            select new SatisViewModel
                                           {
                                               CalisanIDNumber=k.CalisanID,
                                               MusteriIDNumber=k.MusteriID,
                                               SatisFiyatNumber=k.SatisFiyat,
                                               SatisIDNumber=k.SatisID,
                                               SatisTarihNumber=k.SatisTarih,
                                               
                                           


                                           }).OrderByDescending(f => f.SatisIDNumber).ToList();
            return PartialView(Listele);
        }
        [HttpPost]
        public string Create(SatisViewModel model)  //yeni bir tane film tur ekleme yapar
        {
            try
            {
                SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
                Satis ft = null;
                if (model.SatisIDNumber > 0)
                    ft = db.Satis.FirstOrDefault(f => f.SatisID == model.SatisIDNumber);
                else
                    ft = new Satis();
                ft.MusteriID = model.MusteriIDNumber;
                ft.CalisanID = model.CalisanIDNumber;
                ft.SatisFiyat = model.SatisFiyatNumber;
                ft.SatisTarih = DateTime.Now;
                if (model.SatisIDNumber == 0)
                    db.Satis.Add(ft);
                db.SaveChanges();
                return "1";
            }
            catch
            {
                return "-1";
            }
        }
        [HttpPost]
        public JsonResult SatisGetir(int id)
        {
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
            Satis kullanici = db.Satis.FirstOrDefault(f => f.SatisID == id);
            SatisViewModel model = new SatisViewModel();
            model.CalisanIDNumber = kullanici.CalisanID;
            model.MusteriIDNumber = kullanici.MusteriID;
            model.SatisFiyatNumber = kullanici.SatisFiyat;
            model.SatisIDNumber = kullanici.SatisID;
            model.SatisTarihNumber = kullanici.SatisTarih;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int Delete(int id)
        {
            try
            {
                SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
                var fx = db.Satis.FirstOrDefault(f => f.SatisID == id);
                db.Satis.Remove(fx);
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
