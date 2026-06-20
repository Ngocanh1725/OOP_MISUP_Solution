using MISUP.BLL.Services;
using MISUP.Models;
using MISUP.WinForms.Utils;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MISUP.WinForms
{
    public partial class ucBaoCao : UserControl
    {
        private HangHoaBLL db = new HangHoaBLL();
        private Label lblThongKe1, lblThongKe2, lblThongKe3;

        public ucBaoCao()
        {
            InitializeComponent();

            // Ép chặt giao diện lưới
            dgvData.AllowUserToResizeColumns = false;
            dgvData.AllowUserToResizeRows = false;
            dgvData.AllowUserToOrderColumns = false;

            SetupDashboardCards();

            // Gắn sự kiện cho các nút và bộ lọc
            btnXem.Click += (s, e) => LoadReport();
            cmbLoaiBaoCao.SelectedIndexChanged += (s, e) => LoadReport(); // Tự động load khi đổi loại báo cáo
            dtpTuNgay.ValueChanged += (s, e) => LoadReport();
            dtpDenNgay.ValueChanged += (s, e) => LoadReport();

            // Xử lý sự kiện xuất file CSV thực tế
            btnXuatExcel.Click += BtnXuatExcel_Click;

            // Load mặc định
            LoadReport();
        }

        private void SetupDashboardCards()
        {
            Panel card1 = CreateCard("📊 TỔNG MÃ HÀNG", Color.FromArgb(142, 68, 173), out lblThongKe1);
            Panel card2 = CreateCard("💰 TỔNG TÀI SẢN KHO", Color.FromArgb(39, 174, 96), out lblThongKe2);
            Panel card3 = CreateCard("📦 TỔNG SL VẬT LÝ", Color.FromArgb(230, 126, 34), out lblThongKe3);

            card1.Left = 20; card2.Left = 280; card3.Left = 540;
            pnlDashboard.Controls.AddRange(new Control[] { card1, card2, card3 });
        }

        private Panel CreateCard(string title, Color bgColor, out Label lblValue)
        {
            Panel p = new Panel() { Width = 240, Height = 80, BackColor = bgColor, Top = 10 };
            Label lblTitle = new Label() { Text = title, ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold), AutoSize = true, Left = 10, Top = 10 };
            lblValue = new Label() { Text = "0", ForeColor = Color.White, Font = new Font("Segoe UI", 18, FontStyle.Bold), AutoSize = true, Left = 10, Top = 35 };
            p.Controls.Add(lblTitle); p.Controls.Add(lblValue);
            return p;
        }

        private void LoadReport()
        {
            // Cập nhật thẻ Thống kê bằng Data thực tế từ BLL
            lblThongKe1.Text = db.DemTongSoMatHang().ToString("N0") + " SKU";
            lblThongKe2.Text = db.TinhTongGiaTriKho().ToString("N0") + " đ";
            lblThongKe3.Text = db.TinhTongTonKhoThucTe().ToString("N0") + " cái";

            // Lọc dữ liệu theo ComboBox
            if (cmbLoaiBaoCao.SelectedIndex == 0) // Tồn kho hiện tại
            {
                var list = db.LayDanhSach().OrderByDescending(x => x.TinhTongGiaTriSauThue()).ToList();
                dgvData.DataSource = list.Select(h => new {
                    h.MaHang,
                    h.TenHang,
                    NhaCungCap = h.NhaSanXuat,
                    Kho = h.SoLuongNhap,
                    GiaTri = h.TinhTongGiaTriSauThue().ToString("N0") + " đ"
                }).ToList();
            }
            else if (cmbLoaiBaoCao.SelectedIndex == 1) // Hàng sắp hết (< 10) hoăc theo TonKhoToiThieu
            {
                var list = db.LayHangSapHetTonKho();
                dgvData.DataSource = list.Select(h => new {
                    h.MaHang,
                    h.TenHang,
                    Kho = h.SoLuongNhap,
                    MucCanhBao = h.TonKhoToiThieu,
                    TinhTrang = "Cần nhập gấp"
                }).ToList();
            }
            else // Hàng Cận Date
            {
                var list = db.LayHangCanDate(30); // Cận date trong 30 ngày tới
                dgvData.DataSource = list.Select(h => new {
                    h.MaHang,
                    h.TenHang,
                    Kho = h.SoLuongNhap,
                    HSD = h.HanSuDung.HasValue ? h.HanSuDung.Value.ToString("dd/MM/yyyy") : "-",
                    TinhTrang = "Cận Date"
                }).ToList();
            }

            // Đổi tên header cho đẹp
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

        private void BtnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV File (*.csv)|*.csv", FileName = "BaoCao_" + DateTime.Now.ToString("ddMMyyyy_HHmm") + ".csv" })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        StringBuilder sb = new StringBuilder();

                        // Lấy danh sách tên cột
                        var headers = dgvData.Columns.Cast<DataGridViewColumn>().Select(c => c.HeaderText).ToArray();
                        sb.AppendLine(string.Join(",", headers));

                        // Lấy dữ liệu từng dòng
                        foreach (DataGridViewRow row in dgvData.Rows)
                        {
                            var cells = row.Cells.Cast<DataGridViewCell>().Select(c =>
                            {
                                string cellValue = c.Value != null ? c.Value.ToString() : "";
                                // Xóa dấu phẩy trong giá trị (nếu có) để tránh làm hỏng cấu trúc file CSV
                                return cellValue.Replace(",", "");
                            }).ToArray();

                            sb.AppendLine(string.Join(",", cells));
                        }

                        // Ghi ra file với chuẩn UTF-8 BOM để Excel đọc được tiếng Việt có dấu
                        File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                        MessageBox.Show("Xuất báo cáo thành công!\nĐã lưu tại: " + sfd.FileName, "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}