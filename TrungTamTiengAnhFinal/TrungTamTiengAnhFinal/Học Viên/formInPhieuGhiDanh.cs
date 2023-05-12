using Microsoft.Reporting.WinForms;
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
    public partial class formInPhieuGhiDanh : Form
    {
        public formInPhieuGhiDanh()
        {
            InitializeComponent();
        }

        private void formInPhieuGhiDanh_Load(object sender, EventArgs e)
        {
            TrungTamAnhNguModel context = new TrungTamAnhNguModel();
            List<DangKy> listdangKies = context.DangKies.ToList();
            List<inPhieuGhiDanh> listReport = new List<inPhieuGhiDanh>();

            foreach (DangKy sv in listdangKies)
            {
                inPhieuGhiDanh temp = new inPhieuGhiDanh();
                temp.MaHV = sv.MaHV;
                temp.TenHV = sv.HocVien.TenHV;
                temp.MaPhieu = sv.MaPhieu;
                temp.NgayGhiDanh = sv.PhieuGhiDanh.NgayGhiDanh;
                temp.TenKH = sv.KhoaHoc.TenKH;
                temp.HocPhi = sv.KhoaHoc.HocPhi;
                temp.DaDong = sv.PhieuGhiDanh.DaDong;
                temp.ConNo = sv.KhoaHoc.HocPhi - sv.PhieuGhiDanh.DaDong;


                listReport.Add(temp);

            }

            reportViewer1.LocalReport.ReportPath = "rptPhieuGhIDanhReport.rdlc";
            var source = new ReportDataSource("DataSet", listReport);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.RefreshReport();
        }
    }
}
