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
    public class MaloaisController : Controller
    {
        private qlthuvien db = new qlthuvien();

        // GET: Maloais
        public ActionResult Index()
        {
            return View(db.Maloais.ToList());
        }

        // GET: Maloais/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maloai maloai = db.Maloais.Find(id);
            if (maloai == null)
            {
                return HttpNotFound();
            }
            return View(maloai);
        }
        //
        public void Messagebox(string xMessage)
        {
            Response.Write("<script>alert('" + xMessage + "')</script>");
        }

        // GET: Maloais/Create
        public ActionResult Create()
        {
            var ketqua = db.Maloais.OrderByDescending(ml => ml.Maloaisach);
            string str = "";
            foreach (var item in ketqua)
            {
                str = item.Maloaisach;
                break;
            }
            string[] CatChuoi = str.Split('L');
            int s = Convert.ToInt32(CatChuoi[1]);

            return View(new Maloai()
            {
                Maloaisach = "ML" + (s + 1)
            });
        }

        // POST: Maloais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Maloaisach,Tenloaisach")] Maloai maloai)
        {
            if (ModelState.IsValid)//Kiểm tra tính hợp lệ của cơ sở dữ liệu
            {
                try
                {
                    db.Maloais.Add(maloai);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch
                {
                    Messagebox("Thông báo: Bạn nhập trùng mã rồi !");
                }
            }
            return View();
        }

        // GET: Maloais/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maloai maloai = db.Maloais.Find(id);
            if (maloai == null)
            {
                return HttpNotFound();
            }
            return View(maloai);
        }

        // POST: Maloais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Maloaisach,Tenloaisach")] Maloai maloai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maloai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maloai);
        }

        // GET: Maloais/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maloai maloai = db.Maloais.Find(id);
            if (maloai == null)
            {
                return HttpNotFound();
            }
            return View(maloai);
        }

        // POST: Maloais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Maloai maloai = db.Maloais.Find(id);
                db.Maloais.Remove(maloai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Messagebox("Mã loại đang có sách, không được xóa !!!");
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
