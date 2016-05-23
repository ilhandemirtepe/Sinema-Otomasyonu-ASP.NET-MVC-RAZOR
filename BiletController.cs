using MvcSinemaOdev.Models;
using MvcSinemaOdev.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSinemaOdev.Controllers
{
    public class BiletController : BaseController
    {


        public ActionResult Index()
        {
            BiletViewModel model = new BiletViewModel();
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
            ViewBag.SatisIDNumber = new SelectList(db.Satis, "SatisID", "SatisFiyat");
            ViewBag.FilmIDNumber = new SelectList(db.Film, "FilmID", "FilmAd");
            return View(model);
        }
        public PartialViewResult BiletList()  //var olan biletleri listeleme yap
        {
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();

            List<BiletViewModel> Listele = (from k in db.Bilet
                                            select new BiletViewModel
                                              {
                                                  BiletIDNumber=k.BiletID,
                                                  BiletKoltukNumaraNumber=k.BiletKoltukNumara,
                                                  BiletTarihNumber=k.BiletTarih,
                                                  FilmIDNumber=k.FilmID,
                                                  SatisIDNumber=k.SatisID,

                                              }).OrderByDescending(f => f.BiletIDNumber).ToList();
            return PartialView(Listele);
        }
        [HttpPost]
        public string Create(BiletViewModel model)  //yeni bir tane bilet ekleme yapar
        {
            try
            {
                SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
                Bilet ft = null;
                if (model.BiletIDNumber > 0)
                    ft = db.Bilet.FirstOrDefault(f => f.BiletID == model.BiletIDNumber);
                else
                    ft = new Bilet();
                ft.BiletKoltukNumara = model.BiletKoltukNumaraNumber;
                ft.BiletTarih = DateTime.Now;
                ft.FilmID = model.FilmIDNumber;
                ft.SatisID = model.SatisIDNumber;
                if (model.BiletIDNumber == 0)
                    db.Bilet.Add(ft);

                db.SaveChanges();

                return "1";
            }
            catch
            {
                return "-1";
            }
        }
        [HttpPost]
        public JsonResult BiletGetir(int id)
        {
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
            Bilet kullanici = db.Bilet.FirstOrDefault(f => f.BiletID == id);
           BiletViewModel model = new BiletViewModel();
           model.BiletIDNumber = kullanici.BiletID;
           model.BiletKoltukNumaraNumber = kullanici.BiletKoltukNumara;
           model.BiletTarihNumber = kullanici.BiletTarih;
           model.FilmIDNumber = kullanici.FilmID;
           model.SatisIDNumber = model.SatisIDNumber;

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int Delete(int id)
        {
            try
            {
                SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
                var tur = db.Bilet.FirstOrDefault(f => f.BiletID == id);
                db.Bilet.Remove(tur);
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
