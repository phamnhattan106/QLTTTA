namespace TrungTamTiengAnhFinal.KetNoiDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GiangVien")]
    public partial class GiangVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GiangVien()
        {
            LopHocs = new HashSet<LopHoc>();
        }

        [Key]
        [StringLength(6)]
        public string MaGV { get; set; }

        [StringLength(30)]
        public string TenGV { get; set; }

        [StringLength(3)]
        public string GioiTinhGV { get; set; }

        [StringLength(12)]
        public string SDTGV { get; set; }

        [StringLength(50)]
        public string EmailGV { get; set; }

        [StringLength(10)]
        public string MaLoaiGV { get; set; }

        public virtual LoaiGV LoaiGV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LopHoc> LopHocs { get; set; }
    }
}
