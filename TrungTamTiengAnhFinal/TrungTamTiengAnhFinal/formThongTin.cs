using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrungTamTiengAnhFinal
{
    public partial class formThongTin : Form
    {
        public formThongTin()
        {
            InitializeComponent();
        }

        private void formThongTin_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://discord.gg/5XGTTR2F");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
