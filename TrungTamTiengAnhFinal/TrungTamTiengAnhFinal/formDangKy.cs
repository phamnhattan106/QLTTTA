using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrungTamTiengAnhFinal.KetNoiDB;
using TrungTamTiengAnhFinal.BusinessTier;

namespace TrungTamTiengAnhFinal
{
    public partial class formDangKy : Form
    {
        private TaiKhoanBT taiKhoanBT;
        public formDangKy()
        {
            InitializeComponent();
            taiKhoanBT = new TaiKhoanBT();
        }

        private void btnDangKi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenDangNhap.Text))
            {
                MessageBox.Show("Hãy điền tên đăng nhập!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            if (string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Hãy điền mật khẩu!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            string error;
            TaiKhoan taiKhoan = new TaiKhoan();
            taiKhoan.TenDangNhap = txtTenDangNhap.Text;
            taiKhoan.MatKhau = txtMatKhau.Text;
            if (taiKhoanBT.LuuTaiKhoan(taiKhoan, out error))
            {
                MessageBox.Show("Lưu tài khoản thành công!", "Thông báo", MessageBoxButtons.OK);
                this.Close();
            }
            else
            {
                MessageBox.Show("Lỗi: " + error);
            }
        }

        private void labelThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboXemMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (comboXemMatKhau.Checked)
            {
                txtMatKhau.PasswordChar = '\0';

            }
            else
            {
                txtMatKhau.PasswordChar = '*';

            }
        }
    }
}
