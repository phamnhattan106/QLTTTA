namespace TrungTamTiengAnhFinal.KetNoiDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BangDiem")]
    public partial class BangDiem
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaHV { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(9)]
        public string MaLop { get; set; }

        [StringLength(10)]
        public string MaPhieu { get; set; }

        public int? DiemNghe { get; set; }

        public int? DiemNoi { get; set; }

        public int? DiemDoc { get; set; }

        public int? DiemViet { get; set; }

        public virtual HocVien HocVien { get; set; }

        public virtual LopHoc LopHoc { get; set; }

        public virtual PhieuGhiDanh PhieuGhiDanh { get; set; }
    }
}
