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

namespace TrungTamTiengAnhFinal.Học_Viên
{
    public partial class LuuPhieuGhiDanh : Form
    {
        List<DangKy> finddangKies;
        List<DangKy> dangKies;
        List<HocVien> hocViens;
        List<KhoaHoc> khoaHocs;
        List<DangKy> listHocPhiBangHocVien;
        String MaHV;
        
        TrungTamAnhNguModel context = new TrungTamAnhNguModel();
        public LuuPhieuGhiDanh()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LuuPhieuGhiDanh_Load(object sender, EventArgs e)
        {
            khoaHocs = context.KhoaHocs.ToList();
            hocViens = context.HocViens.ToList();
            dangKies = context.DangKies.ToList();
            FillHocVienToDataGridView(hocViens);
            FillDangKyToDataGridView(dangKies);
            FillKhoaHocToComboBox(khoaHocs);
        }
        #region Truyền dữ liệu
        private void FillHocVienToDataGridView(List<HocVien> hocViens)
        {
            gridDSHV.Rows.Clear();
            foreach (var item in hocViens)
            {
                int i = gridDSHV.Rows.Add();
                gridDSHV.Rows[i].Cells[0].Value = item.MaHV;
                gridDSHV.Rows[i].Cells[1].Value = item.TenHV;
                gridDSHV.Rows[i].Cells[2].Value = item.NgaySinh;
                gridDSHV.Rows[i].Cells[3].Value = item.GioiTinhHV;
            }
        }

      private void FillDangKyToDataGridView(List<DangKy> listdangKies)
        {
            gridPhieuGhiDanh.Rows.Clear();
            foreach (var item in listdangKies)
            {
                int index = gridPhieuGhiDanh.Rows.Add();
                gridPhieuGhiDanh.Rows[index].Cells[0].Value = item.MaHV;
                gridPhieuGhiDanh.Rows[index].Cells[1].Value = item.HocVien.TenHV;
                gridPhieuGhiDanh.Rows[index].Cells[2].Value = item.MaPhieu;
                gridPhieuGhiDanh.Rows[index].Cells[3].Value = item.PhieuGhiDanh.NgayGhiDanh.ToString();
                gridPhieuGhiDanh.Rows[index].Cells[4].Value = item.KhoaHoc.TenKH;
                gridPhieuGhiDanh.Rows[index].Cells[5].Value = item.KhoaHoc.HocPhi;
                gridPhieuGhiDanh.Rows[index].Cells[6].Value = item.PhieuGhiDanh.DaDong;
                gridPhieuGhiDanh.Rows[index].Cells[7].Value = item.KhoaHoc.HocPhi - item.PhieuGhiDanh.DaDong;

            }
        }

        private void FillKhoaHocToComboBox(List<KhoaHoc> listkhoaHocs)
        {
            this.cmbKhoaHoc.DataSource = listkhoaHocs;
            this.cmbKhoaHoc.DisplayMember = "TenKH";
            this.cmbKhoaHoc.ValueMember = "MaKH";
        }




        #endregion

        #region Click vào GridDSH thì sẽ hiện ra trong GridHocPhi
        private void gridDSHV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MaHV = gridDSHV.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            listHocPhiBangHocVien = context.DangKies.Where(p => p.MaHV == MaHV).ToList();
            FillDangKyToDataGridView(listHocPhiBangHocVien);
        }
        #endregion


        #region Đặt lại
        private void btnDatLaiPhieu_Click(object sender, EventArgs e)
        {
            LuuPhieuGhiDanh_Load(sender, e);
        }
        #endregion

        #region Tìm kiếm thông tin
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //Tolower: Chuyen chu  In thanh chu thuong
            finddangKies = new List<DangKy>();

            String hoten = txtMaHVSearch.Text.ToLower();
            // var la kieu du lieu dong ( kieu du lieu suy luan, suy luan ra kieu du lieu dua vao bien truyen vao )
            foreach (var items in dangKies)
            {
                if (txtMaHVSearch.Text == "")
                {

                    finddangKies.Add(items);


                }
                else if (txtMaHVSearch.Text != "")
                {
                    if (items.HocVien.TenHV.ToString().ToLower().IndexOf(hoten) != -1)
                    {
                        finddangKies.Add(items);
                    }
                }
                else if (txtMaHVSearch.Text == "")
                {

                    finddangKies.Add(items);

                }

            }
            FillDangKyToDataGridView(finddangKies);
        }

        #endregion

        private void gridPhieuGhiDanh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridPhieuGhiDanh.Rows[e.RowIndex].Cells[e.ColumnIndex] != null)
            {
                gridPhieuGhiDanh.CurrentRow.Selected = true;
                txtMaHV.Text = gridPhieuGhiDanh.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                txtTenHV.Text = gridPhieuGhiDanh.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                txtMaPhieu.Text = gridPhieuGhiDanh.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                dateNgayGhiDanh.Text = gridPhieuGhiDanh.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                cmbKhoaHoc.Text = gridPhieuGhiDanh.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
                txtHocPhi.Text = gridPhieuGhiDanh.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
                txtDaDong.Text = gridPhieuGhiDanh.Rows[e.RowIndex].Cells[6].FormattedValue.ToString();
                txtConno.Text = gridPhieuGhiDanh.Rows[e.RowIndex].Cells[7].FormattedValue.ToString();


                btnXoa.Enabled = true;
            }
            else
            {
                MessageBox.Show("Ô bạn chọn không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = GetSelectedRow(txtMaHV.Text);
                if (selectedRow == -1)
                {
                    throw new Exception("Không tìm thấy mã học viên cần xóa");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa?", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        gridPhieuGhiDanh.Rows.RemoveAt(selectedRow);
                        MessageBox.Show("Xóa thông tin học viên thành công", "Thông Báo", MessageBoxButtons.OK);

                    }
                    HocVien hocvien = context.HocViens.FirstOrDefault(p => p.MaHV.ToString() == txtMaHV.Text);
                    context.HocViens.Remove(hocvien);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetSelectedRow(string MaHV)
        {
            for (int i = 0; i < gridPhieuGhiDanh.Rows.Count; i++)
            {
                if (gridPhieuGhiDanh.Rows[i].Cells[0].Value != null)
                {
                    if (gridPhieuGhiDanh.Rows[i].Cells[0].Value.ToString() == MaHV)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }


        private void ResetValue()
        {
            txtMaHV.Text = "";
            txtTenHV.Text = "";
            txtMaPhieu.Text = "";
            dateNgayGhiDanh.Text = "";
            cmbKhoaHoc.Text = "";
            txtHocPhi.Text = "";
            txtDaDong.Text = "";
            txtConno.Text = "";

        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ResetValue();
        }

        private void btnInPhieuGhiDanh_Click(object sender, EventArgs e)
        {
            formInPhieuGhiDanh fmInPhieu = new formInPhieuGhiDanh();
            fmInPhieu.Show();
        }
    }
}
