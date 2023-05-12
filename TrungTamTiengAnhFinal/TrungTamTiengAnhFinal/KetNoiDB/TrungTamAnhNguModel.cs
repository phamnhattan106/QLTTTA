using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TrungTamTiengAnhFinal.KetNoiDB
{
    public partial class TrungTamAnhNguModel : DbContext
    {
        public TrungTamAnhNguModel()
            : base("name=TrungTamAnhNguModel")
        {
        }

        public virtual DbSet<BangDiem> BangDiems { get; set; }
        public virtual DbSet<DangKy> DangKies { get; set; }
        public virtual DbSet<GiangVien> GiangViens { get; set; }
        public virtual DbSet<HocVien> HocViens { get; set; }
        public virtual DbSet<KhoaHoc> KhoaHocs { get; set; }
        public virtual DbSet<LoaiGV> LoaiGVs { get; set; }
        public virtual DbSet<LoaiHV> LoaiHVs { get; set; }
        public virtual DbSet<LoaiNV> LoaiNVs { get; set; }
        public virtual DbSet<LopHoc> LopHocs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<PhieuGhiDanh> PhieuGhiDanhs { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BangDiem>()
                .Property(e => e.MaHV)
                .IsUnicode(false);

            modelBuilder.Entity<BangDiem>()
                .Property(e => e.MaLop)
                .IsUnicode(false);

            modelBuilder.Entity<BangDiem>()
                .Property(e => e.MaPhieu)
                .IsUnicode(false);

            modelBuilder.Entity<DangKy>()
                .Property(e => e.MaHV)
                .IsUnicode(false);

            modelBuilder.Entity<DangKy>()
                .Property(e => e.MaKH)
                .IsUnicode(false);

            modelBuilder.Entity<DangKy>()
                .Property(e => e.MaPhieu)
                .IsUnicode(false);

            modelBuilder.Entity<GiangVien>()
                .Property(e => e.MaGV)
                .IsUnicode(false);

            modelBuilder.Entity<GiangVien>()
                .Property(e => e.SDTGV)
                .IsUnicode(false);

            modelBuilder.Entity<GiangVien>()
                .Property(e => e.EmailGV)
                .IsUnicode(false);

            modelBuilder.Entity<GiangVien>()
                .Property(e => e.MaLoaiGV)
                .IsUnicode(false);

            modelBuilder.Entity<HocVien>()
                .Property(e => e.MaHV)
                .IsUnicode(false);

            modelBuilder.Entity<HocVien>()
                .Property(e => e.SDTHV)
                .IsUnicode(false);

            modelBuilder.Entity<HocVien>()
                .Property(e => e.EmailHV)
                .IsUnicode(false);

            modelBuilder.Entity<HocVien>()
                .Property(e => e.MaLoaiHV)
                .IsUnicode(false);

            modelBuilder.Entity<HocVien>()
                .HasMany(e => e.BangDiems)
                .WithRequired(e => e.HocVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HocVien>()
                .HasMany(e => e.DangKies)
                .WithRequired(e => e.HocVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhoaHoc>()
                .Property(e => e.MaKH)
                .IsUnicode(false);

            modelBuilder.Entity<KhoaHoc>()
                .Property(e => e.HocPhi)
                .HasPrecision(19, 4);

            modelBuilder.Entity<KhoaHoc>()
                .Property(e => e.MaPhieu)
                .IsUnicode(false);

            modelBuilder.Entity<KhoaHoc>()
                .HasMany(e => e.DangKies)
                .WithRequired(e => e.KhoaHoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiGV>()
                .Property(e => e.MaLoaiGV)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiGV>()
                .Property(e => e.TenLoaiGV)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiHV>()
                .Property(e => e.MaLoaiHV)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiNV>()
                .Property(e => e.MaLoaiNV)
                .IsUnicode(false);

            modelBuilder.Entity<LopHoc>()
                .Property(e => e.MaLop)
                .IsUnicode(false);

            modelBuilder.Entity<LopHoc>()
                .Property(e => e.MaKH)
                .IsUnicode(false);

            modelBuilder.Entity<LopHoc>()
                .Property(e => e.MaGV)
                .IsUnicode(false);

            modelBuilder.Entity<LopHoc>()
                .HasMany(e => e.BangDiems)
                .WithRequired(e => e.LopHoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.SDTNV)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.EmailNV)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaLoaiNV)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuGhiDanh>()
                .Property(e => e.MaPhieu)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuGhiDanh>()
                .Property(e => e.DaDong)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PhieuGhiDanh>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuGhiDanh>()
                .Property(e => e.ConNo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PhieuGhiDanh>()
                .HasMany(e => e.DangKies)
                .WithRequired(e => e.PhieuGhiDanh)
                .WillCascadeOnDelete(false);
        }
    }
}
