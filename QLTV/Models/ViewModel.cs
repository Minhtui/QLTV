using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QLTV.Models
{
    //class ViewModel chứa các trường dữ liệu lấy từ nhiều bảng để truyền sang View
    public class ViewModel
    {
        public Docgia docgia { get; set; }
        public Chitietphieumuon chitietphieumuon { get; set; }
        public Phieumuon phieumuon { get; set; }
        public Nhanvien nhanvien { get; set; }
        public Sach sach { get; set; }
    }
}