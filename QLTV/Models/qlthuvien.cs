using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QLTV.Models
{
    public partial class qlthuvien : DbContext
    {
        public qlthuvien()
            : base("name=qlthuvien")
        {
        }

        public virtual DbSet<Chitietphieumuon> Chitietphieumuons { get; set; }
        public virtual DbSet<Docgia> Docgias { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Maloai> Maloais { get; set; }
        public virtual DbSet<Nhanvien> Nhanviens { get; set; }
        public virtual DbSet<Phieumuon> Phieumuons { get; set; }
        public virtual DbSet<Sach> Saches { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sach>()
                .Property(e => e.Hinh)
                .IsUnicode(false);
        }

        //public System.Data.Entity.DbSet<QLTV.Models.Login> Login { get; set; }
    }
}
