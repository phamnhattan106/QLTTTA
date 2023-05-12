using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrungTamTiengAnhFinal.Học_Viên;
using TrungTamTiengAnhFinal.KetNoiDB;

namespace TrungTamTiengAnhFinal.Quản_trị
{
    public partial class formQuanLyHV : Form
    {
        List<HocVien> hocViens; 
        List<LoaiHV> LoaiHVs;
        List<HocVien> findHocViens;
        TrungTamAnhNguModel context = new TrungTamAnhNguModel();
        public formQuanLyHV()
        {
            InitializeComponent();
        }
        #region Nút Đóng
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Thêm một học viên
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaHV.Text == "" || txtHoTen.Text == "" || txtDiaChi.Text == "" || txtEmail.Text == "" || txtSDT.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin học viên cần thêm");
                HocVien hocvien = context.HocViens.FirstOrDefault(p => p.MaHV.ToString() == txtMaHV.Text);
                if (hocvien == null)
                {
                    HocVien hv = new HocVien();
                    hv.MaHV = txtMaHV.Text;
                    hv.TenHV = txtHoTen.Text;
                    hv.NgaySinh = dateNgaySinh.Value;
                    hv.GioiTinhHV = comboGioiTinh.Text;
                    hv.DiaChi = txtDiaChi.Text; ;
                    hv.EmailHV = txtEmail.Text;
                    hv.SDTHV = txtSDT.Text;
                    hv.NgayTiepNhan = dateNgayTiepNhan.Value;
                    hv.MaLoaiHV = cboLoaiHV.SelectedValue.ToString();
                   
                  
                    context.HocViens.Add(hv);
                    context.SaveChanges();
                }
                int selectedRow = GetSelectedRow(txtMaHV.Text);
                if (selectedRow == -1)
                {
                    selectedRow = gridDSHV.Rows.Add();
                    InsertUpdate(selectedRow);
                    MessageBox.Show("Thêm thông tin học viên mới thành công", "Thông báo", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show("Mã số đã tồn tại!!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InsertUpdate(int selectedRow)
        {
            gridDSHV.Rows[selectedRow].Cells[0].Value = txtMaHV.Text;
            gridDSHV.Rows[selectedRow].Cells[1].Value = txtHoTen.Text;
            gridDSHV.Rows[selectedRow].Cells[2].Value = dateNgaySinh.Text;
            gridDSHV.Rows[selectedRow].Cells[3].Value = comboGioiTinh.Text;
            gridDSHV.Rows[selectedRow].Cells[4].Value = txtDiaChi.Text;
            gridDSHV.Rows[selectedRow].Cells[5].Value = txtEmail.Text;
            gridDSHV.Rows[selectedRow].Cells[6].Value = txtSDT.Text;
            gridDSHV.Rows[selectedRow].Cells[7].Value = dateNgayTiepNhan.Text;
            gridDSHV.Rows[selectedRow].Cells[8].Value = cboLoaiHV.Text;
        }

        private int GetSelectedRow(string MaHV)
        {
            for (int i = 0; i < gridDSHV.Rows.Count; i++)
            {
                if (gridDSHV.Rows[i].Cells[0].Value != null)
                {
                    if (gridDSHV.Rows[i].Cells[0].Value.ToString() == MaHV)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        #endregion Kết Thúc

        #region Truyền dữ liễu từ DB vào GridView
        private void formQuanLyHV_Load(object sender, EventArgs e)
        {
            try
            {

                hocViens = context.HocViens.ToList();
                LoaiHVs = context.LoaiHVs.ToList();
                FillLoaiHVCombobox(LoaiHVs);
                BindGrid(hocViens);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindGrid(List<HocVien> listhocViens)
        {
            gridDSHV.Rows.Clear();
            foreach (var item in listhocViens)
            {
                int index = gridDSHV.Rows.Add();
                gridDSHV.Rows[index].Cells[0].Value = item.MaHV;
                gridDSHV.Rows[index].Cells[1].Value = item.TenHV;
                gridDSHV.Rows[index].Cells[2].Value = item.NgaySinh;
                gridDSHV.Rows[index].Cells[3].Value = item.GioiTinhHV;
                gridDSHV.Rows[index].Cells[4].Value = item.DiaChi;
                gridDSHV.Rows[index].Cells[5].Value = item.EmailHV;
                gridDSHV.Rows[index].Cells[6].Value = item.SDTHV;
                gridDSHV.Rows[index].Cells[7].Value = item.NgayTiepNhan;
                gridDSHV.Rows[index].Cells[8].Value = item.LoaiHV.TenLoaiHV;

            }
        }

        private void FillLoaiHVCombobox(List<LoaiHV> listloaiHVs)
        {
            this.cboLoaiHV.DataSource = listloaiHVs;
            this.cboLoaiHV.DisplayMember = "TenLoaiHV";
            this.cboLoaiHV.ValueMember = "MaLoaiHV";
        }
        #endregion

        private void ResetValue()
        {
            txtMaHV.Text = "";
            txtHoTen.Text = "";
            dateNgaySinh.Text = "";
            comboGioiTinh.Text = "";
            txtDiaChi.Text = "";
            txtEmail.Text = "";
            txtSDT.Text = "";
            cboLoaiHV.Text = "";
            dateNgayTiepNhan.Text = "";
            cboLoaiHV.Text = "";
        }

        #region Trả về Control khi Click vào GridView
        private void gridDSHV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridDSHV.Rows[e.RowIndex].Cells[e.ColumnIndex] != null)
            {
                gridDSHV.CurrentRow.Selected = true;
                txtMaHV.Text = gridDSHV.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                txtHoTen.Text = gridDSHV.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                dateNgaySinh.Text = gridDSHV.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                comboGioiTinh.Text = gridDSHV.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                txtDiaChi.Text = gridDSHV.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
                txtEmail.Text = gridDSHV.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
                txtSDT.Text = gridDSHV.Rows[e.RowIndex].Cells[6].FormattedValue.ToString();
               dateNgayTiepNhan.Text = gridDSHV.Rows[e.RowIndex].Cells[7].FormattedValue.ToString();
                cboLoaiHV.Text = gridDSHV.Rows[e.RowIndex].Cells[8].FormattedValue.ToString();



                btnXoa.Enabled = true;
            }
            else
            {
                MessageBox.Show("Ô bạn chọn không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Sửa thông tin học viên
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaHV.Text == "" || txtHoTen.Text == "" || txtDiaChi.Text == "" || txtEmail.Text == "" || txtSDT.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin học viên cần sửa");
                HocVien hocvien = context.HocViens.FirstOrDefault(p => p.MaHV.ToString() == txtMaHV.Text);
                if (hocvien == null)
                {
                    HocVien hv = new HocVien();
                    hv.MaHV = txtMaHV.Text;
                    hv.TenHV = txtHoTen.Text;
                    hv.NgaySinh = dateNgaySinh.Value;
                    hv.GioiTinhHV = comboGioiTinh.Text;
                    hv.DiaChi = txtDiaChi.Text; ;
                    hv.EmailHV = txtEmail.Text;
                    hv.SDTHV = txtSDT.Text;
                    hv.NgayTiepNhan = dateNgayTiepNhan.Value;
                    hv.MaLoaiHV = cboLoaiHV.SelectedValue.ToString();


                    context.SaveChanges();

                }
                int selectedRow = GetSelectedRow(txtMaHV.Text);
                InsertUpdate(selectedRow);
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Xóa thông tin học viên
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
                        gridDSHV.Rows.RemoveAt(selectedRow);
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
        #endregion

        #region Tìm kiếm học viên
        private void btnTimKiemHocVien_Click(object sender, EventArgs e)
        {
            //Tolower: Chuyen chu  In thanh chu thuong
            findHocViens = new List<HocVien>();

            String hoten = txtHoTen.Text.ToLower();
            // var la kieu du lieu dong ( kieu du lieu suy luan, suy luan ra kieu du lieu dua vao bien truyen vao )
            foreach (var items in hocViens )
            {
                if (txtMaHV.Text == "" && txtHoTen.Text == "")
                {
                    if (items.LoaiHV.TenLoaiHV == cboLoaiHV.Text)
                    {
                        findHocViens.Add(items);
                    }

                }
                else
                {
                    if (txtMaHV.Text != "" && txtHoTen.Text != "")
                    {
                        if (items.MaHV.ToString() == txtMaHV.Text &&
                        items.TenHV.ToString().ToLower().IndexOf(hoten) != -1 &&
                        items.LoaiHV.TenLoaiHV == cboLoaiHV.Text)
                        {
                            findHocViens.Add(items);
                        }
                    }
                    else if (txtMaHV.Text == "" && txtHoTen.Text != "")
                    {
                        if (items.TenHV.ToString().ToLower().IndexOf(hoten) != -1 &&
                            items.LoaiHV.TenLoaiHV == cboLoaiHV.Text)
                        {
                            findHocViens.Add(items);
                        }
                    }
                    else if (txtMaHV.Text != "" && txtHoTen.Text == "")
                    {
                        if (items.MaHV.ToString() == txtMaHV.Text && items.LoaiHV.TenLoaiHV == cboLoaiHV.Text)
                        {
                            findHocViens.Add(items);
                        }
                    }


                }
            }
            txtKetQua.Text = findHocViens.Count.ToString();
            BindGrid(findHocViens);
        }
        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ResetValue();
        }

        private void btnDatLai_Click(object sender, EventArgs e)
        {
            formQuanLyHV_Load(sender, e);

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
 
}



