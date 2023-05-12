using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrungTamTiengAnhFinal.Giảng_Viên;
using TrungTamTiengAnhFinal.Học_Viên;
using TrungTamTiengAnhFinal.Nhân_Viên;
using TrungTamTiengAnhFinal.Quản_trị;
using TrungTamTiengAnhFinal.Libs;

namespace TrungTamTiengAnhFinal
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
            customDesign();
                    
        }

        
        #region Thiet ke form
        private void customDesign()
        {
            panelHocVien.Visible = false;
            panelGiangVien.Visible = false;
            panelNhanVien.Visible = false;
            panelQuanTri.Visible = false;
        }

        private void hideMenu()
        {
            if (panelHocVien.Visible == true)
                panelHocVien.Visible = false;
            if (panelNhanVien.Visible == true)
                panelNhanVien.Visible = false;
            if (panelNhanVien.Visible == true)
                panelNhanVien.Visible = false;
            if (panelQuanTri.Visible == true)
                panelQuanTri.Visible = false;
        }

        private void showMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        //Hoc Vien 

        private void btnHocVien_Click(object sender, EventArgs e)
        {
            showMenu(panelHocVien);
        }

        private void btnTiepNhanHV_Click(object sender, EventArgs e)
        {
            openchildform(new formTiepNhanHocVien());
            hideMenu();
        }

        private void btnGhiDanhHV_Click(object sender, EventArgs e)
        {
            openchildform(new LuuPhieuGhiDanh());
            hideMenu();
        }

        private void btnQuanLyDiemHV_Click(object sender, EventArgs e)
        {
            openchildform(new QuanLyDiemHV());
            hideMenu();
        }

        private void btnBangDiemHV_Click(object sender, EventArgs e)
        {
            new formBangDiemHV().ShowDialog();
            hideMenu();
        }

        private void btnHocPhiHV_Click(object sender, EventArgs e)
        {
            openchildform(new formHocPhiHV());
            hideMenu();
        }

        private void btnXepLopHV_Click(object sender, EventArgs e)
        {
            openchildform(new formXepLopHV());
            hideMenu();
        }

        //Giang Vien

        private void btnGiangVien_Click(object sender, EventArgs e)
        {
            showMenu(panelGiangVien);
        }

        private void btnCacLopDaDayGV_Click(object sender, EventArgs e)
        {
            openchildform(new formCacLopDaDayGV());
            hideMenu();
        }

        //Nhan Vien

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            showMenu(panelNhanVien);
        }

        private void btnThongKeDiemNV_Click(object sender, EventArgs e)
        {
            openchildform(new formThongKeDiemTheoLopNV());
            hideMenu();
        }

        //Quan Tri

        private void btnQuanTri_Click(object sender, EventArgs e)
        {
            showMenu(panelQuanTri);
        }

        private void btnQuanLyHV_Click(object sender, EventArgs e)
        {
            openchildform(new formQuanLyHV());
            hideMenu();
        }

        private void btnQuanLyNV_Click(object sender, EventArgs e)
        {
            openchildform(new QuanLyNV());
            hideMenu();
        }

        private void btnQuanLyGV_Click(object sender, EventArgs e)
        {
            openchildform(new formQuanLyGV());
            hideMenu();
        }

        private void btnQuanLyLopHoc_Click(object sender, EventArgs e)
        {
            openchildform(new formQuanLyLopHoc());
            hideMenu();
        }

        private void btnQuanLyKhoaHoc_Click(object sender, EventArgs e)
        {
            openchildform(new formQuanLyKhoaHoc());
            hideMenu();
        }

        private void btnQuanLyHocPhi_Click(object sender, EventArgs e)
        {
            openchildform(new formQuanLyHocPhi());
            hideMenu();
        }

        private Form activeForm = null;

        //Cửa sổ nhỏ dành cho button

        private void openchildform(Form childform)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            panel_main.Controls.Add(childform);
            panel_main.Tag = childform;
            childform.BringToFront();
            childform.Show();
        }

        private void btnTroGiup_Click(object sender, EventArgs e)
        {
            new formThongTin().ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if ( Opacity == 1)
            {
                timer1.Stop();
            }
            Opacity += .2;
        }
        #endregion

    }
}

