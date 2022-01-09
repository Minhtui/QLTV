namespace QLTV.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Maloai")]
    public partial class Maloai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Maloai()
        {
            Saches = new HashSet<Sach>();
        }

        [Key]
        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập mã loại sách!")]
        public string Maloaisach { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập tên loại sách!")]
        public string Tenloaisach { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sach> Saches { get; set; }
    }
}
