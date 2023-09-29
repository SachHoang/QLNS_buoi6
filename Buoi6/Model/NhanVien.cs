namespace Buoi6.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [Key]
        [StringLength(6)]
        public string MaNV { get; set; }

        [Required]
        [StringLength(30)]
        public string TenNV { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(6)]
        public string MaPB { get; set; }

        public virtual PhongBan PhongBan { get; set; }
    }
}
