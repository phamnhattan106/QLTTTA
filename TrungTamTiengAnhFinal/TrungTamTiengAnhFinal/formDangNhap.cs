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
    public partial class formDangNhap : Form
    {
        private TaiKhoanBT taiKhoanBT;
        public formDangNhap()
        {
            InitializeComponent();
            taiKhoanBT = new TaiKhoanBT();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
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
            string tenDangNhap = txtTenDangNhap.Text;
            string matKhau = txtMatKhau.Text;
            TaiKhoan taiKhoan = taiKhoanBT.LayTaiKhoan(tenDangNhap, matKhau);
            if (taiKhoan != null)
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK);
                formLoading2 fmMain = new formLoading2();
                fmMain.Show();
                this.Hide();
            }
         
            else
            {
                MessageBox.Show("Đăng nhập không thành công!", "Thông báo", MessageBoxButtons.OK);
                txtTenDangNhap.Clear();
                txtMatKhau.Clear();
                txtTenDangNhap.Focus();
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            formDangKy frmDangKy = new formDangKy();
            frmDangKy.StartPosition = FormStartPosition.CenterScreen;
            frmDangKy.ShowDialog();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            formDangKy frmDangKy = new formDangKy();
            frmDangKy.StartPosition = FormStartPosition.CenterScreen;
            frmDangKy.ShowDialog();
        }

        private void labelThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            txtTenDangNhap.Focus();
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
