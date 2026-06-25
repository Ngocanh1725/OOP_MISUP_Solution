using MISUP.BLL.Services;
using MISUP.Models;
using MISUP.WinForms.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // Thư viện Chart

namespace MISUP.WinForms
{
    public partial class ucBaoCao : UserControl
    {
        private HangHoaBLL db = new HangHoaBLL();
        private List<HangHoa> _currentList = new List<HangHoa>();

        public ucBaoCao()
        {
            InitializeComponent();
            SetRoundedRegion(pnlCard, 15);

            btnXem.Click += (s, e) => LoadReport();
            cmbLoaiBaoCao.SelectedIndexChanged += (s, e) => LoadReport();
            btnXuatExcel.Click += BtnXuatExcel_Click;

            LoadReport();
        }

        private void SetRoundedRegion(Control control, int radius)
        {
            control.Resize += (s, e) =>
            {
                GraphicsPath path = new GraphicsPath();
                path.AddArc(0, 0, radius, radius, 180, 90); path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90); path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
                path.CloseFigure(); control.Region = new Region(path);
            };
        }

        private void LoadReport()
        {
            lblThongKe1.Text = db.DemTongSoMatHang().ToString("N0") + " SKU";
            lblThongKe2.Text = db.TinhTongGiaTriKho().ToString("N0") + " đ";
            lblThongKe3.Text = db.TinhTongTonKhoThucTe().ToString("N0") + " cái";

            string metric = "SoLuong";

            if (cmbLoaiBaoCao.SelectedIndex == 0) // Tồn kho hiện tại
            {
                _currentList = db.LayDanhSach().OrderByDescending(x => x.TinhTongGiaTriSauThue()).ToList();
                dgvData.DataSource = _currentList.Select(h => new {
                    h.MaHang,
                    h.TenHang,
                    NhaCungCap = h.NhaSanXuat,
                    Kho = h.SoLuongNhap,
                    GiaTri = h.TinhTongGiaTriSauThue().ToString("N0") + " đ"
                }).ToList();
                metric = "GiaTri";
            }
            else if (cmbLoaiBaoCao.SelectedIndex == 1) // Hàng sắp hết (< 10)
            {
                _currentList = db.LayHangSapHetTonKho();
                dgvData.DataSource = _currentList.Select(h => new {
                    h.MaHang,
                    h.TenHang,
                    Kho = h.SoLuongNhap,
                    MucCanhBao = h.TonKhoToiThieu,
                    TinhTrang = "Cần nhập gấp"
                }).ToList();
            }
            else // Hàng Cận Date
            {
                _currentList = db.LayHangCanDate(30);
                dgvData.DataSource = _currentList.Select(h => new {
                    h.MaHang,
                    h.TenHang,
                    Kho = h.SoLuongNhap,
                    HSD = h.HanSuDung.HasValue ? h.HanSuDung.Value.ToString("dd/MM/yyyy") : "-",
                    TinhTrang = "Cận Date"
                }).ToList();
            }

            FormatGrid();
            DrawChart(_currentList, metric); // Vẽ Biểu đồ
        }

        private void FormatGrid()
        {
            if (dgvData.Columns.Count > 0)
            {
                dgvData.Columns["MaHang"].HeaderText = "Mã Hàng";
                dgvData.Columns["TenHang"].HeaderText = "Tên Sản Phẩm";
                if (dgvData.Columns.Contains("NhaCungCap")) dgvData.Columns["NhaCungCap"].HeaderText = "Nhà Cung Cấp";
                if (dgvData.Columns.Contains("Kho")) dgvData.Columns["Kho"].HeaderText = "Tồn Kho";
                if (dgvData.Columns.Contains("GiaTri")) dgvData.Columns["GiaTri"].HeaderText = "Giá Trị";
                if (dgvData.Columns.Contains("MucCanhBao")) dgvData.Columns["MucCanhBao"].HeaderText = "Mức Cảnh Báo";
                if (dgvData.Columns.Contains("TinhTrang")) dgvData.Columns["TinhTrang"].HeaderText = "Tình Trạng";
                if (dgvData.Columns.Contains("HSD")) dgvData.Columns["HSD"].HeaderText = "Hạn Sử Dụng";

                dgvData.Columns["TenHang"].FillWeight = 200;
            }
        }

        // ==========================================
        // NHÚNG BIỂU ĐỒ TRỰC TIẾP VÀO GIAO DIỆN
        // ==========================================
        private void DrawChart(List<HangHoa> data, string metric)
        {
            chartThongKe.Series.Clear();

            Series series = new Series();
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = false; // Tắt label để trông gọn giống Image 2

            // Lấy 15 sản phẩm đầu tiên để biểu đồ không bị ép chặt
            var topData = data.Take(15).ToList();

            foreach (var item in topData)
            {
                string name = item.TenHang.Length > 10 ? item.TenHang.Substring(0, 10) + ".." : item.TenHang;

                int pointIndex;
                if (metric == "GiaTri")
                {
                    series.Name = "Giá Trị (VNĐ)";
                    pointIndex = series.Points.AddXY(name, item.TinhTongGiaTriSauThue());
                }
                else
                {
                    series.Name = "Số Lượng (SP)";
                    pointIndex = series.Points.AddXY(name, item.SoLuongNhap);
                }

                // Custom Color Palette: Cột màu Light Blue, nhưng nếu là cột cao nhất thì tô màu Vàng (Giống Image 2)
                DataPoint pt = series.Points[pointIndex];
                pt.Color = Color.FromArgb(178, 235, 242); // Màu xanh dương nhạt #B2EBF2
            }

            if (series.Points.Count > 0)
            {
                // Tìm cột có giá trị cao nhất và đổi thành màu Vàng rực rỡ
                var maxPoint = series.Points.OrderByDescending(p => p.YValues[0]).First();
                maxPoint.Color = Color.FromArgb(244, 208, 63); // Màu vàng Gold #F4D03F
            }

            chartThongKe.Series.Add(series);
        }

        private void BtnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count == 0) { MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            ExportHelper.ExportToCSV(db.LayDanhSach());
        }
    }
}