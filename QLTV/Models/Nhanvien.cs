namespace QLTV.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Nhanvien")]
    public partial class Nhanvien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Nhanvien()
        {
            Phieumuons = new HashSet<Phieumuon>();
        }

        [Key]
        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng điền mã nhân viên!")]
        public string Manv { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng điền tên nhân viên!")]
        public string Tennv { get; set; }

        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Vui lòng điền ngày sinh!")]
        public DateTime? Ngaysinh { get; set; }

        public bool? Gioitinh { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng điền địa chỉ!")]
        public string Diachi { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng điền số điện thoại!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Số điện thoại không hợp lệ!")]
        public string SDT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Phieumuon> Phieumuons { get; set; }
    }
}
