namespace QLTV.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Docgia")]
    public partial class Docgia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Docgia()
        {
            Phieumuons = new HashSet<Phieumuon>();
        }

        [Key]
        [StringLength(50)]
        [Required(ErrorMessage = "Bạn chưa điền mã độc giả!")]
        public string Madg { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Bạn chưa điền tên độc giả!")]
        public string Tendg { get; set; }

        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Vui lòng điền ngày sinh!")]
        public DateTime? Ngaysinh { get; set; }

        public bool? Gioitinh { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng điền địa chỉ!")]
        public string Diachi { get; set; }

        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Vui lòng điền ngày cấp thẻ!")]
        public DateTime? Ngaycapthe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Phieumuon> Phieumuons { get; set; }
    }
}
