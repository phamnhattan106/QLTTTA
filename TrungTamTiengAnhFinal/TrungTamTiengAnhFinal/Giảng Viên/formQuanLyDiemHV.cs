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
using TrungTamTiengAnhFinal.Quản_trị.Quản_lý_điểm;

namespace TrungTamTiengAnhFinal.Học_Viên
{
    public partial class QuanLyDiemHV : Form
    {
        List<BangDiem> findbangDiems;
        List<LopHoc> lopHocs;
        List<HocVien> hocViens;
        List<BangDiem> bangDiems;
        TrungTamAnhNguModel context = new TrungTamAnhNguModel();


        public QuanLyDiemHV()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void QuanLyDiemHV_Load(object sender, EventArgs e)
        {
            lopHocs = context.LopHocs.ToList();
            hocViens = context.HocViens.ToList();
            bangDiems = context.BangDiems.ToList();
            BindGirdBangDiem(bangDiems);
        }
        #region Truyền dữ liệu vào DB
        private void BindGirdBangDiem(List<BangDiem> listbangDiems)
        {
            gridBangDiem.Rows.Clear();
            foreach (var item in listbangDiems)
            {
                int index = gridBangDiem.Rows.Add();
                gridBangDiem.Rows[index].Cells[0].Value = item.MaHV;
                gridBangDiem.Rows[index].Cells[1].Value = item.HocVien.TenHV;
                gridBangDiem.Rows[index].Cells[2].Value = item.LopHoc.TenLop;
                gridBangDiem.Rows[index].Cells[3].Value = item.DiemNghe;
                gridBangDiem.Rows[index].Cells[4].Value = item.DiemNoi;
                gridBangDiem.Rows[index].Cells[5].Value = item.DiemDoc;
                gridBangDiem.Rows[index].Cells[6].Value = item.DiemViet;

            }
        }


        #endregion

        #region Trả về label
        private void gridBangDiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridBangDiem.Rows[e.RowIndex].Cells[e.ColumnIndex] != null)
            {
                gridBangDiem.CurrentRow.Selected = true;
                lblMaHV.Text = gridBangDiem.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                lblTenHV.Text = gridBangDiem.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                lblTenLop.Text = gridBangDiem.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                lblDiemNghe.Text = gridBangDiem.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                lblDiemNoi.Text = gridBangDiem.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
                lblDiemDoc.Text = gridBangDiem.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
                lblDiemViet.Text = gridBangDiem.Rows[e.RowIndex].Cells[6].FormattedValue.ToString();
                btnXoa.Enabled = true;
            }
            else
            {
                MessageBox.Show("Ô bạn chọn không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Tìm kiếm theo tên học viên
        private void btnTimKiem_Click(object sender, EventArgs e)
        { //Tolower: Chuyen chu  In thanh chu thuong
            findbangDiems = new List<BangDiem>();

            String hocvien = txtTenHV.Text.ToLower();
            // var la kieu du lieu dong ( kieu du lieu suy luan, suy luan ra kieu du lieu dua vao bien truyen vao )
            foreach (var items in bangDiems)
            {
                if (txtTenHV.Text == "")
                {
                    findbangDiems.Add(items);
                }
                else
                {
                    if (txtTenHV.Text != "")
                    {
                        if (items.HocVien.TenHV.ToString().ToLower().IndexOf(hocvien) != -1)
                        {
                            findbangDiems.Add(items);
                        }
                    }
                    else if (txtTenHV.Text == "")
                    {

                        findbangDiems.Add(items);
                    }
                }
            }
            BindGirdBangDiem(findbangDiems);

        }

        

        #endregion

        #region Xóa thông tin
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = GetSelectedRow(lblMaHV.Text);
                if (selectedRow == -1)
                {
                    throw new Exception("Không tìm thấy mã học viên cần xóa");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa?", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        gridBangDiem.Rows.RemoveAt(selectedRow);
                        MessageBox.Show("Xóa thông tin bảng điểm của học viên thành công", "Thông Báo", MessageBoxButtons.OK);

                    }
                    HocVien hocvien = context.HocViens.FirstOrDefault(p => p.MaHV.ToString() == lblMaHV.Text);
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
            for (int i = 0; i < gridBangDiem.Rows.Count; i++)
            {
                if (gridBangDiem.Rows[i].Cells[0].Value != null)
                {
                    if (gridBangDiem.Rows[i].Cells[0].Value.ToString() == MaHV)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        #endregion

        #region Đặt lại
        private void btnDatLai_Click(object sender, EventArgs e)
        {
            QuanLyDiemHV_Load(sender, e);
        }
        #endregion

        #region Refresh
        private void ResetValue()
        {
            lblMaHV.Text = "";
            lblTenHV.Text = "";
            lblTenLop.Text = "";
            lblDiemNghe.Text = "";
            lblDiemNoi.Text = "";
            lblDiemDoc.Text = "";
            lblDiemViet.Text = "";
        }
        

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ResetValue();
        }
        #endregion
    }
}
