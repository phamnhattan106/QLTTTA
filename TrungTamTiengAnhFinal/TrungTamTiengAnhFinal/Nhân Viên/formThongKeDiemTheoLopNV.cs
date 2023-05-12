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

namespace TrungTamTiengAnhFinal
{
    public partial class formThongKeDiemTheoLopNV : Form
    {
        TrungTamAnhNguModel context = new TrungTamAnhNguModel();
        List<BangDiem> bangDiems;
        String classID;
        List<BangDiem> listPointByClass;
        public formThongKeDiemTheoLopNV()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnInDiem_Click(object sender, EventArgs e)
        {
            new formInBangDiem().Show();
        }

        private void formThongKeDiemTheoLopNV_Load(object sender, EventArgs e)
        {
            bangDiems = context.BangDiems.ToList();
            List<LopHoc>listLopHoc = context.LopHocs.ToList();
            FillBangDiemToDataGridView(bangDiems);
            FillLopHocToDataGridView(listLopHoc);
        }
        
        private void FillLopHocToDataGridView(List<LopHoc> lopHocs)
        {
            gridLop.Rows.Clear();
            foreach (var item in lopHocs)
            {
                int i = gridLop.Rows.Add();
                gridLop.Rows[i].Cells[0].Value = item.MaLop;
                gridLop.Rows[i].Cells[1].Value = item.TenLop;
            }
        }

        private void FillBangDiemToDataGridView(List<BangDiem> bangDiems)
        {
            gridThongKe.Rows.Clear();
            foreach( var item in bangDiems)
            {
                int index = gridThongKe.Rows.Add();
                gridThongKe.Rows[index].Cells[0].Value = item.MaHV;
                gridThongKe.Rows[index].Cells[1].Value = item.HocVien.TenHV;
                gridThongKe.Rows[index].Cells[2].Value = item.DiemNghe;
                gridThongKe.Rows[index].Cells[3].Value = item.DiemNoi;
                gridThongKe.Rows[index].Cells[4].Value = item.DiemDoc;
                gridThongKe.Rows[index].Cells[5].Value = item.DiemViet;
                gridThongKe.Rows[index].Cells[6].Value = ((item.DiemNghe + item.DiemNoi + item.DiemDoc + item.DiemViet) / 4).ToString();
            }
        }

        private void gridLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            classID = gridLop.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            listPointByClass = context.BangDiems.Where(p => p.MaLop == classID).ToList();
            FillBangDiemToDataGridView(listPointByClass);
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            List<BangDiem> listPoint = listPointByClass.Where(p => p.HocVien.TenHV == txtTenHV.Text).ToList();
            FillBangDiemToDataGridView(listPoint);
            txtTenHV.Text = "";
        }

        private void btnDatLai_Click(object sender, EventArgs e)
        {
            formThongKeDiemTheoLopNV_Load(sender, e);
        }
    }
}
