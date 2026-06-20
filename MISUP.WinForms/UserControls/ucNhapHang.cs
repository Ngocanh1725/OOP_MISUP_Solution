using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MISUP.WinForms
{
    public partial class ucNhapHang : UserControl
    {
        public ucNhapHang()
        {
            InitializeComponent();
            dgvData.AllowUserToResizeColumns = false;
            dgvData.AllowUserToResizeRows = false;
            dgvData.AllowUserToOrderColumns = false;
            AttachEvents();
            LoadMockData();
        }

        private void AttachEvents()
        {
            txtTimKiem.Enter += (s, e) => { if (txtTimKiem.Text == "Tìm theo mã phiếu...") { txtTimKiem.Text = ""; txtTimKiem.ForeColor = Color.Black; } };
            txtTimKiem.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txtTimKiem.Text)) { txtTimKiem.Text = "Tìm theo mã phiếu..."; txtTimKiem.ForeColor = Color.Gray; } };
            dgvData.CellPainting += DgvData_CellPainting;
        }

        private void LoadMockData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaPhieu"); dt.Columns.Add("ThoiGian"); dt.Columns.Add("NhaCungCap"); dt.Columns.Add("ChiNhanh"); dt.Columns.Add("TongTien"); dt.Columns.Add("TrangThai");

            dt.Rows.Add("PON0001", "20/06/2026 14:30", "Công ty CP Vinamilk", "Kho Tổng HN", "125,500,000", "Đã nhập kho");
            dt.Rows.Add("PON0002", "19/06/2026 09:15", "Samsung Electronics", "Kho Miền Nam", "450,000,000", "Đã nhập kho");
            dt.Rows.Add("PON0003", "21/06/2026 10:00", "Tập đoàn Sunhouse", "Kho Tổng HN", "85,200,000", "Đang vận chuyển");
            dt.Rows.Add("PON0004", "18/06/2026 16:45", "Nhà PP Hàng Tiêu Dùng", "Kho Miền Trung", "12,000,000", "Đã hủy");
            dt.Rows.Add("PON0005", "15/06/2026 11:20", "Công ty Thời Trang Yody", "Kho Tổng HN", "54,600,000", "Đã nhập kho");

            dgvData.DataSource = dt;
            dgvData.Columns["MaPhieu"].HeaderText = "Mã Phiếu"; dgvData.Columns["ThoiGian"].HeaderText = "Thời Gian"; dgvData.Columns["NhaCungCap"].HeaderText = "Nhà Cung Cấp"; dgvData.Columns["ChiNhanh"].HeaderText = "Chi Nhánh Nhận"; dgvData.Columns["TongTien"].HeaderText = "Tổng Tiền Nhập"; dgvData.Columns["TrangThai"].HeaderText = "Trạng Thái";
            dgvData.Columns["NhaCungCap"].FillWeight = 200;
        }

        private void DgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvData.Columns["TrangThai"].Index && e.Value != null)
            {
                e.PaintBackground(e.CellBounds, true);
                string text = e.Value.ToString();
                Color bgColor = Color.White, textColor = Color.Black, borderColor = Color.Gray;

                if (text == "Đã nhập kho") { bgColor = Color.FromArgb(237, 247, 237); textColor = Color.FromArgb(46, 125, 50); borderColor = Color.FromArgb(200, 230, 201); }
                else if (text == "Đang vận chuyển") { bgColor = Color.FromArgb(232, 244, 253); textColor = Color.FromArgb(2, 136, 209); borderColor = Color.FromArgb(179, 229, 252); }
                else if (text == "Đã hủy") { bgColor = Color.FromArgb(255, 235, 238); textColor = Color.FromArgb(211, 47, 47); borderColor = Color.FromArgb(255, 205, 210); }

                Graphics g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle badgeRect = new Rectangle(e.CellBounds.Left + 10, e.CellBounds.Top + 10, (int)g.MeasureString(text, e.CellStyle.Font).Width + 20, 24);

                using (GraphicsPath path = new GraphicsPath())
                {
                    int r = 12, d = r * 2;
                    path.AddArc(badgeRect.X, badgeRect.Y, d, d, 180, 90); path.AddArc(badgeRect.Right - d, badgeRect.Y, d, d, 270, 90);
                    path.AddArc(badgeRect.Right - d, badgeRect.Bottom - d, d, d, 0, 90); path.AddArc(badgeRect.X, badgeRect.Bottom - d, d, d, 90, 90); path.CloseFigure();
                    g.FillPath(new SolidBrush(bgColor), path); g.DrawPath(new Pen(borderColor), path);
                }
                TextRenderer.DrawText(g, text, new Font("Segoe UI", 9, FontStyle.Bold), badgeRect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                e.Handled = true;
            }
        }
    }
}