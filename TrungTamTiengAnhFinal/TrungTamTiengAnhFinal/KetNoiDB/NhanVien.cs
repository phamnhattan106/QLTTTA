namespace TrungTamTiengAnhFinal.KetNoiDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            PhieuGhiDanhs = new HashSet<PhieuGhiDanh>();
        }

        [Key]
        [StringLength(6)]
        public string MaNV { get; set; }

        [StringLength(30)]
        public string TenNV { get; set; }

        [StringLength(12)]
        public string SDTNV { get; set; }

        [StringLength(50)]
        public string EmailNV { get; set; }

        [StringLength(5)]
        public string MaLoaiNV { get; set; }

        public virtual LoaiNV LoaiNV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuGhiDanh> PhieuGhiDanhs { get; set; }
    }
}
