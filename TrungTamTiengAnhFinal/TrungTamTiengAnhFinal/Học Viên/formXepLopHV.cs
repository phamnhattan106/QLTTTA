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
    
    public partial class formXepLopHV : Form
    {
        List<DangKy> finddangKies;
        List<KhoaHoc> khoaHocs;
        List<DangKy> dangKyes;
        List<PhieuGhiDanh> phieuGhiDanhs;
        TrungTamAnhNguModel context = new TrungTamAnhNguModel();
        public formXepLopHV()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void formXepLopHV_Load(object sender, EventArgs e)
        {
            try
            {
                khoaHocs = context.KhoaHocs.ToList();
                dangKyes = context.DangKies.ToList();
                phieuGhiDanhs = context.PhieuGhiDanhs.ToList();
                FillPhieuGhiDanhCombobox(phieuGhiDanhs);
                FillKhoaHocCombobox(khoaHocs);
                BindGrid(dangKyes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindGrid(List<DangKy> listdangKies)
        {
            gridXepLop.Rows.Clear();
            foreach (var item in listdangKies)
            {
                int index = gridXepLop.Rows.Add();
                gridXepLop.Rows[index].Cells[0].Value = item.MaHV;
                gridXepLop.Rows[index].Cells[1].Value = item.HocVien.TenHV;
                gridXepLop.Rows[index].Cells[2].Value = item.MaPhieu;
                gridXepLop.Rows[index].Cells[3].Value = item.KhoaHoc.LopHocs.Where(p => p.MaKH == item.MaKH).FirstOrDefault().TenLop;
                gridXepLop.Rows[index].Cells[4].Value = item.KhoaHoc.TenKH;
        
            }
        }

        private void FillPhieuGhiDanhCombobox(List<PhieuGhiDanh> listphieuGhiDanhs)
        {
            this.cmbMaPhieu.DataSource = listphieuGhiDanhs;
            this.cmbMaPhieu.ValueMember = "MaPhieu";
        }
        private void FillKhoaHocCombobox(List<KhoaHoc> listkhoaHocs)
        {
            this.cmbKH.DataSource = listkhoaHocs;
            this.cmbKH.DisplayMember = "TenKH";
            this.cmbKH.ValueMember = "MaKH";
        }

        private void gridXepLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridXepLop.Rows[e.RowIndex].Cells[e.ColumnIndex] != null)
            {
                gridXepLop.CurrentRow.Selected = true;
                txtMaHV.Text = gridXepLop.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                txtHoTen.Text = gridXepLop.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                cmbMaPhieu.Text = gridXepLop.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                txtLopHoc.Text = gridXepLop.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                cmbKH.Text = gridXepLop.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
                btnXoa.Enabled = true;
            }
            else
            {
                MessageBox.Show("Ô bạn chọn không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtDatlai_Click(object sender, EventArgs e)
        {
            formXepLopHV_Load(sender, e);
        }

        private void ResetValue()
        {
            txtMaHV.Text = "";
            txtHoTen.Text = "";
            cmbMaPhieu.Text = "";
            txtLopHoc.Text = "";
            cmbKH.Text = "";
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ResetValue();
        }
        #region Thêm
        private void btnThemHV_Click(object sender, EventArgs e)
        {
            
        }
        #endregion

        private void InsertUpdate(int selectedRow)
        {
            gridXepLop.Rows[selectedRow].Cells[0].Value = txtMaHV.Text;
            gridXepLop.Rows[selectedRow].Cells[1].Value = txtHoTen.Text;
            gridXepLop.Rows[selectedRow].Cells[2].Value = cmbMaPhieu.Text;
            gridXepLop.Rows[selectedRow].Cells[3].Value = txtLopHoc.Text;
            gridXepLop.Rows[selectedRow].Cells[4].Value = cmbKH.Text;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = GetSelectedRow(txtMaHV.Text);
                if (selectedRow == -1)
                {
                    throw new Exception("Không tìm thấy thông tin lớp cần xóa");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa?", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        gridXepLop.Rows.RemoveAt(selectedRow);
                        MessageBox.Show("Xóa thông tin xếp lớp thành công", "Thông Báo", MessageBoxButtons.OK);

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
            for (int i = 0; i < gridXepLop.Rows.Count; i++)
            {
                if (gridXepLop.Rows[i].Cells[0].Value != null)
                {
                    if (gridXepLop.Rows[i].Cells[0].Value.ToString() == MaHV)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //Tolower: Chuyen chu  In thanh chu thuong
            finddangKies = new List<DangKy>();

            String hoten = txtHoTen.Text.ToLower();
            // var la kieu du lieu dong ( kieu du lieu suy luan, suy luan ra kieu du lieu dua vao bien truyen vao )
            foreach (var items in dangKyes)
            {
                if ( txtHoTen.Text == "")
                {
                    
                        finddangKies.Add(items);
                    

                }
                else
                {
                    if ( txtHoTen.Text != "")
                    {
                        if (
                        items.HocVien.TenHV.ToString().ToLower().IndexOf(hoten) != -1 )
                        {
                            finddangKies.Add(items);
                        }
                    }
                    else if ( txtHoTen.Text != "")
                    {
                        if (items.HocVien.TenHV.ToString().ToLower().IndexOf(hoten) != -1 )
                        {
                            finddangKies.Add(items);
                        }
                    }
                    else if ( txtHoTen.Text == "")
                    {
                        
                            finddangKies.Add(items);
                        
                    }


                }
            }
            txtDemHV.Text = finddangKies.Count.ToString();
            BindGrid(finddangKies);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
           
        }
    }
}
