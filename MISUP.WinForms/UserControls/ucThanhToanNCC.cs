using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MISUP.WinForms
{
    public partial class ucThanhToanNCC : UserControl
    {
        private DataTable dt;

        public ucThanhToanNCC()
        {
            InitializeComponent();
            AttachEvents();
            LoadMockData();
            SetRoundedRegion(pnlCard, 15);
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

        private void AttachEvents()
        {
            txtTimKiem.Enter += (s, e) => { if (txtTimKiem.Text.Contains("Tìm")) { txtTimKiem.Text = ""; txtTimKiem.ForeColor = Color.Black; } };
            txtTimKiem.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txtTimKiem.Text)) { txtTimKiem.Text = "🔍 Tìm theo mã phiếu chi..."; txtTimKiem.ForeColor = Color.Gray; } };

            dgvData.CellPainting += DgvData_CellPainting;

            btnTim.Click += (s, e) => {
                string key = txtTimKiem.Text.Contains("Tìm") ? "" : txtTimKiem.Text.Trim();
                if (dt != null) dt.DefaultView.RowFilter = $"MaPhieu LIKE '%{key}%' OR TenNCC LIKE '%{key}%'";
            };

            btnLapPhieuChi.Click += (s, e) => {
                dt.Rows.InsertAt(dt.NewRow(), 0);
                dt.Rows[0]["MaPhieu"] = "PC00" + (dt.Rows.Count + 1);
                dt.Rows[0]["ThoiGian"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                dt.Rows[0]["TenNCC"] = "NCC Vừa Chọn";
                dt.Rows[0]["SoTien"] = "0";
                dt.Rows[0]["PhuongThuc"] = "Tiền mặt";
                dt.Rows[0]["TrangThai"] = "Kỳ hạn nợ";
                MessageBox.Show("Đã lập phiếu chi mới!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
        }

        private void LoadMockData()
        {
            dt = new DataTable();
            dt.Columns.Add("MaPhieu"); dt.Columns.Add("ThoiGian"); dt.Columns.Add("TenNCC"); dt.Columns.Add("SoTien"); dt.Columns.Add("PhuongThuc"); dt.Columns.Add("TrangThai");

            dt.Rows.Add("PC0001", "20/06/2026 15:00", "Công ty CP Sữa Việt Nam", "125,500,000", "Chuyển khoản", "Đã thanh toán");
            dt.Rows.Add("PC0002", "19/06/2026 10:30", "Tập đoàn Masan", "20,000,000", "Tiền mặt", "Đã thanh toán");
            dt.Rows.Add("PC0003", "18/06/2026 14:15", "Samsung Electronics", "150,000,000", "Chuyển khoản", "Kỳ hạn nợ");

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
                SizeF textSize = g.MeasureString(text, e.CellStyle.Font);
                Rectangle badgeRect = new Rectangle(e.CellBounds.Left + 15, e.CellBounds.Top + (e.CellBounds.Height - 26) / 2, (int)textSize.Width + 20, 26);

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