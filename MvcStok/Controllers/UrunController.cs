using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
		MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
			var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }
		[HttpGet]
		public ActionResult UrunEkle()
		{
			//DropDownListte Listelemek için komutlar
			List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
											 select new SelectListItem
											 {
												 Text = i.KATEGORIAD,
												 Value = i.KATEGORIID.ToString()
											 }).ToList();
			//Bu kod normalde get işlemi plduğu için bu sayfaya
			//geçtiğimizde çalışmaz ama ViewBag komtu sayesinde arkadan kodu taşıyıp getirir. 
			ViewBag.dgr = degerler;
			return View();
		}
		[HttpPost]
		public ActionResult UrunEkle(TBLURUNLER p1)
		{
			//Kategori Id yi almak için Linq sorgusu
			//Yukarıda yazdığımız Value değerine ulaşmak için yazılan Linq sorgusu
			var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
			p1.TBLKATEGORILER = ktg;

			db.TBLURUNLER.Add(p1);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult SIL(int id)
		{
			var urun = db.TBLURUNLER.Find(id);
			db.TBLURUNLER.Remove(urun);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult UrunGetir(int id)
		{
			var urun = db.TBLURUNLER.Find(id);
			List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
											 select new SelectListItem
											 {
												 Text = i.KATEGORIAD,
												 Value = i.KATEGORIID.ToString()
											 }).ToList(); 
			ViewBag.dgr = degerler;
			return View("UrunGetir", urun);
		}
		
		public ActionResult Guncelle(TBLURUNLER p)
		{
			var urun = db.TBLURUNLER.Find(p.URUNID);
			urun.URUNAD = p.URUNAD;
			urun.MARKA = p.MARKA;
			urun.STOK = p.STOK;
			urun.FIYAT = p.FIYAT;
			//urun.URUNKATEGORI = p.URUNKATEGORI;
			var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
			urun.URUNKATEGORI = ktg.KATEGORIID;
			db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}