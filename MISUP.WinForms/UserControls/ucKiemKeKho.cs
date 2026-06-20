using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MISUP.WinForms
{
    public partial class ucKiemKeKho : UserControl
    {
        private DataTable dt;

        public ucKiemKeKho()
        {
            InitializeComponent();

            // ÉP CHẶT LƯỚI KHÔNG CHO XÔ LỆCH
            dgvData.AllowUserToResizeColumns = false;
            dgvData.AllowUserToResizeRows = false;
            dgvData.AllowUserToOrderColumns = false;

            AttachEvents();
            LoadMockData();
        }

        private void AttachEvents()
        {
            txtTimKiem.Enter += (s, e) => { if (txtTimKiem.Text == "Tìm mã phiếu...") { txtTimKiem.Text = ""; txtTimKiem.ForeColor = Color.Black; } };
            txtTimKiem.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txtTimKiem.Text)) { txtTimKiem.Text = "Tìm mã phiếu..."; txtTimKiem.ForeColor = Color.Gray; } };
            dgvData.CellPainting += DgvData_CellPainting;

            // Xử lý tạo phiếu
            btnThem.Click += (s, e) => {
                dt.Rows.InsertAt(dt.NewRow(), 0);
                dt.Rows[0]["MaPhieu"] = "PKK" + DateTime.Now.ToString("MMddHHmm");
                dt.Rows[0]["ThoiGian"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                dt.Rows[0]["NhanVien"] = "Admin";
                dt.Rows[0]["KhoKiem"] = "Kho Tổng HN";
                dt.Rows[0]["SLChenhLech"] = "0";
                dt.Rows[0]["TrangThai"] = "Đang xử lý";
                MessageBox.Show("Đã tạo phiếu kiểm kê nháp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
        }

        private void LoadMockData()
        {
            dt = new DataTable();
            dt.Columns.Add("MaPhieu"); dt.Columns.Add("ThoiGian"); dt.Columns.Add("NhanVien"); dt.Columns.Add("KhoKiem"); dt.Columns.Add("SLChenhLech"); dt.Columns.Add("TrangThai");

            dt.Rows.Add("PKK240615", "15/06/2026 14:30", "Nguyễn Văn A", "Kho Tổng HN", "-5", "Đã cân bằng");
            dt.Rows.Add("PKK240510", "10/05/2026 09:15", "Trần Thị B", "Kho Miền Nam", "+2", "Đã cân bằng");
            dt.Rows.Add("PKK240401", "01/04/2026 10:00", "Lê Văn C", "Kho Tổng HN", "-10", "Đang xử lý");
            dt.Rows.Add("PKK240315", "15/03/2026 16:45", "Nguyễn Văn A", "Kho Miền Trung", "0", "Đã hủy");

            dgvData.DataSource = dt;
            dgvData.Columns["MaPhieu"].HeaderText = "Mã Kiểm Kê"; dgvData.Columns["ThoiGian"].HeaderText = "Ngày Kiểm"; dgvData.Columns["NhanVien"].HeaderText = "Người Kiểm"; dgvData.Columns["KhoKiem"].HeaderText = "Kho"; dgvData.Columns["SLChenhLech"].HeaderText = "SL Chênh Lệch"; dgvData.Columns["TrangThai"].HeaderText = "Trạng Thái";
        }

        private void DgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvData.Columns["TrangThai"].Index && e.Value != null)
            {
                e.PaintBackground(e.CellBounds, true);
                string text = e.Value.ToString();
                Color bgColor = Color.White, textColor = Color.Black, borderColor = Color.Gray;

                if (text == "Đã cân bằng") { bgColor = Color.FromArgb(237, 247, 237); textColor = Color.FromArgb(46, 125, 50); borderColor = Color.FromArgb(200, 230, 201); }
                else if (text == "Đang xử lý") { bgColor = Color.FromArgb(255, 244, 229); textColor = Color.FromArgb(255, 152, 0); borderColor = Color.FromArgb(255, 224, 178); }
                else if (text == "Đã hủy") { bgColor = Color.FromArgb(255, 235, 238); textColor = Color.FromArgb(211, 47, 47); borderColor = Color.FromArgb(255, 205, 210); }

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