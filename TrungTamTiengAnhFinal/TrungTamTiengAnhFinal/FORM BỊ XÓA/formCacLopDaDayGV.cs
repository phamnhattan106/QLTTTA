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

namespace TrungTamTiengAnhFinal.Giảng_Viên
{
    public partial class formCacLopDaDayGV : Form

    {
      //  List<GiangVien> findgiangViens;
        List<GiangVien> giangViens;
        List<KhoaHoc> khoaHocs;
        List<LopHoc> lopHocs;
        TrungTamAnhNguModel context = new TrungTamAnhNguModel();
        public formCacLopDaDayGV()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void formCacLopDaDayGV_Load(object sender, EventArgs e)
        {
            lopHocs = context.LopHocs.ToList();
            khoaHocs = context.KhoaHocs.ToList();
            giangViens = context.GiangViens.ToList();
            BindGrid(lopHocs);
            FillCacKhoaHocCombobox(khoaHocs);
        }
        private void FillCacKhoaHocCombobox(List<KhoaHoc> listkhoaHocs)
        {
            this.cmbKH.DataSource = listkhoaHocs;
            this.cmbKH.DisplayMember = "TenKH";
            this.cmbKH.ValueMember = "MaKH";
        }

        private void BindGrid(List<LopHoc> listlopHocs )
        {
            gridXepLop.Rows.Clear();
            foreach (var item in listlopHocs)
            {
                int index = gridXepLop.Rows.Add();
                gridXepLop.Rows[index].Cells[0].Value = item.MaGV;
           //     gridXepLop.Rows[index].Cells[1].Value = item.GiangVien.TenGV;
                gridXepLop.Rows[index].Cells[2].Value = item.TenLop;
            //    gridXepLop.Rows[index].Cells[3].Value = item.KhoaHoc.TenKH;
            }
        }

        private void txtDatlai_Click(object sender, EventArgs e)
        {
            formCacLopDaDayGV_Load(sender, e);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = GetSelectedRow(txtMaGV.Text);
                if (selectedRow == -1)
                {
                    throw new Exception("Không tìm thấy thông tin giảng viên cần xóa");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa?", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        gridXepLop.Rows.RemoveAt(selectedRow);
                        MessageBox.Show("Xóa thông tin giảng viên thành công", "Thông Báo", MessageBoxButtons.OK);

                    }
                    GiangVien giangvien = context.GiangViens.FirstOrDefault(p => p.MaGV.ToString() == txtMaGV.Text);
                    context.GiangViens.Remove(giangvien);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetSelectedRow(string MaGV)
        {
            for (int i = 0; i < gridXepLop.Rows.Count; i++)
            {
                if (gridXepLop.Rows[i].Cells[0].Value != null)
                {
                    if (gridXepLop.Rows[i].Cells[0].Value.ToString() == MaGV)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //{
            //    //Tolower: Chuyen chu  In thanh chu thuong
            //    findgiangViens = new List<GiangVien>();

            //    String hoten = txtHoTen.Text.ToLower();
            //    // var la kieu du lieu dong ( kieu du lieu suy luan, suy luan ra kieu du lieu dua vao bien truyen vao )
            //    foreach (var items in giangViens)
            //    {
            //        if (txtHoTen.Text == "")
            //        {

            //            findgiangViens.Add(items);


            //        }
            //        else
            //        {
            //            if (txtHoTen.Text != "")
            //            {
            //                if (
            //                items.TenGV.ToString().ToLower().IndexOf(hoten) != -1)
            //                {
            //                    findgiangViens.Add(items);
            //                }
            //            }
            //            else if (txtHoTen.Text != "")
            //            {
            //                if (items.TenGV.ToString().ToLower().IndexOf(hoten) != -1)
            //                {
            //                    findgiangViens.Add(items);
            //                }
            //            }
            //            else if (txtHoTen.Text == "")
            //            {

            //                findgiangViens.Add(items);

            //            }


            //        }
            //    }
            //    txtDemGV.Text = findgiangViens.Count.ToString();
            //    BindGrid(findgiangViens);
            //}
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
