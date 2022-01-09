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
    public class ChitietphieumuonsController : Controller
    {
        private qlthuvien db = new qlthuvien();

        // GET: Chitietphieumuons
        public ActionResult Index(string maphieu = null, string Maphieu = null)
        {

            var chitietphieumuons = db.Chitietphieumuons.Include(c => c.Sach).Where(x => x.Maphieu == maphieu);

            //Hiện mã và tên độc giả của chi tiết phiếu mượn
            using (qlthuvien db = new qlthuvien())
            {
                List<Docgia> docgia = db.Docgias.ToList();
                List<Phieumuon> phieumuon = db.Phieumuons.ToList();
                var main = from h in phieumuon
                           join k in docgia on h.Madg equals k.Madg
                           where (h.Maphieu == Maphieu)
                           select new ViewModel
                           {
                               phieumuon = h,
                               docgia = k
                           };
                //truyền đối tượng trên sang View
                ViewBag.Main = main;
                return View(chitietphieumuons.ToList());
            }    
        }

        // GET: Chitietphieumuons/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chitietphieumuon chitietphieumuon = db.Chitietphieumuons.Find(id);
            if (chitietphieumuon == null)
            {
                return HttpNotFound();
            }
            return View(chitietphieumuon);
        }

        // GET: Chitietphieumuons/Create
        public ActionResult Create()
        {
            var ketqua = db.Chitietphieumuons.OrderByDescending(p => p.MaCTPM);
            string str = "";
            foreach (var item in ketqua)
            {
                str = item.MaCTPM;
                break;
            }
            string[] arrListStr = str.Split('T');
            int s = Convert.ToInt32(arrListStr[1]);
            return View(new Chitietphieumuon() { MaCTPM = "CT" + (s + 1) });
        }

        //
        public void Messagebox(string xMessage)
        {
            Response.Write("<script>alert('" + xMessage + "')</script>");
        }
        // POST: Chitietphieumuons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCTPM,Maphieu,Masach")] Chitietphieumuon chitietphieumuon)
        {

            if (ModelState.IsValid)//Kiểm tra tính hợp lệ của cơ sở dữ liệu
            {
                db.Chitietphieumuons.Add(chitietphieumuon);
                var sachs = db.Saches.Find(chitietphieumuon.Masach);
                if (sachs.Soluong > 1)
                {
                    sachs.Soluong = sachs.Soluong - 1;
                    db.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("Index");

                }
                else
                {
                    Messagebox("Thông báo: Sách này đã cho mượn hết !");
                }
            }
            ViewBag.Maphieu = new SelectList(db.Phieumuons, "Maphieu", "Madg", chitietphieumuon.Maphieu);
            ViewBag.Masach = new SelectList(db.Saches, "Masach", "Tensach", chitietphieumuon.Masach);
            return View();
        }

        // GET: Chitietphieumuons/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chitietphieumuon chitietphieumuon = db.Chitietphieumuons.Find(id);
            if (chitietphieumuon == null)
            {
                return HttpNotFound();
            }
            ViewBag.Maphieu = new SelectList(db.Phieumuons, "Maphieu", "Madg", chitietphieumuon.Maphieu);
            ViewBag.Masach = new SelectList(db.Saches, "Masach", "Tensach", chitietphieumuon.Masach);
            return View(chitietphieumuon);
        }

        // POST: Chitietphieumuons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCTPM,Maphieu,Masach")] Chitietphieumuon chitietphieumuon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chitietphieumuon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Maphieu = new SelectList(db.Phieumuons, "Maphieu", "Madg", chitietphieumuon.Maphieu);
            ViewBag.Masach = new SelectList(db.Saches, "Masach", "Tensach", chitietphieumuon.Masach);
            return View(chitietphieumuon);
        }

        // GET: Chitietphieumuons/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chitietphieumuon chitietphieumuon = db.Chitietphieumuons.Find(id);
            if (chitietphieumuon == null)
            {
                return HttpNotFound();
            }
            return View(chitietphieumuon);
        }

        // POST: Chitietphieumuons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Chitietphieumuon chitietphieumuon = db.Chitietphieumuons.Find(id);//xuống database lấy chi tiết phiếu mượn theo id
            var sachs = db.Saches.Find(chitietphieumuon.Masach); //xuống database sách mượn theo mã sách trong chi tiết phiếu mượn
            sachs.Soluong = sachs.Soluong + 1;
            db.Chitietphieumuons.Remove(chitietphieumuon);
            db.SaveChanges();
            return RedirectToAction("Index");
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
