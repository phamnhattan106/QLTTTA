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

namespace TrungTamTiengAnhFinal.Quản_trị
{
    public partial class formQuanLyLopHoc : Form
    {
        List<LopHoc> LopHocs;
        List<KhoaHoc> khoaHocs;
        List<LopHoc> findLopHocs;
        TrungTamAnhNguModel context = new TrungTamAnhNguModel();
        public formQuanLyLopHoc()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        #region Truyền dữ liệu từ DB sang GridView
        private void formQuanLyLopHoc_Load(object sender, EventArgs e)
        {
            try
            {

                LopHocs = context.LopHocs.ToList();
                khoaHocs = context.KhoaHocs.ToList();
                FillKhoaHocCombobox(khoaHocs);
                BindGrid(LopHocs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillKhoaHocCombobox(List<KhoaHoc> listkhoaHocs)
        {
            this.cmbKhoaHoc.DataSource = listkhoaHocs;
            this.cmbKhoaHoc.DisplayMember = "TenKH";
            this.cmbKhoaHoc.ValueMember = "MaKH";
        }

        private void BindGrid(List<LopHoc> listlopHocs)
        {
            gridLop.Rows.Clear();
            foreach (var item in listlopHocs)
            {
                int index = gridLop.Rows.Add();
                gridLop.Rows[index].Cells[0].Value = item.MaLop;
                gridLop.Rows[index].Cells[1].Value = item.TenLop;
                gridLop.Rows[index].Cells[2].Value = item.NgayBD;
                gridLop.Rows[index].Cells[3].Value = item.NgayKT;
                if (item.DangMo == true)
                {
                    gridLop.Rows[index].Cells[4].Value = "Đang Mở";
                }
                else
                {
                    gridLop.Rows[index].Cells[4].Value = "Chưa Mở";
                }
                gridLop.Rows[index].Cells[5].Value = item.SiSo;
            }
        }
        #endregion

        #region Trả về control 
        private void gridLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridLop.Rows[e.RowIndex].Cells[e.ColumnIndex] != null)
            {
                gridLop.CurrentRow.Selected = true;
                lblMaLop.Text = txtMaLop.Text = gridLop.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                lblTenLop.Text = txtTenLop.Text = gridLop.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                lblNgayBatDau.Text = dateNgayBD.Text = gridLop.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                lblNgayKetThuc.Text = dateNgayKT.Text = gridLop.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();

                if (gridLop.Rows[e.RowIndex].Cells[4].FormattedValue.ToString() == "Đang Mở")
                    ckbTinhTrang.Checked = true;
                else
                    ckbTinhTrang.Checked = false;

                lblSiSo.Text = txtSiSo.Text = gridLop.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
                btnXoa.Enabled = true;
            }
            else
            {
                MessageBox.Show("Ô bạn chọn không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Refresh 
        private void ResetValue()
        {
            txtMaLop.Text = "";
            txtTenLop.Text = "";
            dateNgayBD.Text = "";
            dateNgayKT.Text = "";
            cmbKhoaHoc.Text = "";
            txtSiSo.Text = "";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ResetValue();
        }

        #endregion

        #region Thêm lớp học_ đang lỗi
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaLop.Text == "" || txtTenLop.Text == "" || txtSiSo.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin lớp học cần thêm");
                LopHoc lophoc = context.LopHocs.FirstOrDefault(p => p.MaLop.ToString() == txtMaLop.Text);
                if (lophoc == null)
                {
                    LopHoc lh = new LopHoc();
                    lh.MaLop = txtMaLop.Text;
                    lh.TenLop = txtTenLop.Text;
                    lh.NgayBD = dateNgayBD.Value;
                    lh.NgayKT = dateNgayKT.Value;
                    lh.DangMo = ckbTinhTrang.Checked;
                    lh.SiSo = Convert.ToInt32 (txtSiSo.Text);
                    context.LopHocs.Add(lh);
                    context.SaveChanges();
                }
                int selectedRow = GetSelectedRow(txtMaLop.Text);
                if (selectedRow == -1)
                {
                    selectedRow = gridLop.Rows.Add();
                    InsertUpdate(selectedRow);
                    MessageBox.Show("Thêm thông tin lớp học mới thành công", "Thông báo", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show("Lớp học đã tồn tại!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InsertUpdate(int selectedRow)
        {
            gridLop.Rows[selectedRow].Cells[0].Value = txtMaLop.Text;
            gridLop.Rows[selectedRow].Cells[1].Value = txtTenLop.Text;
            gridLop.Rows[selectedRow].Cells[2].Value = dateNgayBD.Text;
            gridLop.Rows[selectedRow].Cells[3].Value = dateNgayKT.Text;
            if (ckbTinhTrang.Checked)
                gridLop.Rows[selectedRow].Cells[4].Value = "Đang Mở";
            else
                gridLop.Rows[selectedRow].Cells[4].Value = "Chưa Mở";
            gridLop.Rows[selectedRow].Cells[5].Value = txtSiSo.Text;
        }

        private int GetSelectedRow(string MaLop)
        {
            for (int i = 0; i < gridLop.Rows.Count; i++)
            {
                if (gridLop.Rows[i].Cells[0].Value != null)
                {
                    if (gridLop.Rows[i].Cells[0].Value.ToString() == MaLop)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        #endregion

        #region Sửa thông tin lớp học_Đang lỗi
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaLop.Text == "" || txtTenLop.Text == "" || txtSiSo.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin lớp học cần sửa");
                LopHoc lophoc = context.LopHocs.FirstOrDefault(p => p.MaLop.ToString() == txtMaLop.Text);
                if (lophoc == null)
                {
                    LopHoc lh = new LopHoc();
                    lh.MaLop = txtMaLop.Text;
                    lh.TenLop = txtTenLop.Text;
                    lh.NgayBD = dateNgayBD.Value;
                    lh.NgayKT = dateNgayKT.Value;
                    lh.DangMo = ckbTinhTrang.Checked;
                    lh.SiSo = Convert.ToInt32(txtSiSo.Text); 
                    context.SaveChanges();

                }
                int selectedRow = GetSelectedRow(txtMaLop.Text);
                InsertUpdate(selectedRow);
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    

        #endregion

       
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = GetSelectedRow(txtMaLop.Text);
                if (selectedRow == -1)
                {
                    throw new Exception("Không tìm thấy mã lớp cần xóa");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa?", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        gridLop.Rows.RemoveAt(selectedRow);
                        MessageBox.Show("Xóa thông tin lớp thành công", "Thông Báo", MessageBoxButtons.OK);

                    }
                    LopHoc lophoc = context.LopHocs.FirstOrDefault(p => p.MaLop.ToString() == txtMaLop.Text);
                    context.LopHocs.Remove(lophoc);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //Tolower: Chuyen chu  In thanh chu thuong
            findLopHocs = new List<LopHoc>();

            String malop = txtMaLop.Text.ToLower();
            // var la kieu du lieu dong ( kieu du lieu suy luan, suy luan ra kieu du lieu dua vao bien truyen vao )
            foreach (var items in LopHocs)
            {
                if (txtMaLop.Text == "" && txtTenLop.Text == "")
                {
                    
                        findLopHocs.Add(items);
                    

                }
                else
                {
                    if (txtMaLop.Text != "" && txtTenLop.Text != "")
                    {
                        if (items.MaLop.ToString() == txtMaLop.Text &&
                        items.TenLop.ToString().ToLower().IndexOf(malop) != -1)
                        {
                            findLopHocs.Add(items);
                        }
                    }
                    else if (txtMaLop.Text == "" && txtTenLop.Text != "")
                    {
                        if (items.TenLop.ToString().ToLower().IndexOf(malop) != -1 )
                        {
                            findLopHocs.Add(items);
                        }
                    }
                    else if (txtMaLop.Text != "" && txtTenLop.Text == "")
                    {
                        if (items.MaLop.ToString() == txtMaLop.Text)
                        {
                            findLopHocs.Add(items);
                        }
                    }


                }
            }
            txtKetQua.Text = findLopHocs.Count.ToString();
            BindGrid(findLopHocs);
        }

        private void btnDatlai_Click(object sender, EventArgs e)
        {
            formQuanLyLopHoc_Load(sender, e);
        }
    }
}
