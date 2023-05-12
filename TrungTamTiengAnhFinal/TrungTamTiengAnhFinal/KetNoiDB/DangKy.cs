namespace TrungTamTiengAnhFinal.KetNoiDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DangKy")]
    public partial class DangKy
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaHV { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(4)]
        public string MaKH { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string MaPhieu { get; set; }

        public virtual HocVien HocVien { get; set; }

        public virtual KhoaHoc KhoaHoc { get; set; }

        public virtual PhieuGhiDanh PhieuGhiDanh { get; set; }
    }
}
