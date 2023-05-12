using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrungTamTiengAnhFinal.DataTier;
using TrungTamTiengAnhFinal.KetNoiDB;
using TrungTamTiengAnhFinal.Libs;

namespace TrungTamTiengAnhFinal.BusinessTier
{
    class TaiKhoanBT
    {
        private readonly TaiKhoanDT taiKhoanDT;
        public TaiKhoanBT()
        {
            taiKhoanDT = new TaiKhoanDT();
        }
        public TaiKhoan LayTaiKhoan(string tenDangNhap, string matKhau)
        {
            //Ma hoa mat khau MD5
            matKhau = Helpers.MaHoaMD5(matKhau);
            return taiKhoanDT.LayTaiKhoan(tenDangNhap, matKhau);
        }

        internal bool LuuTaiKhoan(TaiKhoan taiKhoan, out string error)
        {
            try
            {
                taiKhoan.MatKhau = Helpers.MaHoaMD5(taiKhoan.MatKhau);
                return taiKhoanDT.LuuTaiKhoan(taiKhoan, out error);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
