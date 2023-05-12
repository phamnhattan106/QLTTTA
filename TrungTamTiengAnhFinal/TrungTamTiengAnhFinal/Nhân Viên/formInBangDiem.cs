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

namespace TrungTamTiengAnhFinal.Quản_trị.Quản_lý_điểm
{
    public partial class formInBangDiem : Form
    {
        public formInBangDiem()
        {
            InitializeComponent();
        }

        private void formInBangDiem_Load(object sender, EventArgs e)
        {
            TrungTamAnhNguModel context = new TrungTamAnhNguModel();
            List<BangDiem> listBangDiem = context.BangDiems.ToList();
            List<InBangDiemReport> listReport = new List<InBangDiemReport>();

            foreach (BangDiem i in listBangDiem)
            {
                InBangDiemReport temp = new InBangDiemReport();
                temp.MaHV = i.MaHV;
                temp.MaPhieu = i.MaPhieu;
                temp.MaLop = i.MaLop;
                temp.DiemViet = (int)i.DiemViet;
                temp.DiemDoc = (int)i.DiemDoc;
                temp.DiemNoi = (int)i.DiemNoi;
                temp.DiemNghe = (int)i.DiemNghe;

                listReport.Add(temp);

            }
            reportViewer1.LocalReport.ReportPath = "reportInBangDiem.rdlc";
            var source = new ReportDataSource("InBangDiemDataSet", listReport);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.RefreshReport();
            
        }
    }
}
