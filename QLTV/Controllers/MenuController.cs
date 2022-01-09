using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLTV.Models;
using System.Collections;

namespace QLTV.Controllers
{
    //public dynamic GetViewBag()
    //{
    //    return ViewBag;
    //}
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            using (qlthuvien db = new qlthuvien()) //Khai báo
            {
                //Khai báo biến
                var maloai = db.Maloais.ToList(); //ToList() trả về danh sách 
                Hashtable tenloaisach = new Hashtable(); //sử dụng đối tượng hashtable để hiện tên sách 
                foreach (var item in maloai)
                {
                    tenloaisach.Add(item.Maloaisach, item.Tenloaisach);
                }
                ViewBag.Tenloaisach = tenloaisach;
                return PartialView("Index");
            }
        }
    }
}