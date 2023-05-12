namespace TrungTamTiengAnhFinal.KetNoiDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiGV")]
    public partial class LoaiGV
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiGV()
        {
            GiangViens = new HashSet<GiangVien>();
        }

        [Key]
        [StringLength(10)]
        public string MaLoaiGV { get; set; }

        [Required]
        [StringLength(30)]
        public string TenLoaiGV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GiangVien> GiangViens { get; set; }
    }
}
