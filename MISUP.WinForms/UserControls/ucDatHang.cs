using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MISUP.WinForms
{
    public partial class ucDatHang : UserControl
    {
        public ucDatHang()
        {
            InitializeComponent();
            AttachEvents();
            LoadMockData();
            
        }

        private void AttachEvents()
        {
            // Placeholder clear for search box
            txtTimKiem.Enter += (s, e) => { if (txtTimKiem.Text.Contains("Tìm kiếm")) { txtTimKiem.Text = ""; txtTimKiem.ForeColor = Color.Black; } };
            txtTimKiem.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txtTimKiem.Text)) { txtTimKiem.Text = "🔍 Tìm kiếm mã đơn, tên NCC..."; txtTimKiem.ForeColor = Color.Gray; } };

            // Draw border for the card panel
            pnlCard.Paint += (s, e) => { ControlPaint.DrawBorder(e.Graphics, pnlCard.ClientRectangle, Color.FromArgb(226, 232, 240), ButtonBorderStyle.Solid); };

            // Cell painting for status badges
            dgvDonHang.CellPainting += DgvDonHang_CellPainting;
        }

        private void LoadMockData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaDon", typeof(string));
            dt.Columns.Add("NgayTao", typeof(string));
            dt.Columns.Add("NhaCungCap", typeof(string));
            dt.Columns.Add("TongTien", typeof(string));
            dt.Columns.Add("TrangThai", typeof(string));
            dt.Columns.Add("NguoiTao", typeof(string));

            dt.Rows.Add("PON001", "20/06/2026", "Công ty CP Acecook", "15,500,000", "Đang giao dịch", "Admin");
            dt.Rows.Add("PON002", "19/06/2026", "Samsung Việt Nam", "125,000,000", "Hoàn thành", "Nhân viên Kho");
            dt.Rows.Add("PON003", "18/06/2026", "Nhà phân phối Unilever", "8,200,000", "Phiếu tạm", "Admin");
            dt.Rows.Add("PON004", "15/06/2026", "Đại lý phân phối TH True Milk", "4,350,000", "Đã hủy", "Nhân viên Kho");
            dt.Rows.Add("PON005", "10/06/2026", "Công ty TNHH Panasonic", "45,000,000", "Hoàn thành", "Admin");

            dgvDonHang.DataSource = dt;

            dgvDonHang.Columns["MaDon"].HeaderText = "Mã đơn nhập";
            dgvDonHang.Columns["NgayTao"].HeaderText = "Ngày tạo";
            dgvDonHang.Columns["NhaCungCap"].HeaderText = "Nhà cung cấp";
            dgvDonHang.Columns["TongTien"].HeaderText = "Tổng tiền";
            dgvDonHang.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvDonHang.Columns["NguoiTao"].HeaderText = "Người tạo";

            dgvDonHang.Columns["NhaCungCap"].FillWeight = 200;
        }

        private void DgvDonHang_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.Value != null)
            {
                if (dgvDonHang.Columns[e.ColumnIndex].Name == "TrangThai")
                {
                    e.PaintBackground(e.CellBounds, true);

                    string text = e.Value.ToString();
                    Color bgColor, textColor, borderColor;

                    if (text == "Hoàn thành")
                    {
                        bgColor = Color.FromArgb(237, 247, 237);
                        textColor = Color.FromArgb(46, 125, 50);
                        borderColor = Color.FromArgb(200, 230, 201);
                    }
                    else if (text == "Đang giao dịch")
                    {
                        bgColor = Color.FromArgb(232, 244, 253);
                        textColor = Color.FromArgb(25, 118, 210);
                        borderColor = Color.FromArgb(187, 222, 251);
                    }
                    else if (text == "Đã hủy")
                    {
                        bgColor = Color.FromArgb(255, 235, 238);
                        textColor = Color.FromArgb(211, 47, 47);
                        borderColor = Color.FromArgb(255, 205, 210);
                    }
                    else // Phiếu tạm
                    {
                        bgColor = Color.FromArgb(255, 244, 229);
                        textColor = Color.FromArgb(255, 152, 0);
                        borderColor = Color.FromArgb(255, 224, 178);
                    }

                    Graphics g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    SizeF textSize = g.MeasureString(text, e.CellStyle.Font);
                    int badgeWidth = (int)textSize.Width + 20;
                    int badgeHeight = 26;
                    int x = e.CellBounds.Left + 15;
                    int y = e.CellBounds.Top + (e.CellBounds.Height - badgeHeight) / 2;

                    Rectangle badgeRect = new Rectangle(x, y, badgeWidth, badgeHeight);

                    using (GraphicsPath path = GetRoundedRect(badgeRect, 13))
                    {
                        using (SolidBrush brush = new SolidBrush(bgColor)) g.FillPath(brush, path);
                        using (Pen pen = new Pen(borderColor, 1)) g.DrawPath(pen, path);
                    }

                    TextRenderer.DrawText(g, text, new Font("Segoe UI", 9, FontStyle.Bold), badgeRect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                    e.Handled = true;
                }
            }
        }

        private GraphicsPath GetRoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0) { path.AddRectangle(bounds); return path; }

            path.AddArc(arc, 180, 90); arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90); arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90); arc.X = bounds.Left;
            path.AddArc(arc, 90, 90); path.CloseFigure();
            return path;
        }
    }
}