namespace TrungTamTiengAnhFinal.KetNoiDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LopHoc")]
    public partial class LopHoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LopHoc()
        {
            BangDiems = new HashSet<BangDiem>();
        }

        [Key]
        [StringLength(9)]
        public string MaLop { get; set; }

        [StringLength(30)]
        public string TenLop { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NgayBD { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NgayKT { get; set; }

        public int? SiSo { get; set; }

        [StringLength(4)]
        public string MaKH { get; set; }

        public bool? DangMo { get; set; }

        [StringLength(6)]
        public string MaGV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BangDiem> BangDiems { get; set; }

        public virtual GiangVien GiangVien { get; set; }

        public virtual KhoaHoc KhoaHoc { get; set; }
    }
}
