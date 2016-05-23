using MvcSinemaOdev.Models;
using MvcSinemaOdev.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSinemaOdev.Controllers
{
    public class FilmController : BaseController
    {

        public ActionResult Index()
        {
            FilmViewModel model = new FilmViewModel();
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
            ViewBag.FilmTurIDNumber = new SelectList(db.FilmTur, "FilmTurID", "FilmTurAd");
            return View(model);
        }
        public PartialViewResult FilmList()  //var olan  filmleri listeleme yap
        {
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();

            List<FilmViewModel> Listele = (from k in db.Film
                                           select new FilmViewModel
                                           {
                                                FilmAdName=k.FilmAd,
                                                FilmIDNumber=k.FilmID,
                                                FilmSureTime=k.FilmSure,
                                                FilmTurIDNumber=k.FilmTurID,
                                                FilmUcBoyutlumuCheck=k.FilmUcBoyutlumu,
                                                FilmYerlimiCheck=k.FilmYerlimi
                                          

                                           }).OrderByDescending(f => f.FilmIDNumber).ToList();
            return PartialView(Listele);
        }
        [HttpPost]
        public string Create(FilmViewModel model)  //yeni bir tane film tur ekleme yapar
        {
            try
            {
                SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
                Film ft = null;
                if (model.FilmIDNumber > 0)
                    ft = db.Film.FirstOrDefault(f => f.FilmID == model.FilmIDNumber);
                else
                    ft = new Film();
                ft.FilmAd = model.FilmAdName;
                ft.FilmSure = model.FilmSureTime;
                ft.FilmTurID = model.FilmTurIDNumber;
                ft.FilmUcBoyutlumu = model.FilmUcBoyutlumuCheck;
                ft.FilmYerlimi = model.FilmYerlimiCheck;
              
                if (model.FilmIDNumber == 0)
                    db.Film.Add(ft);
                db.SaveChanges();
                return "1";
            }
            catch
            {
                return "-1";
            }
        }
        [HttpPost]
        public JsonResult FilmGetir(int id)
        {
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
            Film kullanici = db.Film.FirstOrDefault(f => f.FilmID == id);
            FilmViewModel model = new FilmViewModel();
            model.FilmAdName = kullanici.FilmAd;
            model.FilmIDNumber = kullanici.FilmID;
            model.FilmSureTime = kullanici.FilmSure;
            model.FilmTurIDNumber = kullanici.FilmTurID;
            model.FilmUcBoyutlumuCheck = kullanici.FilmUcBoyutlumu;
            model.FilmYerlimiCheck = kullanici.FilmYerlimi;    
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int Delete(int id)
        {
            try
            {
                SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
                var fx = db.Film.FirstOrDefault(f => f.FilmID == id);
                db.Film.Remove(fx);
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
