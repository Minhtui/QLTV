using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLTV.Models;

namespace QLTV.Controllers
{
    public class PhieumuonsController : Controller
    {
        private qlthuvien db = new qlthuvien();

        // GET: Phieumuons
        public ActionResult Index(string searchString)
        {
            //var phieumuons = db.Phieumuons.Include(p => p.Docgia).Include(p => p.Nhanvien);
            //return View(phieumuons.ToList());

            //Tìm kiếm
            var phieumuons = from l in db.Phieumuons select l;
            if (!String.IsNullOrEmpty(searchString))
            {
                phieumuons = phieumuons.Where(s => s.Docgia.Tendg.Contains(searchString));
            }
            return View(phieumuons.ToList());
        }

        // GET: Phieumuons/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phieumuon phieumuon = db.Phieumuons.Find(id);
            if (phieumuon == null)
            {
                return HttpNotFound();
            }
            return View(phieumuon);
        }

        //
        public void Messagebox(string xMessage)
        {
            Response.Write("<script>alert('" + xMessage + "')</script>");
        }

        // GET: Phieumuons/Create
        public ActionResult Create()
        {
            //ViewBag.Madg = new SelectList(db.Docgias, "Madg", "Tendg");
            //ViewBag.Manv = new SelectList(db.Nhanviens, "Manv", "Tennv");
            //return View();

            var ketqua = db.Phieumuons.OrderByDescending(pm => pm.Maphieu);
            string str = "";
            foreach (var item in ketqua)
            {
                str = item.Maphieu;
                break;
            }
            string[] CatChuoi = str.Split('M');
            int s = Convert.ToInt32(CatChuoi[1]);

            return View(new Phieumuon()
            {
                Maphieu = "PM" + (s + 1)
            });
        }

        // POST: Phieumuons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Maphieu,Madg,Manv,Ngaylapphieu,Ngaytra")] Phieumuon phieumuon)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Phieumuons.Add(phieumuon);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch
                {
                    Messagebox("Thông báo: Bạn nhập trùng mã rồi !");
                }
            }

            ViewBag.Madg = new SelectList(db.Docgias, "Madg", "Tendg", phieumuon.Madg);
            ViewBag.Manv = new SelectList(db.Nhanviens, "Manv", "Tennv", phieumuon.Manv);
            return View();
        }

        // GET: Phieumuons/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phieumuon phieumuon = db.Phieumuons.Find(id);
            if (phieumuon == null)
            {
                return HttpNotFound();
            }
            ViewBag.Madg = new SelectList(db.Docgias, "Madg", "Tendg", phieumuon.Madg);
            ViewBag.Manv = new SelectList(db.Nhanviens, "Manv", "Tennv", phieumuon.Manv);
            return View(phieumuon);
        }

        // POST: Phieumuons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Maphieu,Madg,Manv,Ngaylapphieu,Ngaytra")] Phieumuon phieumuon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieumuon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Madg = new SelectList(db.Docgias, "Madg", "Tendg", phieumuon.Madg);
            ViewBag.Manv = new SelectList(db.Nhanviens, "Manv", "Tennv", phieumuon.Manv);
            return View(phieumuon);
        }

        // GET: Phieumuons/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phieumuon phieumuon = db.Phieumuons.Find(id);
            if (phieumuon == null)
            {
                return HttpNotFound();
            }
            return View(phieumuon);
        }

        // POST: Phieumuons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Phieumuon phieumuon = db.Phieumuons.Find(id);
                db.Phieumuons.Remove(phieumuon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Messagebox("Chưa trả sách ");
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
