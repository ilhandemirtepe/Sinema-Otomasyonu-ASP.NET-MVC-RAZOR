using MvcSinemaOdev.Models;
using MvcSinemaOdev.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSinemaOdev.Controllers
{
    public class CalisanController : BaseController
    {


        public ActionResult Index()
        {
            CalisanViewModel model = new CalisanViewModel();
            return View(model);
        }
        public ActionResult CalisanList()  //var olan  filmTurleri listeleme yap
        {
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();

            List<CalisanViewModel> Listele = (from k in db.Calisan
                                              select new CalisanViewModel
                                              {
                                                  CalisanIDNumber = k.CalisanID,
                                                  CalisanAdNumber = k.CalisanAd,
                                                  CalisanKullaniciAdNumber = k.CalisanKullaniciAd,
                                                  CalisanParolaNumber = k.CalisanParola,
                                                  CalisanSoyadNumber = k.CalisanSoyad,
                                                  CalisanTcNoNumber = k.CalisanTcNo
                                              }).OrderByDescending(f => f.CalisanIDNumber).ToList();
            return View(Listele);
        }
        [HttpPost]
        public string Create(CalisanViewModel model)  //yeni bir tane film tur ekleme yapar
        {
            try
            {
                SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
                Calisan ft = null;
                if (model.CalisanIDNumber > 0)
                    ft = db.Calisan.FirstOrDefault(f => f.CalisanID== model.CalisanIDNumber);
                else
                    ft = new Calisan();
                ft.CalisanAd = model.CalisanAdNumber;
                ft.CalisanKullaniciAd = model.CalisanKullaniciAdNumber;
                ft.CalisanParola = model.CalisanParolaNumber;
                ft.CalisanSoyad = model.CalisanSoyadNumber;
                ft.CalisanTcNo = model.CalisanTcNoNumber;
                
          
                if (model.CalisanIDNumber == 0)
                    db.Calisan.Add(ft);

                db.SaveChanges();

                return "1";
            }
            catch
            {
                return "-1";
            }
        }
        [HttpPost]
        public JsonResult CalisanGetir(int id)
        {
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
            Calisan kullanici = db.Calisan.FirstOrDefault(f => f.CalisanID == id);
            CalisanViewModel model = new CalisanViewModel();
            model.CalisanAdNumber = kullanici.CalisanAd;
            model.CalisanIDNumber = kullanici.CalisanID;
            model.CalisanKullaniciAdNumber = kullanici.CalisanKullaniciAd;
            model.CalisanParolaNumber = kullanici.CalisanParola;
            model.CalisanSoyadNumber = kullanici.CalisanSoyad;
            model.CalisanTcNoNumber = kullanici.CalisanTcNo;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int Delete(int id)
        {
            try
            {
                SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
                var tur = db.Calisan.FirstOrDefault(f => f.CalisanID == id);
                db.Calisan.Remove(tur);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return -1;
            }
        }
        public ActionResult deneme()
        {
            return View();
        }

    }
}
