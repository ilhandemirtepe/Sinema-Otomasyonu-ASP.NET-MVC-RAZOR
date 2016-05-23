using MvcSinemaOdev.Models;
using MvcSinemaOdev.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSinemaOdev.Controllers
{
    public class MusteriController : BaseController
    {

        public ActionResult Index()
        {
            MusteriViewModel model = new MusteriViewModel();
            return View(model);
        }
        public PartialViewResult MusteriList()  //var olan  musteri listele
        {
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();

            List<MusteriViewModel> Listele = (from k in db.Musteri
                                              select new MusteriViewModel
                                              {
                                                  MusteriAdıNumber=k.MusteriAdı,
                                                  MusteriIDNumber=k.MusteriID,
                                                  MusteriPassWordNumber=k.MusteriPassWord,
                                                  MusteriSoyadNumber=k.MusteriSoyad,
                                                  MusteriUserNameNumber=k.MusteriUserName
                                              }).OrderByDescending(f => f.MusteriIDNumber).ToList();
            return PartialView(Listele);
        }
        [HttpPost]
        public string Create(MusteriViewModel model)  //yeni bir tane film tur ekleme yapar
        {
            try
            {
                SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
                Musteri ft = null;
                if (model.MusteriIDNumber > 0)
                    ft = db.Musteri.FirstOrDefault(f => f.MusteriID== model.MusteriIDNumber);
                else
                    ft = new Musteri();
                ft.MusteriAdı = model.MusteriAdıNumber;
                ft.MusteriSoyad = model.MusteriSoyadNumber;
                ft.MusteriUserName = model.MusteriUserNameNumber;
                ft.MusteriPassWord = model.MusteriPassWordNumber;

                if (model.MusteriIDNumber == 0)
                    db.Musteri.Add(ft);

                db.SaveChanges();

                return "1";
            }
            catch
            {
                return "-1";
            }
        }
        [HttpPost]
        public JsonResult MusteriGetir(int id)
        {
            SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
            Musteri kullanici = db.Musteri.FirstOrDefault(f => f.MusteriID == id);
            MusteriViewModel model = new MusteriViewModel();
            model.MusteriIDNumber = kullanici.MusteriID;
            model.MusteriAdıNumber = kullanici.MusteriAdı;
            model.MusteriPassWordNumber = kullanici.MusteriPassWord;
            model.MusteriSoyadNumber = kullanici.MusteriSoyad;
            model.MusteriUserNameNumber = kullanici.MusteriUserName;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int Delete(int id)
        {
            try
            {
                SinemaOtomasyonuEntities db = new SinemaOtomasyonuEntities();
                var tur = db.Musteri.FirstOrDefault(f => f.MusteriID == id);
                db.Musteri.Remove(tur);
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
