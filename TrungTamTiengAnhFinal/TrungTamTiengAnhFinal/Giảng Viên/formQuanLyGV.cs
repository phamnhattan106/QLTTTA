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
using TrungTamTiengAnhFinal.KetNoiDB;

namespace TrungTamTiengAnhFinal.Quản_trị
{
    public partial class formQuanLyGV : Form
    {
        List<GiangVien> giangViens;
        List<LoaiGV> LoaiGVs;
        List<GiangVien> findGiangViens;
        TrungTamAnhNguModel context = new TrungTamAnhNguModel();
        public formQuanLyGV()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        #region Thêm giảng viên 
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaGiangVien.Text == "" || txtHoTen.Text == "" || txtSDT.Text == "" || txtEmail.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin giảng viên cần thêm");
                GiangVien giangvien = context.GiangViens.FirstOrDefault(p => p.MaGV.ToString() == txtMaGiangVien.Text);
                if (giangvien == null)
                {
                    GiangVien gv = new GiangVien();
                    gv.MaGV = txtMaGiangVien.Text;
                    gv.TenGV = txtHoTen.Text;
                    gv.GioiTinhGV = cboGioiTinh.Text;
                    gv.SDTGV = txtSDT.Text;
                    gv.EmailGV = txtEmail.Text;
                    gv.MaLoaiGV = cmbLoaiGV.SelectedValue.ToString();
                    context.GiangViens.Add(gv);
                    context.SaveChanges();
                }
                int selectedRow = GetSelectedRow(txtMaGiangVien.Text);
                if (selectedRow == -1)
                {
                    selectedRow = gridGV.Rows.Add();
                    InsertUpdate(selectedRow);
                    MessageBox.Show("Thêm thông tin giảng viên mới thành công", "Thông báo", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show("Giảng viên đã tồn tại!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InsertUpdate(int selectedRow)
        {
            gridGV.Rows[selectedRow].Cells[0].Value = txtMaGiangVien.Text;
            gridGV.Rows[selectedRow].Cells[1].Value = txtHoTen.Text;
            gridGV.Rows[selectedRow].Cells[2].Value = cboGioiTinh.Text;
            gridGV.Rows[selectedRow].Cells[3].Value = txtSDT.Text;
            gridGV.Rows[selectedRow].Cells[4].Value = txtEmail.Text;
            gridGV.Rows[selectedRow].Cells[5].Value = cmbLoaiGV.Text;

        }

        private int GetSelectedRow(String MaGV)
        {
            for (int i = 0; i < gridGV.Rows.Count; i++)
            {
                if (gridGV.Rows[i].Cells[0].Value != null)
                {
                    if (gridGV.Rows[i].Cells[0].Value.ToString() == MaGV)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        #endregion

        #region Truyền Thông Tin Giảng Viên Từ DB vào GridView
        private void formQuanLyGV_Load(object sender, EventArgs e)
        {
            try
            {

                giangViens = context.GiangViens.ToList();
                LoaiGVs = context.LoaiGVs.ToList();
                FillLoaiGVCombobox(LoaiGVs);
                BindGrid(giangViens);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindGrid(List<GiangVien> listgiangViens)
        {
            gridGV.Rows.Clear();
            foreach (var item in listgiangViens)
            {
                int index = gridGV.Rows.Add();
                gridGV.Rows[index].Cells[0].Value = item.MaGV;
                gridGV.Rows[index].Cells[1].Value = item.TenGV;
                gridGV.Rows[index].Cells[2].Value = item.GioiTinhGV;
                gridGV.Rows[index].Cells[3].Value = item.SDTGV;
                gridGV.Rows[index].Cells[4].Value = item.EmailGV;
                gridGV.Rows[index].Cells[5].Value = item.LoaiGV.TenLoaiGV;


            }
        }

        private void FillLoaiGVCombobox(List<LoaiGV> listloaiGVs)
        {
            this.cmbLoaiGV.DataSource = listloaiGVs;
            this.cmbLoaiGV.DisplayMember = "TenLoaiGV";
            this.cmbLoaiGV.ValueMember = "MaLoaiGV";
        }
        #endregion

        #region Hàm Refresh
        private void ResetValue()
        {
            txtMaGiangVien.Text = "";
            txtHoTen.Text = "";
            cboGioiTinh.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ResetValue();
        }
        #endregion

        #region Trả về Control
        private void gridGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {    
            if (gridGV.Rows[e.RowIndex].Cells[e.ColumnIndex] != null)
            {
                gridGV.CurrentRow.Selected = true;
                txtMaGiangVien.Text = gridGV.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                txtHoTen.Text = gridGV.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                cboGioiTinh.Text = gridGV.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                txtSDT.Text = gridGV.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                txtEmail.Text = gridGV.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
                cmbLoaiGV.Text = gridGV.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();

                btnXoa.Enabled = true;
            }
            else
            {
                MessageBox.Show("Ô bạn chọn không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        #endregion

        #region Xóa thông tin giảng viên
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = GetSelectedRow(txtMaGiangVien.Text);
                if (selectedRow == -1)
                {
                    throw new Exception("Không tìm thấy mã  giảng viên cần xóa");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa?", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        gridGV.Rows.RemoveAt(selectedRow);
                        MessageBox.Show("Xóa thông tin giảng viên thành công", "Thông Báo", MessageBoxButtons.OK);

                    }
                    GiangVien giangvien = context.GiangViens.FirstOrDefault(p => p.MaGV.ToString() == txtMaGiangVien.Text);
                    context.GiangViens.Remove(giangvien);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Sửa thông tin giảng viên
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaGiangVien.Text == "" || txtHoTen.Text == "" || txtSDT.Text == "" || txtEmail.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin giảng viên cần sửa");
                GiangVien giangvien = context.GiangViens.FirstOrDefault(p => p.MaGV.ToString() == txtMaGiangVien.Text);
                if (giangvien == null)
                {
                    GiangVien gv = new GiangVien();
                    gv.MaGV = txtMaGiangVien.Text;
                    gv.TenGV = txtHoTen.Text;
                    gv.GioiTinhGV = cboGioiTinh.Text;
                    gv.SDTGV = txtSDT.Text;
                    gv.EmailGV = txtEmail.Text;
                    gv.MaLoaiGV = cmbLoaiGV.SelectedValue.ToString();
                    context.SaveChanges();

                }
                int selectedRow = GetSelectedRow(txtMaGiangVien.Text);
                InsertUpdate(selectedRow);
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Tìm kiếm giảng viên 
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //Tolower: Chuyen chu  In thanh chu thuong
            findGiangViens = new List<GiangVien>();

            String hoten = txtHoTen.Text.ToLower();
            // var la kieu du lieu dong ( kieu du lieu suy luan, suy luan ra kieu du lieu dua vao bien truyen vao )
            foreach (var items in giangViens)
            {
                if (txtMaGiangVien.Text == "" && txtHoTen.Text == "")
                {
                    if (items.LoaiGV.TenLoaiGV == cmbLoaiGV.Text)
                    {
                        findGiangViens.Add(items);
                    }

                }
                else
                {
                    if (txtMaGiangVien.Text != "" && txtHoTen.Text != "")
                    {
                        if (items.MaGV.ToString() == txtMaGiangVien.Text &&
                        items.TenGV.ToString().ToLower().IndexOf(hoten) != -1 &&
                        items.LoaiGV.TenLoaiGV == cmbLoaiGV.Text)
                        {
                            findGiangViens.Add(items);
                        }
                    }
                    else if (txtMaGiangVien.Text == "" && txtHoTen.Text != "")
                    {
                        if (items.TenGV.ToString().ToLower().IndexOf(hoten) != -1 &&
                            items.LoaiGV.TenLoaiGV == cmbLoaiGV.Text)
                        {
                            findGiangViens.Add(items);
                        }
                    }
                    else if (txtMaGiangVien.Text != "" && txtHoTen.Text == "")
                    {
                        if (items.MaGV.ToString() == txtMaGiangVien.Text && items.LoaiGV.TenLoaiGV == cmbLoaiGV.Text)
                        {
                            findGiangViens.Add(items);
                        }
                    }


                }
            }
            txtKetQua.Text = findGiangViens.Count.ToString();
            BindGrid(findGiangViens);
        }
        #endregion

        private void btnDatLai_Click(object sender, EventArgs e)
        {
            formQuanLyGV_Load(sender, e);
        }
    }
}

