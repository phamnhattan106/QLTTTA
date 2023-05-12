using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrungTamTiengAnhFinal.KetNoiDB;

namespace TrungTamTiengAnhFinal.DataTier
{
    class TaiKhoanDT
    {
        //kiem tra tai khoan
        public TaiKhoan LayTaiKhoan(string tenDangNhap, string matKhau)
        {
            using (var dbContext = new TrungTamAnhNguModel())
            {
                return dbContext.TaiKhoans.Where(p => p.TenDangNhap == tenDangNhap
               && p.MatKhau == matKhau).FirstOrDefault();
            }
        }
        //luu thong tin tai khoan
        public bool LuuTaiKhoan(TaiKhoan taiKhoan, out string error)
        {
            error = string.Empty;
            try
            {
                using (var dbContext = new TrungTamAnhNguModel())
                {
                    dbContext.TaiKhoans.Add(taiKhoan);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
