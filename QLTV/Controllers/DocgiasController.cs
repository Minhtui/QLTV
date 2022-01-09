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
    public class DocgiasController : Controller
    {
        private qlthuvien db = new qlthuvien();

        // GET: Docgias
        public ActionResult Index(string searchString)
        {
            //return View(db.Docgias.ToList());
            //Tìm kiếm
            var docgias = from l in db.Docgias select l;
            if (!String.IsNullOrEmpty(searchString))
            {
                docgias = docgias.Where(s => s.Tendg.Contains(searchString));
            }
            return View(docgias.ToList());
        }

        // GET: Docgias/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Docgia docgia = db.Docgias.Find(id);
            if (docgia == null)
            {
                return HttpNotFound();
            }
            return View(docgia);
        }

        // GET: Docgias/Create
        public ActionResult Create()
        {
            var ketqua = db.Docgias.OrderByDescending(dg => dg.Madg);
            string str = "";
            foreach (var item in ketqua)
            {
                str = item.Madg;
                break;
            }
            string[] CatChuoi = str.Split('G');
            int s = Convert.ToInt32(CatChuoi[1]);

            return View(new Docgia()
            {
                Madg = "DG" + (s + 1)
            });
        }
        //
        public void Messagebox(string xMessage)
        {
            Response.Write("<script>alert('" + xMessage + "')</script>");
        }
        // POST: Docgias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Madg,Tendg,Ngaysinh,Gioitinh,Diachi,Ngaycapthe")] Docgia docgia)
        {
            if (ModelState.IsValid)//Kiểm tra tính hợp lệ của cơ sở dữ liệu
            {
                try
                {
                    db.Docgias.Add(docgia);
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

        // GET: Docgias/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Docgia docgia = db.Docgias.Find(id);
            if (docgia == null)
            {
                return HttpNotFound();
            }
            return View(docgia);
        }

        // POST: Docgias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Madg,Tendg,Ngaysinh,Gioitinh,Diachi,Ngaycapthe")] Docgia docgia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(docgia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(docgia);
        }

        // GET: Docgias/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Docgia docgia = db.Docgias.Find(id);
            if (docgia == null)
            {
                return HttpNotFound();
            }
            return View(docgia);
        }

        // POST: Docgias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Docgia docgia = db.Docgias.Find(id);
                db.Docgias.Remove(docgia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Messagebox("Độc giả chưa trả sách");
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
