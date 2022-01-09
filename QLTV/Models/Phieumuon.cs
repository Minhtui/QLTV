namespace QLTV.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Phieumuon")]
    public partial class Phieumuon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Phieumuon()
        {
            Chitietphieumuons = new HashSet<Chitietphieumuon>();
        }

        [Key]
        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng điền mã phiếu!")]
        public string Maphieu { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng điền mã độc giả!")]
        public string Madg { get; set; }

        [StringLength(50)]

        public string Manv { get; set; }

        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Vui lòng điền ngày lập phiếu!")]
        public DateTime? Ngaylapphieu { get; set; }

        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Vui lòng điền ngày trả!")]
        public DateTime? Ngaytra { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chitietphieumuon> Chitietphieumuons { get; set; }

        public virtual Docgia Docgia { get; set; }

        public virtual Nhanvien Nhanvien { get; set; }
    }
}
