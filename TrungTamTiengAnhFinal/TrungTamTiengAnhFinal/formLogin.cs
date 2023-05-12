using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_TTTA
{
    public partial class formLogin : Form
    {
        public formLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtuser.Focus();
        }

        private void btlogin_Click(object sender, EventArgs e)
        {
            if (txtuser.Text == "admin" && txtpassword.Text == "112233")
            {
                new formdashboard().Show();
                this.Hide();

            }

            else
            {
                MessageBox.Show("Tài khoản và mật khẩu bạn vừa nhập không đúng, hãy kiểm tra lại!");
                txtuser.Clear();
                txtpassword.Clear();
                txtuser.Focus();
            }
        }

        private void btclear_Click(object sender, EventArgs e)
        {
            txtuser.Clear();
            txtpassword.Clear();
            txtuser.Focus();
        }

        private void cbshowpass_CheckedChanged(object sender, EventArgs e)
        {
            if (cbshowpass.Checked)
            {
                txtpassword.PasswordChar = '\0';

            }
            else
            {
                txtpassword.PasswordChar = '•';

            }
        }
    }
}
