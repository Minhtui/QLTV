using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLTV.Models;
using System.IO;
using PagedList;

namespace QLTV.Controllers
{
    public class SachesController : Controller
    {
        private qlthuvien db = new qlthuvien();

        // GET: Saches
        public ActionResult Index(string sortOder, int ? page, string searchString, string maloaisach = null)
        {
            //Tìm kiếm
            var saches = from l in db.Saches select l;
            if (!String.IsNullOrEmpty(searchString))
            {
                saches = saches.Where(s => s.Tensach.Contains(searchString));
            }

            //sắp xếp danh sách của sách theo tên sách
            ViewBag.SortByName = String.IsNullOrEmpty(sortOder) ? "ten_desc" : "";
            ViewBag.CurrentSort = sortOder;
            switch (sortOder)
            {
                case "ten_desc":
                    saches = saches.OrderByDescending(s => s.Tensach);
                    break;
                default: //mặc định sắp xếp theo tên sách
                    saches = saches.OrderBy(s => s.Tensach);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(saches.ToPagedList(pageNumber, pageSize));
        }

        // GET: Saches/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // GET: Saches/Create
        public ActionResult Create()
        {
            var ketqua = db.Saches.OrderByDescending(sa => sa.Masach);
            string str = "";
            foreach (var item in ketqua)
            {
                str = item.Masach;
                break;
            }
            string[] CatChuoi = str.Split('S');
            int s = Convert.ToInt32(CatChuoi[1]);

            return View(new Sach()
            {
                Masach = "MS" + (s + 1)
            });
        }

        //
        public void Messagebox(string xMessage)
        {
            Response.Write("<script>alert('" + xMessage + "')</script>");
        }
        // POST: Saches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Masach,Maloaisach,Tensach,Tacgia,NXB,Namxuatban,Soluong,Hinh")] Sach sach,
            HttpPostedFileBase Hinh)
        {
            if (ModelState.IsValid)
            {
                //Kiểm tra tính hợp lệ của cơ sở dữ liệu
                try
                {
                    db.Saches.Add(sach);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch
                {
                    Messagebox("Thông báo: Bạn nhập trùng mã rồi !");
                }

                //Hình ảnh
                if (Hinh != null && Hinh.ContentLength > 0)
                {
                    string filename = Path.GetFileName(Hinh.FileName);
                    string path = Server.MapPath("~/Images" + filename);
                    sach.Hinh = @"Images/" + filename;
                    Hinh.SaveAs(path);
                }
                
            }

            ViewBag.Maloaisach = new SelectList(db.Maloais, "Maloaisach", "Tenloaisach", sach.Maloaisach);
            return View();
        }

        // GET: Saches/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            ViewBag.Maloaisach = new SelectList(db.Maloais, "Maloaisach", "Tenloaisach", sach.Maloaisach);
            return View(sach);
        }

        // POST: Saches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Masach,Maloaisach,Tensach,Tacgia,NXB,Namxuatban,Soluong,Hinh")] Sach sach,
            HttpPostedFileBase HinhUpload, string Hinh)
        {
            if (ModelState.IsValid)
            {
                if (HinhUpload != null && HinhUpload.ContentLength > 0)
                {
                    string filename = Path.GetFileName(HinhUpload.FileName);
                    string path = Server.MapPath("~/Images" + filename);
                    sach.Hinh = @"Images/" + filename;
                    HinhUpload.SaveAs(path);
                }
                else
                {
                    sach.Hinh = Hinh; //nếu không chọn hình mới thì giữ hình cũ
                }
                db.Entry(sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Maloaisach = new SelectList(db.Maloais, "Maloaisach", "Tenloaisach", sach.Maloaisach);
            return View(sach);
        }

        // GET: Saches/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // POST: Saches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Sach sach = db.Saches.Find(id);
                db.Saches.Remove(sach);
                db.SaveChanges();
                //Xóa file hình trong thư mục Images
                System.IO.File.Delete(Server.MapPath("~/" + sach.Hinh));
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Messagebox("Xóa gì! Đấm mặt mày giờ !!!");
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
