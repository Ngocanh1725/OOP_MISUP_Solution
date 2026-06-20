using MISUP.BLL.Services;
using MISUP.Models;
using MISUP.WinForms.Utils;
using MISUP.WinForms.Forms; // Dùng để gọi Form ChartDialog
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MISUP.WinForms
{
    public partial class ucBaoCao : UserControl
    {
        private HangHoaBLL db = new HangHoaBLL();

        // Lưu trữ danh sách đang được lọc để truyền sang biểu đồ
        private List<HangHoa> _currentList = new List<HangHoa>();
        private string _currentMetric = "GiaTri"; // Hoặc "SoLuong"
        private string _currentTitle = "";

        public ucBaoCao()
        {
            InitializeComponent();
            SetRoundedRegion(pnlCard, 15);

            btnXem.Click += (s, e) => LoadReport();
            cmbLoaiBaoCao.SelectedIndexChanged += (s, e) => LoadReport();
            btnXuatExcel.Click += BtnXuatExcel_Click;

            // Gắn sự kiện bật biểu đồ
            btnXemBieuDo.Click += BtnXemBieuDo_Click;

            LoadReport(); // Mặc định chạy lần đầu
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

                _currentTitle = "Top 10 Sản phẩm có Giá trị Kho cao nhất";
                _currentMetric = "GiaTri";
            }
            else if (cmbLoaiBaoCao.SelectedIndex == 1) // Hàng sắp hết (< 10) hoăc theo TonKhoToiThieu
            {
                _currentList = db.LayHangSapHetTonKho();
                dgvData.DataSource = _currentList.Select(h => new {
                    h.MaHang,
                    h.TenHang,
                    Kho = h.SoLuongNhap,
                    MucCanhBao = h.TonKhoToiThieu,
                    TinhTrang = "Cần nhập gấp"
                }).ToList();

                _currentTitle = "Biểu đồ Các Sản phẩm Sắp hết hàng";
                _currentMetric = "SoLuong";
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

                _currentTitle = "Số lượng Tồn của hàng Cận Date";
                _currentMetric = "SoLuong";
            }

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
            }
        }

        private void BtnXemBieuDo_Click(object sender, EventArgs e)
        {
            if (_currentList == null || _currentList.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu nào để vẽ biểu đồ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Gọi cửa sổ Popup chứa Biểu đồ lên
            using (var dialog = new ChartDialog(_currentList, _currentTitle, _currentMetric))
            {
                dialog.ShowDialog();
            }
        }

        private void BtnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count == 0) { MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            ExportHelper.ExportToCSV(db.LayDanhSach());
        }
    }
}