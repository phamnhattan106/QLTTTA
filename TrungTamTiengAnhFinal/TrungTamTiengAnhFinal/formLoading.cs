using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrungTamTiengAnhFinal
{
    public partial class formLoading : Form
    {
        public formLoading()
        {
            InitializeComponent();
            LoadLab.Parent = pictureBox1;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            formDangNhap form = new formDangNhap();
            form.Show();
            this.Hide();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Opacity == 1)
            {
                timer1.Stop();
            }
            Opacity += .2;
        }
    }
}
