using MvcSinemaOdev.Models;
using MvcSinemaOdev.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSinemaOdev.Controllers
{
    public class FilmTurController : BaseController
    {

        public ActionResult Index()
        {
            FilmTurViewModel model = new FilmTurViewModel();
            return View(model);
        }
        public PartialViewResult FilmTurList()  //var olan  filmTurleri listeleme yap
        {
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();

            List<FilmTurViewModel> Listele = (from k in db.FilmTur
                                              select new FilmTurViewModel
                                                {
                                                    FilmTurName = k.FilmTurAd,
                                                    FilmTurNumber = k.FilmTurID
                                                }).OrderByDescending(f => f.FilmTurNumber).ToList();
            return PartialView(Listele);
        }
        [HttpPost]
        public string Create(FilmTurViewModel model)  //yeni bir tane film tur ekleme yapar
        {
            try
            {
                SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
                FilmTur ft = null;
                if (model.FilmTurNumber > 0)
                    ft = db.FilmTur.FirstOrDefault(f => f.FilmTurID == model.FilmTurNumber);
                else
                    ft = new FilmTur();
                ft.FilmTurAd = model.FilmTurName;
                if (model.FilmTurNumber == 0)
                    db.FilmTur.Add(ft);

                db.SaveChanges();

                return "1";
            }
            catch
            {
                return "-1";
            }
        }
        [HttpPost]
        public JsonResult FilmTurGetir(int id)
        {
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
            FilmTur kullanici = db.FilmTur.FirstOrDefault(f => f.FilmTurID == id);
            FilmTurViewModel model = new FilmTurViewModel();
            model.FilmTurName = kullanici.FilmTurAd;
            model.FilmTurNumber = kullanici.FilmTurID;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int Delete(int id)
        {
            try
            {
                SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
                var tur = db.FilmTur.FirstOrDefault(f => f.FilmTurID == id);
                db.FilmTur.Remove(tur);
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
