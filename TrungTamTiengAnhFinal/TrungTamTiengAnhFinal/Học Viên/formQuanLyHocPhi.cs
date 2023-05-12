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
    public partial class formQuanLyHocPhi : Form
    {
        List<HocVien> hocViens;
        List<DangKy> finddangKies;
        List<KhoaHoc> khoaHocs;
        List<PhieuGhiDanh> phieuGhiDanhs;
        List<DangKy> dangKies;
        TrungTamAnhNguModel context = new TrungTamAnhNguModel();
        public formQuanLyHocPhi()
        {
            InitializeComponent();
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void formQuanLyHocPhi_Load(object sender, EventArgs e)
        {
            hocViens = context.HocViens.ToList();
            dangKies = context.DangKies.ToList();
            khoaHocs = context.KhoaHocs.ToList();
            phieuGhiDanhs = context.PhieuGhiDanhs.ToList();
            BindGirdHocPhi(dangKies);
        }

        private void BindGirdHocPhi(List<DangKy> listdangKies)
        {
            gridHocPhi.Rows.Clear();
            foreach (var item in listdangKies)
            {
                int index = gridHocPhi.Rows.Add();
                gridHocPhi.Rows[index].Cells[0].Value = item.HocVien.MaHV;
                gridHocPhi.Rows[index].Cells[1].Value = item.HocVien.TenHV;
                gridHocPhi.Rows[index].Cells[2].Value = item.KhoaHoc.TenKH;
                gridHocPhi.Rows[index].Cells[3].Value = item.MaPhieu;
                gridHocPhi.Rows[index].Cells[4].Value = item.KhoaHoc.HocPhi;
                gridHocPhi.Rows[index].Cells[5].Value = item.PhieuGhiDanh.DaDong;
                gridHocPhi.Rows[index].Cells[6].Value = item.KhoaHoc.HocPhi - item.PhieuGhiDanh.DaDong;

            }
        }

        private void btnDatLai_Click(object sender, EventArgs e)
        {
            formQuanLyHocPhi_Load(sender, e);
        }

        private void gridHocPhi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridHocPhi.Rows[e.RowIndex].Cells[e.ColumnIndex] != null)
            {
                gridHocPhi.CurrentRow.Selected = true;
                lblMaHV.Text = gridHocPhi.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                lblTenHV.Text = gridHocPhi.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                lblTenKH.Text = gridHocPhi.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                lblMaPhieu.Text = gridHocPhi.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                lblHocPhi.Text = gridHocPhi.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
                lblDaDong.Text = gridHocPhi.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
                lblConNo.Text = gridHocPhi.Rows[e.RowIndex].Cells[6].FormattedValue.ToString();
            }
            else
            {
                MessageBox.Show("Ô bạn chọn không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //Tolower: Chuyen chu  In thanh chu thuong
            finddangKies = new List<DangKy>();

            String hoten = txtTenHV.Text.ToLower();
            // var la kieu du lieu dong ( kieu du lieu suy luan, suy luan ra kieu du lieu dua vao bien truyen vao )
            foreach (var items in dangKies)
            {
                if (txtTenHV.Text == "")
                {
                    
                        finddangKies.Add(items);
                    

                }
                else
                {
                    if ( txtTenHV.Text != "")
                    {
                        if (
                        items.HocVien.TenHV.ToString().ToLower().IndexOf(hoten) != -1)
                        {
                            finddangKies.Add(items);
                        }
                    }
                    else if (txtTenHV.Text != "")
                    {
                        if (items.HocVien.TenHV.ToString().ToLower().IndexOf(hoten) != -1)
                        {
                            finddangKies.Add(items);
                        }
                    }
                    else if (txtTenHV.Text == "")
                    {
                        if (items.MaHV.ToString() == lblMaHV.Text)
                        {
                            finddangKies.Add(items);
                        }
                    }


                }
            }

            BindGirdHocPhi(finddangKies);
        }

        private void Xóa_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = GetSelectedRow(lblMaHV.Text);
                if (selectedRow == -1)
                {
                    throw new Exception("Không tìm thấy thông tin học phí cần xóa");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa?", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        gridHocPhi.Rows.RemoveAt(selectedRow);
                        MessageBox.Show("Xóa thông tin học phí của học viên thành công", "Thông Báo", MessageBoxButtons.OK);

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
            for (int i = 0; i < gridHocPhi.Rows.Count; i++)
            {
                if (gridHocPhi.Rows[i].Cells[0].Value != null)
                {
                    if (gridHocPhi.Rows[i].Cells[0].Value.ToString() == MaHV)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
    }
}
