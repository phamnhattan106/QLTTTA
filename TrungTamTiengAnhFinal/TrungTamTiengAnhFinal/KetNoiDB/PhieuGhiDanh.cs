namespace TrungTamTiengAnhFinal.KetNoiDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuGhiDanh")]
    public partial class PhieuGhiDanh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuGhiDanh()
        {
            BangDiems = new HashSet<BangDiem>();
            DangKies = new HashSet<DangKy>();
        }

        [Key]
        [StringLength(10)]
        public string MaPhieu { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NgayGhiDanh { get; set; }

        [Column(TypeName = "money")]
        public decimal? DaDong { get; set; }

        [StringLength(6)]
        public string MaNV { get; set; }

        [Column(TypeName = "money")]
        public decimal? ConNo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BangDiem> BangDiems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DangKy> DangKies { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
