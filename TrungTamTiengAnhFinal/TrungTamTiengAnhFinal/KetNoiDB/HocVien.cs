namespace TrungTamTiengAnhFinal.KetNoiDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HocVien")]
    public partial class HocVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HocVien()
        {
            BangDiems = new HashSet<BangDiem>();
            DangKies = new HashSet<DangKy>();
        }

        [Key]
        [StringLength(10)]
        public string MaHV { get; set; }

        [StringLength(30)]
        public string TenHV { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NgaySinh { get; set; }

        [StringLength(3)]
        public string GioiTinhHV { get; set; }

        [StringLength(30)]
        public string DiaChi { get; set; }

        [StringLength(12)]
        public string SDTHV { get; set; }

        [StringLength(50)]
        public string EmailHV { get; set; }

        [StringLength(5)]
        public string MaLoaiHV { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NgayTiepNhan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BangDiem> BangDiems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DangKy> DangKies { get; set; }

        public virtual LoaiHV LoaiHV { get; set; }
    }
}
