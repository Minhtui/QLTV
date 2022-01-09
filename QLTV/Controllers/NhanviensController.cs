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
    public class NhanviensController : Controller
    {
        private qlthuvien db = new qlthuvien();

        // GET: Nhanviens
        public ActionResult Index(string searchString)
        {
            //return View(db.Nhanviens.ToList());
            var nhanviens = from l in db.Nhanviens select l;
            if (!String.IsNullOrEmpty(searchString))
            {
               nhanviens = nhanviens.Where(s => s.Tennv.Contains(searchString));
            }
            return View(nhanviens.ToList());
        }

        // GET: Nhanviens/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nhanvien nhanvien = db.Nhanviens.Find(id);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }
            return View(nhanvien);
        }

        // GET: Nhanviens/Create
        public ActionResult Create()
        {
            var ketqua = db.Nhanviens.OrderByDescending(nv => nv.Manv);
            string str = "";
            foreach (var item in ketqua)
            {
                str = item.Manv;
                break;
            }
            string[] CatChuoi = str.Split('V');
            int s = Convert.ToInt32(CatChuoi[1]);

            return View(new Nhanvien()
            {
                Manv = "NV" + (s + 1)
            });
        }

        //
        public void Messagebox(string xMessage)
        {
            Response.Write("<script>alert('" + xMessage + "')</script>");
        }

        // POST: Nhanviens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Manv,Tennv,Ngaysinh,Gioitinh,Diachi,SDT")] Nhanvien nhanvien)
        {
            if (ModelState.IsValid)//Kiểm tra tính hợp lệ của cơ sở dữ liệu
            {
                try
                {
                    db.Nhanviens.Add(nhanvien);
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

        // GET: Nhanviens/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nhanvien nhanvien = db.Nhanviens.Find(id);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }
            return View(nhanvien);
        }

        // POST: Nhanviens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Manv,Tennv,Ngaysinh,Gioitinh,Diachi,SDT")] Nhanvien nhanvien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanvien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhanvien);
        }

        // GET: Nhanviens/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nhanvien nhanvien = db.Nhanviens.Find(id);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }
            return View(nhanvien);
        }

        // POST: Nhanviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
           try
            {
                Nhanvien nhanvien = db.Nhanviens.Find(id);
                db.Nhanviens.Remove(nhanvien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }    
            catch (Exception ex)
            {
                Messagebox("Người này đang phụ trách phiếu");
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
