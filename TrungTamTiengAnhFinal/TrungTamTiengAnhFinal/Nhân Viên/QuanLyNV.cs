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
using TrungTamTiengAnhFinal.Nhân_Viên;

namespace TrungTamTiengAnhFinal.Quản_trị
{
    public partial class QuanLyNV : Form
    {
        List<NhanVien> nhanViens;
        List<LoaiNV> LoaiNVs;
        List<NhanVien> findNhanViens;
        TrungTamAnhNguModel context = new TrungTamAnhNguModel();
        public QuanLyNV()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();

        }


        #region Truyền dữ liệu từ DB sang GridView
        private void QuanLyNV_Load(object sender, EventArgs e)
        {
            try
            {

                nhanViens = context.NhanViens.ToList();
                LoaiNVs = context.LoaiNVs.ToList();
                FillLoaiHVCombobox(LoaiNVs);
                BindGrid(nhanViens);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindGrid(List<NhanVien> listnhanViens)
        {
            gridNV.Rows.Clear();
            foreach (var item in listnhanViens)
            {
                int index = gridNV.Rows.Add();
                gridNV.Rows[index].Cells[0].Value = item.MaNV;
                gridNV.Rows[index].Cells[1].Value = item.TenNV;
                gridNV.Rows[index].Cells[2].Value = item.SDTNV;
                gridNV.Rows[index].Cells[3].Value = item.EmailNV;
                gridNV.Rows[index].Cells[4].Value = item.LoaiNV.TenLoaiNV;

            }
        }

        private void FillLoaiHVCombobox(List<LoaiNV> loaiNVs)
        {
            this.cmbLoaiNV.DataSource = loaiNVs;
            this.cmbLoaiNV.DisplayMember = "TenLoaiNV";
            this.cmbLoaiNV.ValueMember = "MaLoaiNV";
        }
        #endregion

        #region Trả về control 
        private void gridNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridNV.Rows[e.RowIndex].Cells[e.ColumnIndex] != null)
            {
                gridNV.CurrentRow.Selected = true;
                txtMaNV.Text = gridNV.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                txtHoTen.Text = gridNV.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                txtSDT.Text = gridNV.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                txtEmail.Text = gridNV.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                cmbLoaiNV.Text = gridNV.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();


                btnXoa.Enabled = true;
            }
            else
            {
                MessageBox.Show("Ô bạn chọn không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Refesh
        private void ResetValue()
        {
            txtMaNV.Text = "";
            txtHoTen.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            cmbLoaiNV.Text = "";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ResetValue();
        }
        #endregion

        #region Thêm nhân viên 
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaNV.Text == "" || txtHoTen.Text == "" || txtSDT.Text == "" || txtEmail.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin nhân viên cần thêm");
                NhanVien nhanvien = context.NhanViens.FirstOrDefault(p => p.MaNV.ToString() == txtMaNV.Text);
                if (nhanvien == null)
                {
                    NhanVien nv = new NhanVien();
                    nv.MaNV = txtMaNV.Text;
                    nv.TenNV = txtHoTen.Text;
                    nv.SDTNV = txtSDT.Text;
                    nv.EmailNV = txtEmail.Text;
                    nv.MaLoaiNV = cmbLoaiNV.SelectedValue.ToString();
                    context.NhanViens.Add(nv);
                    context.SaveChanges();
                }
                int selectedRow = GetSelectedRow(txtMaNV.Text);
                if (selectedRow == -1)
                {
                    selectedRow = gridNV.Rows.Add();
                    InsertUpdate(selectedRow);
                    MessageBox.Show("Thêm thông tin nhân viên mới thành công", "Thông báo", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show("Nhân viên đã tồn tại!!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InsertUpdate(int selectedRow)
        {
            gridNV.Rows[selectedRow].Cells[0].Value = txtMaNV.Text;
            gridNV.Rows[selectedRow].Cells[1].Value = txtHoTen.Text;
            gridNV.Rows[selectedRow].Cells[2].Value = txtSDT.Text;
            gridNV.Rows[selectedRow].Cells[3].Value = txtEmail.Text;
            gridNV.Rows[selectedRow].Cells[4].Value = cmbLoaiNV.Text;
        }

        private int GetSelectedRow(string MaNV)
        {
            for (int i = 0; i < gridNV.Rows.Count; i++)
            {
                if (gridNV.Rows[i].Cells[0].Value != null)
                {
                    if (gridNV.Rows[i].Cells[0].Value.ToString() == MaNV)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        #endregion

        #region Sửa thông tin nhân viên
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaNV.Text == "" || txtHoTen.Text == "" || txtSDT.Text == "" || txtEmail.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin nhân viên cần thêm");
                NhanVien nhanvien = context.NhanViens.FirstOrDefault(p => p.MaNV.ToString() == txtMaNV.Text);
                if (nhanvien == null)
                {
                    NhanVien nv = new NhanVien();
                    nv.MaNV = txtMaNV.Text;
                    nv.TenNV = txtHoTen.Text;
                    nv.SDTNV = txtSDT.Text;
                    nv.EmailNV = txtEmail.Text;
                    nv.MaLoaiNV = cmbLoaiNV.Text;
                    context.SaveChanges();

                }
                int selectedRow = GetSelectedRow(txtMaNV.Text);
                InsertUpdate(selectedRow);
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Xóa thông tin nhân viên
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = GetSelectedRow(txtMaNV.Text);
                if (selectedRow == -1)
                {
                    throw new Exception("Không tìm thấy mã nhân viên cần xóa");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa?", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        gridNV.Rows.RemoveAt(selectedRow);
                        MessageBox.Show("Xóa thông tin nhân viên thành công", "Thông Báo", MessageBoxButtons.OK);

                    }
                    NhanVien nhanvien = context.NhanViens.FirstOrDefault(p => p.MaNV.ToString() == txtMaNV.Text);
                    context.NhanViens.Remove(nhanvien);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Tìm kiếm nhân viên
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //Tolower: Chuyen chu  In thanh chu thuong
            findNhanViens = new List<NhanVien>();

            String hoten = txtHoTen.Text.ToLower();
            // var la kieu du lieu dong ( kieu du lieu suy luan, suy luan ra kieu du lieu dua vao bien truyen vao )
            foreach (var items in nhanViens)
            {
                if (txtMaNV.Text == "" && txtHoTen.Text == "")
                {
                    if (items.LoaiNV.TenLoaiNV == cmbLoaiNV.Text)
                    {
                        findNhanViens.Add(items);
                    }

                }
                else
                {
                    if (txtMaNV.Text != "" && txtHoTen.Text != "")
                    {
                        if (items.MaNV.ToString() == txtMaNV.Text &&
                        items.TenNV.ToString().ToLower().IndexOf(hoten) != -1 &&
                        items.LoaiNV.TenLoaiNV == cmbLoaiNV.Text)
                        {
                            findNhanViens.Add(items);
                        }
                    }
                    else if (txtMaNV.Text == "" && txtHoTen.Text != "")
                    {
                        if (items.TenNV.ToString().ToLower().IndexOf(hoten) != -1 &&
                            items.LoaiNV.TenLoaiNV == cmbLoaiNV.Text)
                        {
                            findNhanViens.Add(items);
                        }
                    }
                    else if (txtMaNV.Text != "" && txtHoTen.Text == "")
                    {
                        if (items.MaNV.ToString() == txtMaNV.Text && items.LoaiNV.TenLoaiNV == cmbLoaiNV.Text)
                        {
                            findNhanViens.Add(items);
                        }
                    }


                }
            }
            txtKetQua.Text = findNhanViens.Count.ToString();
            BindGrid(findNhanViens);
        }

        private void btnDatLai_Click(object sender, EventArgs e)
        {
            QuanLyNV_Load(sender, e);
        }
    }
#endregion

}






