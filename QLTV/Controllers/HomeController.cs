using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using QLTV.Models;
using PagedList;

namespace QLTV.Controllers
{
    public class HomeController : Controller
    {
        qlthuvien db  = new qlthuvien();

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(Login login)
        {
            if (ModelState.IsValid)
            {
                using (qlthuvien db = new qlthuvien())
                {
                    var obj = db.Logins.Where(a => a.Username.Equals(login.Username) && a.Password.Equals(login.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        HttpCookie cookie = new HttpCookie("Username", obj.Username.ToString());
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(cookie);
                        //Session["UserName"] = obj.Username.ToString();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = "Vui lòng kiểm tra lại tên đăng nhập hoặc mật khẩu";
                    }
                    
                }
            }
            return View();
        }
        public ActionResult Index(string currentFilter, int?page ,string maloaisach = null, string SearchString = "")
        {
            if (SearchString != "")
            {
                page = 1;
                //khai báo biến để lấy danh sách của sách
                var saches = db.Saches.Include(s => s.Maloai).Where(x =>x.Tensach.ToUpper().Contains(SearchString.ToUpper())); //điều kiện ghi chữ in hết
                return View(saches);
            }
            else
                SearchString = currentFilter;
            ViewBag.CurrentFilter = currentFilter;
            if (maloaisach == null)
            {
                int pageSize = 12;
                int pageNumber = (page ?? 1);
                var saches = db.Saches.Include(s => s.Maloai).OrderBy(x => x.Tensach);
                //phải order trước skip
                return View(saches.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                var saches = db.Saches.Include(s => s.Maloai).Where(x => x.Maloaisach == maloaisach);
                return View(saches.ToList());
            }
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        public ActionResult Contact()
        {
            ViewBag.Message = "Liên hệ chúng tôi.";

            return View();
        }
    }
}