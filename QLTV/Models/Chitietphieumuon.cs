namespace QLTV.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Chitietphieumuon")]
    public partial class Chitietphieumuon
    {
        [Key]
        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng không để trống mục này!")]
        public string MaCTPM { get; set; }

        [StringLength(50)]
        public string Maphieu { get; set; }

        [StringLength(50)]
        public string Masach { get; set; }

        public virtual Phieumuon Phieumuon { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
