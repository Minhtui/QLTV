namespace QLTV.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sach")]
    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            Chitietphieumuons = new HashSet<Chitietphieumuon>();
        }

        [Key]
        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập mã sách!")]
        public string Masach { get; set; }

        public string Maloaisach { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập tên sách!")]
        public string Tensach { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập tên tác giả!")]
        public string Tacgia { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập nhà xuất bản!")]
        public string NXB { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập năm xuất bản!")]
        public string Namxuatban { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng sách!")]
        public double? Soluong { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập chọn hình ảnh!")]
        public string Hinh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chitietphieumuon> Chitietphieumuons { get; set; }

        public virtual Maloai Maloai { get; set; }
    }
}
