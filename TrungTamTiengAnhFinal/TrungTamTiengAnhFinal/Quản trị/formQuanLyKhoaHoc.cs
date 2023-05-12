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
    public partial class formQuanLyKhoaHoc : Form
    {
        List<KhoaHoc> khoaHocs;
        List<PhieuGhiDanh> phieuGhiDanhs;
        List<KhoaHoc> findKhoaHocs;
        TrungTamAnhNguModel context = new TrungTamAnhNguModel();
        public formQuanLyKhoaHoc()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        #region Truyền dữ liệu từ DB vào GridView
        private void formQuanLyKhoaHoc_Load(object sender, EventArgs e)
        {
            try
            {

                khoaHocs = context.KhoaHocs.ToList();
                phieuGhiDanhs = context.PhieuGhiDanhs.ToList();
                FillPhieuGhiDanhCombobox(phieuGhiDanhs);
                BindGrid(khoaHocs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindGrid(List<KhoaHoc> listkhoaHocs)
        {
            gridKH.Rows.Clear();
            foreach (var item in listkhoaHocs)
            {
                int index = gridKH.Rows.Add();
                gridKH.Rows[index].Cells[0].Value = item.MaKH;
                gridKH.Rows[index].Cells[1].Value = item.TenKH;
                gridKH.Rows[index].Cells[2].Value = item.HocPhi;
                gridKH.Rows[index].Cells[3].Value = item.MaPhieu;
                
            }
        }

        private void FillPhieuGhiDanhCombobox(List<PhieuGhiDanh> listphieuGhiDanhs)
        {
            this.cmbMaPhieuGhiDanh.DataSource = listphieuGhiDanhs;
            this.cmbMaPhieuGhiDanh.DisplayMember = "MaPhieu";
            this.cmbMaPhieuGhiDanh.ValueMember = "MaPhieu";
        }

        #endregion

        #region Thêm thông tin khóa học
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaKH.Text == "" || txtTenKH.Text == "" || txtHocPhi.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin khóa học cần thêm");
                KhoaHoc khoahoc = context.KhoaHocs.FirstOrDefault(p => p.MaKH.ToString() == txtMaKH.Text);
                if (khoahoc == null)
                {
                    KhoaHoc kh = new KhoaHoc();
                    kh.MaKH = txtMaKH.Text;
                    kh.TenKH = txtTenKH.Text;
                    kh.HocPhi = Int32.Parse (txtHocPhi.Text);
                    context.KhoaHocs.Add(kh);
                    context.SaveChanges();
                }
                int selectedRow = GetSelectedRow(txtMaKH.Text);
                if (selectedRow == -1)
                {
                    selectedRow = gridKH.Rows.Add();
                    InsertUpdate(selectedRow);
                    MessageBox.Show("Thêm thông tin khóa học mới thành công", "Thông báo", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show("Khóa học đã tồn tại !!", "Thông bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InsertUpdate(int selectedRow)
        {
            gridKH.Rows[selectedRow].Cells[0].Value = txtMaKH.Text;
            gridKH.Rows[selectedRow].Cells[1].Value = txtTenKH.Text;
            gridKH.Rows[selectedRow].Cells[2].Value = txtHocPhi.Text;
            gridKH.Rows[selectedRow].Cells[3].Value = cmbMaPhieuGhiDanh.Text;
        }

        private int GetSelectedRow(string MaHV)
        {
            for (int i = 0; i < gridKH.Rows.Count; i++)
            {
                if (gridKH.Rows[i].Cells[0].Value != null)
                {
                    if (gridKH.Rows[i].Cells[0].Value.ToString() == MaHV)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        #endregion

        #region Trả về Control
        private void gridKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridKH.Rows[e.RowIndex].Cells[e.ColumnIndex] != null)
            {
                gridKH.CurrentRow.Selected = true;
                txtMaKH.Text = gridKH.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                txtTenKH.Text = gridKH.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                txtHocPhi.Text = gridKH.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                cmbMaPhieuGhiDanh.Text = gridKH.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                btnXoa.Enabled = true;
            }
            else
            {
                MessageBox.Show("Ô bạn chọn không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Sửa thông tin khóa học 
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaKH.Text == "" || txtTenKH.Text == "" || txtHocPhi.Text == "" || txtHocPhi.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin học khóa học cần sửa");
                KhoaHoc khoahoc = context.KhoaHocs.FirstOrDefault(p => p.MaKH.ToString() == txtMaKH.Text);
                if (khoahoc == null)
                {
                    KhoaHoc kh = new KhoaHoc();
                    kh.MaKH = txtMaKH.Text;
                    kh.TenKH = txtTenKH.Text;
                    kh.HocPhi = Int32.Parse(txtHocPhi.Text);
                    context.SaveChanges();

                }
                int selectedRow = GetSelectedRow(txtMaKH.Text);
                InsertUpdate(selectedRow);
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Xóa thông tin khóa học
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = GetSelectedRow(txtMaKH.Text);
                if (selectedRow == -1)
                {
                    throw new Exception("Không tìm thấy mã khóa học cần xóa");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa?", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        gridKH.Rows.RemoveAt(selectedRow);
                        MessageBox.Show("Xóa thông tin khóa học thành công", "Thông Báo", MessageBoxButtons.OK);

                    }
                    KhoaHoc khoahoc = context.KhoaHocs.FirstOrDefault(p => p.MaKH.ToString() == txtMaKH.Text);
                    context.KhoaHocs.Remove(khoahoc);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Refresh 
        private void ResetValue()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtHocPhi.Text = "";
            cmbMaPhieuGhiDanh.Text = "";
        }
            private void btnRefresh_Click(object sender, EventArgs e)
        {
            ResetValue();
        }
        #endregion

        #region Tìm kiếm thông tin khóa học
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //Tolower: Chuyen chu  In thanh chu thuong
            findKhoaHocs = new List<KhoaHoc>();

            String tenkh = txtTenKH.Text.ToLower();
            // var la kieu du lieu dong ( kieu du lieu suy luan, suy luan ra kieu du lieu dua vao bien truyen vao )
            foreach (var items in khoaHocs)
            {
                if (txtMaKH.Text == "" )
                {
                    if (items.MaKH == cmbMaPhieuGhiDanh.Text)
                    {
                        findKhoaHocs.Add(items);
                    }

                }
                else
                {
                    if (txtMaKH.Text != "" )
                    {
                        if (items.MaKH.ToString() == txtMaKH.Text)
                        {
                            findKhoaHocs.Add(items);
                        }
                    }
                    
                    else if (txtMaKH.Text != "" )
                    {
                        if (items.MaKH.ToString() == txtMaKH.Text )
                        {
                           findKhoaHocs.Add(items);
                        }
                    }


                }
            }
            txtKetQua.Text = findKhoaHocs.Count.ToString();
            BindGrid(findKhoaHocs);
        }
        #endregion

        private void btnLuuThongTin_Click(object sender, EventArgs e)
        {
            formQuanLyKhoaHoc_Load(sender, e);
        }
    }

}
