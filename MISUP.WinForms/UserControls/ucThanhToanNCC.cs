using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MISUP.WinForms
{
    public partial class ucThanhToanNCC : UserControl
    {
        public ucThanhToanNCC()
        {
            InitializeComponent();
            dgvData.AllowUserToResizeColumns = false;
            dgvData.AllowUserToResizeRows = false;
            dgvData.AllowUserToOrderColumns = false;
            txtTimKiem.Enter += (s, e) => { if (txtTimKiem.Text == "Tìm theo mã phiếu chi...") { txtTimKiem.Text = ""; txtTimKiem.ForeColor = Color.Black; } };
            txtTimKiem.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txtTimKiem.Text)) { txtTimKiem.Text = "Tìm theo mã phiếu chi..."; txtTimKiem.ForeColor = Color.Gray; } };
            dgvData.CellPainting += DgvData_CellPainting;
            LoadMockData();
        }

        private void LoadMockData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaPhieu"); dt.Columns.Add("ThoiGian"); dt.Columns.Add("TenNCC"); dt.Columns.Add("SoTien"); dt.Columns.Add("PhuongThuc"); dt.Columns.Add("TrangThai");

            dt.Rows.Add("PC0001", "20/06/2026 15:00", "Công ty CP Sữa Việt Nam", "125,500,000", "Chuyển khoản", "Đã thanh toán");
            dt.Rows.Add("PC0002", "19/06/2026 10:30", "Tập đoàn Masan Consumer", "20,000,000", "Tiền mặt", "Đã thanh toán");
            dt.Rows.Add("PC0003", "18/06/2026 14:15", "Samsung Electronics", "150,000,000", "Chuyển khoản", "Kỳ hạn nợ");
            dt.Rows.Add("PC0004", "15/06/2026 09:00", "Nhà Phân Phối Đồ Gia Dụng HN", "15,000,000", "Chuyển khoản", "Đã thanh toán");

            dgvData.DataSource = dt;
            dgvData.Columns["MaPhieu"].HeaderText = "Mã Phiếu"; dgvData.Columns["ThoiGian"].HeaderText = "Ngày Thanh Toán"; dgvData.Columns["TenNCC"].HeaderText = "Nhà Cung Cấp"; dgvData.Columns["SoTien"].HeaderText = "Số Tiền Chi"; dgvData.Columns["PhuongThuc"].HeaderText = "Phương Thức"; dgvData.Columns["TrangThai"].HeaderText = "Trạng Thái";
            dgvData.Columns["TenNCC"].FillWeight = 180;
        }

        private void DgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvData.Columns["TrangThai"].Index && e.Value != null)
            {
                e.PaintBackground(e.CellBounds, true);
                string text = e.Value.ToString();
                Color bgColor = Color.White, textColor = Color.Black, borderColor = Color.Gray;

                if (text == "Đã thanh toán") { bgColor = Color.FromArgb(237, 247, 237); textColor = Color.FromArgb(46, 125, 50); borderColor = Color.FromArgb(200, 230, 201); }
                else if (text == "Kỳ hạn nợ") { bgColor = Color.FromArgb(255, 244, 229); textColor = Color.FromArgb(255, 152, 0); borderColor = Color.FromArgb(255, 224, 178); }

                Graphics g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle badgeRect = new Rectangle(e.CellBounds.Left + 10, e.CellBounds.Top + 10, (int)g.MeasureString(text, e.CellStyle.Font).Width + 20, 24);

                using (GraphicsPath path = new GraphicsPath())
                {
                    int r = 12, d = r * 2; path.AddArc(badgeRect.X, badgeRect.Y, d, d, 180, 90); path.AddArc(badgeRect.Right - d, badgeRect.Y, d, d, 270, 90); path.AddArc(badgeRect.Right - d, badgeRect.Bottom - d, d, d, 0, 90); path.AddArc(badgeRect.X, badgeRect.Bottom - d, d, d, 90, 90); path.CloseFigure();
                    g.FillPath(new SolidBrush(bgColor), path); g.DrawPath(new Pen(borderColor), path);
                }
                TextRenderer.DrawText(g, text, new Font("Segoe UI", 9, FontStyle.Bold), badgeRect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                e.Handled = true;
            }
        }
    }
}