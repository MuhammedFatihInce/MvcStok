using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
		// GET: Kategori
		MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
			//Listeleme işlemi
			//var degerler = db.TBLKATEGORILER.ToList();
			var degerler = db.TBLKATEGORILER.ToList().ToPagedList(sayfa, 4);
            return View(degerler);
        }
		[HttpGet]//Sayfa yüklenince bunu yapar 
		public ActionResult YeniKategori()
		{
			return View();
		}
		[HttpPost]//Sayfada işlem(gönderme, button) yapınca bunu yapar
		public ActionResult YeniKategori(TBLKATEGORILER p1)
		{
			if (!ModelState.IsValid)//Hata Mesajı Göndermek için
			{
				return View("YeniKategori");
			}
			//Ekleme işlemi
			db.TBLKATEGORILER.Add(p1);
			db.SaveChanges();
			Response.Redirect("/Kategori/Index/");
			return RedirectToAction("Index");
		}
		public ActionResult SIL(int id)
		{
			//Silme işlemi
			var kategori = db.TBLKATEGORILER.Find(id);
			db.TBLKATEGORILER.Remove(kategori);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult KategoriGetir(int id)
		{
			//Sayfalar arası veri taşıma
			var ktgr = db.TBLKATEGORILER.Find(id);
			return View("KategoriGetir", ktgr);
		}
		public ActionResult Guncelle(TBLKATEGORILER p1)
		{
			//Guncelleme İşlemi
			var ktg = db.TBLKATEGORILER.Find(p1.KATEGORIID);
			ktg.KATEGORIAD = p1.KATEGORIAD;
			db.SaveChanges();
			return RedirectToAction("Index");
		}
    }
}