namespace QLTV.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Login")]
    public partial class Login
    {
        [Key]
        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng điền tài khoản!")]
        public string Username { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng điền password!")]
        public string Password { get; set; }
        //public string RememberMe { get; set; }
    }
}
